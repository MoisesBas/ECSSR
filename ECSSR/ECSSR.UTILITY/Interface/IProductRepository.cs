using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ECSSR.UTILITY.ElasticSearch;
using ECSSR.UTILITY.Model;

namespace ECSSR.UTILITY.Interface
{
    public interface IProductRepository
    {
        Task Save(ProductModel content);
        Task SaveBulk(IEnumerable<ProductModel> content);  
        void Initialize();
        Task<SearchResults<ProductModel>> Search(SearchRequest request);
    }
}
