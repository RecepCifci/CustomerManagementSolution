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
    public class CMUserManager : ManagerBase<CMUser>
    {
        public override int Delete(CMUser obj)
        {
            IncidentManager incidentManager = new IncidentManager();
            var incidentList = incidentManager.ListQueryable().Where(x => x.OwnerId == obj.Id);

            foreach (var item in incidentList)
            {
                incidentManager.Delete(item);
            }
            return base.Delete(obj);
        }
        public BusinessLayerResult<CMUser> RegisterUser(CMUser data)
        {
            CMUser user = Find(x => x.Email == data.Email);
            BusinessLayerResult<CMUser> res = new BusinessLayerResult<CMUser>();

            if (user != null)
            {
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                int dbResult = base.Insert(new CMUser()
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Email = data.Email,
                    ProfileImageFilename = "user_boy.png",
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Email == data.Email);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = siteUri + "/CMUser/UserActivate/" + res.Result.ActivateGuid;
                    string body = "Merhaba " + res.Result.Fullname + ";<br><br>Hesabınızı aktifleştirmek için <a href='" + activateUri + "' target='_blank'>tıklayınız</a>.";

                    MailHelper.SendMail(body, res.Result.Email, "Talep Yönetim Sistemi Hesap Aktifleştirme");
                }
            }

            return res;
        }

        public BusinessLayerResult<CMUser> ActivateUser(Guid id)
        {
            BusinessLayerResult<CMUser> res = new BusinessLayerResult<CMUser>();
            res.Result = Find(x => x.ActivateGuid == id);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }

                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<CMUser> UpdateProfile(CMUser cMUser)
        {
            CMUser db_user = Find(x => x.Id != cMUser.Id && x.Email == cMUser.Email);
            BusinessLayerResult<CMUser> res = new BusinessLayerResult<CMUser>();

            if (db_user != null && db_user.Id != cMUser.Id)
            {
                if (db_user.Email == cMUser.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == cMUser.Id);
            res.Result.Email = cMUser.Email;
            res.Result.Name = cMUser.Name;
            res.Result.Surname = cMUser.Surname;
            res.Result.Password = cMUser.Password;
            res.Result.IsAdmin = cMUser.IsAdmin;

            if (!string.IsNullOrEmpty(cMUser.ProfileImageFilename))
            {
                res.Result.ProfileImageFilename = cMUser.ProfileImageFilename;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;
        }
    }
}
