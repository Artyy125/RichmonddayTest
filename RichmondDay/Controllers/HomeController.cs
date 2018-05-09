using Elmah;
using RichmondDay.Interface;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using RichmondDay.Models;
using System.Threading.Tasks;

namespace RichmondDay.Controllers
{
    public class HomeController : Controller
    {
        private IInfo _info;
        public HomeController(IInfo info)
        {
            _info = info;
        }
        public ActionResult Index(string sortOrder, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.FirstNameSortParam = String.IsNullOrEmpty(sortOrder) ? "fDesc" : "";
                ViewBag.LastNameSortParam = String.IsNullOrEmpty(sortOrder) ? "lDesc" : "";
                var allInfo = _info.GetAllInfo(sortOrder);
                int pageNumber = (page ?? 1);
                return View(allInfo.ToPagedList(pageNumber, 10));  
            }
            catch (Exception ex)
            {

                ErrorLog.GetDefault(null).Log(new Error(ex));
                throw;
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> Save(RichmonddayInfoModel data)
        {
            try
            {
                int recordId = await _info.Save(data);
                return Json(recordId, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                throw;
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                string result = await _info.Delete(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                throw;
            }
        }
    }
}