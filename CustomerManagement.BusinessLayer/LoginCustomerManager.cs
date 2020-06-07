using CustomerManagement.BusinessLayer.Abstract;
using CustomerManagement.BusinessLayer.Results;
using CustomerManagement.Entities;
using CustomerManagement.Entities.Messages;
using CustomerManagement.Entities.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.BusinessLayer
{
    public class LoginCustomerManager : ManagerBase<Customer>
    {
        public BusinessLayerResult<Customer> LoginCustomer(LoginViewModel loginModel)
        {
            BusinessLayerResult<Customer> result = new BusinessLayerResult<Customer>();
            result.Result = Find(x => x.Email == loginModel.Email && x.Password == loginModel.Password);
            if (result.Result != null)
            {
                if (!result.Result.IsActive)
                {
                    result.AddError(ErrorMessageCode.CustomerInActive, "Müşteri aktifleştirilmemiştir");
                }
            }
            else
            {
                result.AddError(ErrorMessageCode.CustomerCouldNotFind, "Müşteri bulunamadı");

            }
            return result;
        }
    }
}
