using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Entities.ModelView
{
    public class ActivationViewModal<T>
    {
        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeout { get; set; }
        public ActivationViewModal()
        {
            Header = "Yönlendiriliyorsunuz!";
            Title = "Etkinleştirme Yapıldı";
            IsRedirecting = true;
            RedirectingUrl = "/Login/Index";
            RedirectingTimeout = 10000;
            Items = new List<T>();
        }
    }
}
