using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerManagement.Entities.Base;

namespace CustomerManagement.Entities
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        [DisplayName("Kategori Adı"), StringLength(100)]
        public string Name { get; set; }
        [DisplayName("Açıklama"), StringLength(2000)]
        public string Description { get; set; }
        public virtual List<Incident> Incident { get; set; }
    }
}
