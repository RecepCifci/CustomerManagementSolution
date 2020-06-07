using CustomerManagement.BusinessLayer.Abstract;
using CustomerManagement.BusinessLayer.Results;
using CustomerManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        public override int Delete(Category obj)
        {
            IncidentManager incidentManager = new IncidentManager();
            var incidentList = incidentManager.ListQueryable().Where(x => x.CategoryId == obj.Id);

            foreach (var item in incidentList)
            {
                incidentManager.Delete(item);
            }
            return base.Delete(obj);
        }
    }
}
