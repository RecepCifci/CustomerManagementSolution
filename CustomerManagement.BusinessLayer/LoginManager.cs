using CustomerManagement.BusinessLayer.Abstract;
using CustomerManagement.BusinessLayer.Results;
using CustomerManagement.DataAccessLayer;
using CustomerManagement.Entities;
using CustomerManagement.Entities.Base;
using CustomerManagement.Entities.Messages;
using CustomerManagement.Entities.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.BusinessLayer
{
    public class LoginManager : ManagerBase<CMUser>
    {
        public void initdb()
        {
            DatabaseContext db = new DatabaseContext();
            db.Category.ToList();
        }
        public BusinessLayerResult<CMUser> LoginUser(LoginViewModel loginModel)
        {
            BusinessLayerResult<CMUser> result = new BusinessLayerResult<CMUser>();
            result.Result = Find(x => x.Email == loginModel.Email && x.Password == loginModel.Password);
            if (result.Result != null)
            {
                if (!result.Result.IsActive)
                {
                    result.AddError(ErrorMessageCode.CustomerInActive, "Kullanıcı aktifleştirilmemiştir");
                }
            }
            else
            {
                result.AddError(ErrorMessageCode.CustomerCouldNotFind, "Kullanıcı bulunamadı");

            }
            return result;
        }
    }
}
