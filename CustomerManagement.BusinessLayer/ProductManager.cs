using CustomerManagement.BusinessLayer.Abstract;
using CustomerManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.BusinessLayer
{
    public class ProductManager : ManagerBase<Product>
    {
        public override int Delete(Product obj)
        {
            IncidentManager incidentManager = new IncidentManager();
            var incidentList = incidentManager.ListQueryable().Where(x => x.ProductId == obj.Id);

            foreach (var item in incidentList)
            {
                incidentManager.Delete(item);
            }
            return base.Delete(obj);
        }
    }
}
