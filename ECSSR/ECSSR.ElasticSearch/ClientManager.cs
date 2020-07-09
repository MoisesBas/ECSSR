using System;
using ECSSR.UTILITY.Others;
using Microsoft.Extensions.Options;
using Nest;

namespace ECSSR.ElasticSearch
{
    internal class ClientManager
    {
        private static readonly object Locker = new object();
        private static ClientManager _manager;
        private IOptions<ECSSRSettings> _configuration;

        private ClientManager(IOptions<ECSSRSettings> configuration)
        {
            _configuration = configuration;
        }

        public ElasticClient GetClient<T>(string indexName) where T : class
        {


            var node = new Uri(_configuration.Value.Url);
            var settings = new ConnectionSettings(node)
                .DisableDirectStreaming()
                .DefaultIndex(indexName)
                .DefaultMappingFor<T>(m => m.IndexName(indexName));
            return new ElasticClient(settings);
        }

        public static ClientManager Create(IOptions<ECSSRSettings> configuration)
        {
            lock (Locker)
            {
                if (_manager == null)
                {
                    _manager = new ClientManager(configuration);
                }
            }

            return _manager;
        }
    }
}
