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
    [Table("Incidents")]
    public class Incident : EntityBase
    {
        [DisplayName("Başlık"), StringLength(100)]
        public string Title { get; set; }
        [DisplayName("Açıklama"), StringLength(2000)]
        public string Description { get; set; }
        [DisplayName("Başlangıç Tarihi"), Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayName("Bitiş Tarihi"), Required]
        public DateTime EndDate { get; set; }
        [DisplayName("Durum")]
        public int StateCode { get; set; }


        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }


        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual CMUser Owner { get; set; }
        public virtual Category Category { get; set; }
    }
}
