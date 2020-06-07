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
    [Table("Customers")]
    public class Customer : Person
    {
        [DisplayName("Cep Telefonu"), StringLength(12)]
        public string MobilePhone { get; set; }

        public virtual List<Incident> Incident { get; set; }
    }
}
