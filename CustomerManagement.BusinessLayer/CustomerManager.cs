using CustomerManagement.BusinessLayer.Abstract;
using CustomerManagement.BusinessLayer.Results;
using CustomerManagement.Common.Helpers;
using CustomerManagement.Entities;
using CustomerManagement.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.BusinessLayer
{
    public class CustomerManager : ManagerBase<Customer>
    {
        public override int Delete(Customer obj)
        {
            IncidentManager incidentManager = new IncidentManager();
            var incidentList = incidentManager.ListQueryable().Where(x => x.CustomerId == obj.Id);

            foreach (var item in incidentList)
            {
                incidentManager.Delete(item);
            }
            return base.Delete(obj);
        }
        public new BusinessLayerResult<Customer> Insert(Customer data)
        {
            Customer customer = Find(x => x.Email == data.Email);
            BusinessLayerResult<Customer> res = new BusinessLayerResult<Customer>();

            res.Result = data;

            if (customer != null)
            {
                if (customer.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                res.Result.ProfileImageFilename = "user_boy.png";
                res.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı eklenemedi.");
                }
                res.Result = Find(x => x.Email == data.Email);

                string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                string activateUri = siteUri + "/Customer/CustomerActivate/" + res.Result.ActivateGuid;
                string body = "Merhaba " + res.Result.Fullname + ";<br><br>Hesabınızı aktifleştirmek için <a href='" + activateUri + "' target='_blank'>tıklayınız</a>.";

                MailHelper.SendMail(body, res.Result.Email, "Talep Yönetim Sistemi Hesap Aktifleştirme");
            }
            return res;
        }

        public BusinessLayerResult<Customer> ActivateUser(Guid id)
        {
            BusinessLayerResult<Customer> res = new BusinessLayerResult<Customer>();
            res.Result = Find(x => x.ActivateGuid == id);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Müşteri zaten aktif edilmiştir.");
                    return res;
                }

                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek müşteri bulunamadı.");
            }

            return res;
        }
    }
}
