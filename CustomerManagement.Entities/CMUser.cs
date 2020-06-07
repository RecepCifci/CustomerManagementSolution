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
    [Table("CMUsers")]
    public class CMUser : Person
    {
        [DisplayName("Admin Mi?")]
        public bool IsAdmin { get; set; }
        public virtual List<Incident> Incident { get; set; }
    }
}
