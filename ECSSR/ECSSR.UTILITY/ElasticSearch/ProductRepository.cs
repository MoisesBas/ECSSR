using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using ECSSR.UTILITY.Others;
using Microsoft.Extensions.Options;
using Nest;

namespace ECSSR.UTILITY.ElasticSearch
{
   
    public class ProductRepository : RepositoryBase<ProductModel>, IProductRepository
    {
        public override string IndexName => "Product";

        public ProductRepository(IOptions<ECSSRSettings> configuration)
            : base(configuration)
        {
        }
       
        public async Task Save(ProductModel suggest)
        {
            var client = GetClient();            
            await client.IndexAsync(suggest, m => m.Index(IndexName));
        }
        public async Task SaveBulk(IEnumerable<ProductModel> suggest)
        {          
            if (suggest.Any())
            {                
                await PerformDocumentIndexing(IndexName, suggest.ToList());
            }
        }

        
        public override void CreateIndex()
        {

            GetClient().Indices
                       .Create(IndexName, options =>
                                          options.Settings(CommonIndexDescriptor)
                                          .Map<ProductModel>(m => m.AutoMap()));
        }

        static IPromise<IIndexSettings> CommonIndexDescriptor(IndexSettingsDescriptor descriptor)
        {
            return descriptor
                .NumberOfReplicas(1)
                .NumberOfShards(1)
                .Analysis(InitCommonAnalyzers);
        }

        private static IAnalysis InitCommonAnalyzers(AnalysisDescriptor analysis)
        {
            return analysis;            
        }

        public void Initialize()
        {
            var client = GetClient();
            if (client.Indices.Exists(IndexName).Exists) base.DeleteIndexIfExists();
            this.CreateIndex();
        }

      

        public async Task<SearchResults<ProductModel>> Search(SearchRequest request)
        {
            try
            {

                var result = await GetClient().SearchAsync<ProductModel>(s =>
                s.From(request.Skip)
                 .Size(request.PageSize)
                 .Index(IndexName));
                


                var searchResult = new SearchResults<ProductModel>()
                {
                    TotalResults = result.Total,
                    DebugInformation = result.DebugInformation,
                    OriginalQuery = request.Query

                };
                foreach (var hit in result.Hits)
                {
                    var relatedDocument = result.Documents.FirstOrDefault(p => p.Id.ToString() == hit.Id);
                    searchResult.Items.Add(relatedDocument);
                }
                return searchResult;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
