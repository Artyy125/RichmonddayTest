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
                ViewBag.FirstNameSortParam = String.IsNullOrEmpty(sortOrder) ? "fDesc" : null;
                ViewBag.LastNameSortParam = String.IsNullOrEmpty(sortOrder) ? "lDesc" : null;
                var allInfo = _info.GetAllInfo(sortOrder);
                int pageNumber = (page ?? 1);
                ViewBag.pageNumber = pageNumber;
                return View(allInfo.ToPagedList(pageNumber, 10));  
            }
            catch (Exception ex)
            {

                ErrorLog.GetDefault(null).Log(new Error(ex));
                throw;
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> UpdateInfo(string submitButton, string sortOrder="",int pageNumber = 1, int id=0, RichmonddayInfoModel data=null)
        {
            ViewBag.PageNumber = pageNumber;
            ViewBag.CurrentSort = sortOrder;
            try
            {
                switch (submitButton)
                {
                    case "Save":
                        int recordId = await _info.Save(data);
                        break;
                    case "Delete":
                        string result = await _info.Delete(id);
                        break;
                    case "Update":
                        break;
                }
                var allInfo = _info.GetAllInfo(sortOrder);
                return PartialView("~/Views/Partials/_Info.cshtml", allInfo.ToPagedList(pageNumber, 10));

            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                throw;
            }
        }
    }
}