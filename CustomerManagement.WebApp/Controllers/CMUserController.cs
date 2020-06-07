using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Entities;
using CustomerManagement.WebApp.Models;
using CustomerManagement.BusinessLayer;
using CustomerManagement.BusinessLayer.Results;
using CustomerManagement.Entities.ModelView;
using CustomerManagement.WebApp.Filters;

namespace CustomerManagement.WebApp.Controllers
{
    [Exc]
    public class CMUserController : Controller
    {
        private CMUserManager cmUserManager = new CMUserManager();

        public ActionResult Index()
        {
            return View(cmUserManager.List());
        }

        public ActionResult Details(Guid activateGuid)
        {
            if (activateGuid == null || activateGuid == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMUser cMUser = cmUserManager.Find(x => x.ActivateGuid == activateGuid);
            if (cMUser == null)
            {
                return HttpNotFound();
            }
            return View(cMUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CMUser cMUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<CMUser> res = cmUserManager.RegisterUser(cMUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return RedirectToAction("Index");
                }
            }

            return View(cMUser);
        }

        public ActionResult Edit(Guid activateGuid)
        {
            if (activateGuid == null || activateGuid == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMUser cMUser = cmUserManager.Find(x => x.ActivateGuid == activateGuid);
            if (cMUser == null)
            {
                return HttpNotFound();
            }
            return View(cMUser);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CMUser cMUser, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                    (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{cMUser.ActivateGuid}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/Image/{filename}"));
                    cMUser.ProfileImageFilename = filename;
                }

                BusinessLayerResult<CMUser> res = cmUserManager.UpdateProfile(cMUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return RedirectToAction("Index");
                }

                // Profil güncellendiği için session güncellendi.
                CurrentSession.Set<CMUser>("login", res.Result);

                return RedirectToAction("Details", new { activateGuid = res.Result.ActivateGuid });
            }

            return View(cMUser);
        }

        public ActionResult Delete(Guid activateGuid)
        {
            if (activateGuid == null || activateGuid == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMUser cMUser = cmUserManager.Find(x => x.ActivateGuid == activateGuid);
            if (cMUser == null)
            {
                return HttpNotFound();
            }
            return View(cMUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid activateGuid)
        {
            if (activateGuid == null || activateGuid == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMUser cMUser = cmUserManager.Find(x => x.ActivateGuid == activateGuid);
            cmUserManager.Delete(cMUser);
            return RedirectToAction("Index");
        }

        public ActionResult UserActivate(Guid id)
        {
            ActivationViewModal<string> activationViewModal = new ActivationViewModal<string>();
            BusinessLayerResult<CMUser> res = cmUserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                activationViewModal.Title = "Etkinleştirme Hatası";
                res.Errors.ForEach(x => activationViewModal.Items.Add(x.Message));

                ViewBag.Result = res.Errors;
                return View(activationViewModal);
            }

            return View(activationViewModal);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

    }
}
