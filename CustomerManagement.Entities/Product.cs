using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Entities.Base;

namespace CustomerManagement.Entities
{
    [Table("Products")]
    public class Product : EntityBase
    {
        [Required, DisplayName("Ürün Adı"), StringLength(100)]
        public string Name { get; set; }
        [Required, DisplayName("Ürün Kodu"), StringLength(100)]
        public string Code { get; set; }
        [Required, DisplayName("Ürün Tipi"), StringLength(100)]
        public string Type { get; set; }

        public virtual List<Incident> Incident { get; set; }
    }
}
