using Elmah;
using RichmondDay.Interface;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;

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
    }
}