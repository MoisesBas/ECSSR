using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.UTILITY.Interface;

namespace ECSSR.DOMAIN.Entities
{
    public partial class ProductImage : Entity<int>, ITrackUpdated, ITrackCreated
    {
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
        public int? ProductId { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public Product Product { get; set; }
    }
}
