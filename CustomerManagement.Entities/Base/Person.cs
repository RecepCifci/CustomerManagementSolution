using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Entities.Base
{
    public class Person : EntityBase
    {

        [Required, DisplayName("Ad"), StringLength(50)]
        public string Name { get; set; }
        [Required, DisplayName("Soyad"), StringLength(50)]
        public string Surname { get; set; }
        [Required, DisplayName("E-posta"), StringLength(100)]
        public string Email { get; set; }
        [Required, DisplayName("Şifre"), StringLength(50)]
        public string Password { get; set; }
        public string Fullname => string.Format("{0} {1}", Name, Surname);
        [ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }
        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }
        [DisplayName("Aktif Mi?")]
        public bool IsActive { get; set; }
    }
}
