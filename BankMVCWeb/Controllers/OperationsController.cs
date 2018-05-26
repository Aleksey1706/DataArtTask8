using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BankMVCWeb.Models;


namespace BankMVCWeb.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {
        private BankModel db = new BankModel();

        // GET: Operations

      
        public ActionResult Index(LoginModel model)
        {
          
            var cardId = model.CadrID;
            var operations = db.Operations
                .Include(o => o.Card).Include(o => o.Card1).Where(o => o.InID == cardId || o.OutID == cardId);
            ViewBag.Card = cardId;
            return View(operations.ToList());
        }
        [Authorize]
        public ActionResult Ballance()
        {
            ViewBag.InID = new SelectList(db.Card, "CadrID", "PinCode");
            ViewBag.OutID = new SelectList(db.Card, "CadrID", "PinCode");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Ballance([Bind(Include ="OperationID,InID,OutID,Amount,OperationTime")] Operations operations)
        {
            if (ModelState.IsValid)
            {
                //check ballance
                if (db.Card.FirstOrDefault(u=>u.CadrID==operations.OutID && u.Ballance>=operations.Amount)!=null)
                {
                    db.Operations.Add(operations);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
           
               
            }
            ViewBag.InID = new SelectList(db.Card, "CadrID", "PinCode");
            ViewBag.OutID = new SelectList(db.Card, "CadrID", "PinCode");
            return View(operations);
        }
        
       
    }
}
