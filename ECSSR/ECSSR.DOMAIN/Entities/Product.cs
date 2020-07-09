using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.UTILITY.Interface;

namespace ECSSR.DOMAIN.Entities
{
    public partial class Product:Entity<int>, ITrackUpdated, ITrackCreated
    {       
        public string Name { get; set; }
        public string Color { get; set; }
        public string CompanyName { get; set; }
        public byte[] Video { get; set; }
        public string Category { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<ProductImage> Images => new HashSet<ProductImage>();
    }
}
