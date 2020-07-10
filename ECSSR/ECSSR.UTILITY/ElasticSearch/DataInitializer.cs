using System.Linq;
using ECSSR.UTILITY.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECSSR.UTILITY.ElasticSearch
{
    public class DataInitializer<T> : IDataInitializer where T: class
    {
        private readonly IProductRepository _productRepository;
        
        private readonly IECSSRDbContext _eCSSRDbContext;
        public DataInitializer(IECSSRDbContext eCSSRDbContext, IProductRepository productRepository)
        {
            _eCSSRDbContext = eCSSRDbContext;
            _productRepository = productRepository;
           

        }
        public void Initialize()
        {
            var totalProcess = 0;
            _productRepository.Initialize();        

            while (true)
            {
                const int batchSize = 100;
                var processed = PerformIndexing(batchSize, totalProcess);
                totalProcess += processed;
                if (processed < batchSize) break;
            }
        }

        public int PerformIndexing(int batchSize, int batchSkip = 0)
        {
            var models = _eCSSRDbContext.Set<T>()                         
                         .AsNoTracking()
                         .Skip(batchSkip)
                         .Take(batchSize).AsEnumerable();
            var result = _productRepository.SaveBulk(null).ConfigureAwait(false);
            return null;
        }     

    }
}
