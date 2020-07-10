using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Others;
using Microsoft.Extensions.Options;
using Nest;

namespace ECSSR.UTILITY.ElasticSearch
{
    public abstract class RepositoryBase<T> : IRepositoryBase where T : class      
    {
        protected const int MaxQuerySize = 10000;
        private readonly ClientManager _clientManager;
        public abstract string IndexName { get; }
        public IOptions<ECSSRSettings> _configuration;

        protected RepositoryBase(IOptions<ECSSRSettings> configuration)
        {
            _configuration = configuration;
            _clientManager = ClientManager.Create(_configuration);
        }
        protected virtual async Task<int> PerformDocumentIndexing(string indexName, List<T> documents)
        {
            var client = GetClient();
            if (documents.Any())
            {
                var bulkIndexResponse = await client.BulkAsync(b => b

                       .IndexMany(documents, (op, item) => op
                           .Index(indexName)
                       )
                    );

                if (bulkIndexResponse.Errors)
                {
                    // Handle error...
                }

                return bulkIndexResponse.Items.Count;
            }

            return 0;
        }
        public virtual void DeleteIndexIfExists()
        {
            GetClient().Indices.Delete(IndexName);
        }
        public virtual void CreateIndex()
        {
            GetClient().Indices
                      .Create(IndexName, options =>
                                         options.Settings(CommonIndexDescriptor)
                                         .Map<T>(m => ConfiguredocMapping(m)));
        }

        static IPromise<IIndexSettings> CommonIndexDescriptor(IndexSettingsDescriptor descriptor)
        {
            return descriptor
                .NumberOfReplicas(1)
                .NumberOfShards(1);
        }
        public virtual ITypeMapping ConfiguredocMapping(TypeMappingDescriptor<T> mapping) => mapping.AutoMap();

        protected ElasticClient GetClient()
        {
            return _clientManager.GetClient<T>(IndexName);
        }        

        protected void CheckResponse(IResponse response)
        {
            if (!response.IsValid)
                throw new ApplicationException("Impossible to get requested data", response.OriginalException);
        }
    }
}
