using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.ElasticSearch
{
    public class SearchResults<T> where T : class
    {
        public List<T> Items { get; set; }
        public long TotalResults { get; set; }       

        public SearchResults()
        {
            Items = new List<T>();
        }
    }
}
