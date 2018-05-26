using BankMVCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankMVCWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
       
        [ValidateAntiForgeryToken]
        [HttpPost]
        public  ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
               Card card = null;
                using (BankModel db = new BankModel())
                {
                    card = db.Card.FirstOrDefault(u => u.CadrID == model.CadrID && u.PinCode == model.PinCode);

                }
                if (card != null)
                {
                    FormsAuthentication.SetAuthCookie(model.CadrID, true);
                   
                    return RedirectToAction("Index", "Operations",model);
                }
                else
                {
                    ModelState.AddModelError("", "Wrong pin or number of card");
                }
            }

            return View(model);
        }
    }
}