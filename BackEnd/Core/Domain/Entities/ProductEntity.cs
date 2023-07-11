using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeDomain.Entities
{
    public class ProductEntity
    {
        public string IdProduct { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IdCategory { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string? Notes { get; set; }
    }
}
