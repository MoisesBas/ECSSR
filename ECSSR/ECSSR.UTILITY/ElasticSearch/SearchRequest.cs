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
        public int Skip { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }

    }
}
