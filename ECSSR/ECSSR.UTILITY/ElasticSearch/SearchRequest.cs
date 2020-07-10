using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.ElasticSearch
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            this.PageSize = 20;
        }        

        public string Name { get; set; }
        public string Color { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
