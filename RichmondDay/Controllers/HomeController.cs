using Elmah;
using RichmondDay.Interface;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using RichmondDay.Models;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using RichmondDay.Helper;

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
        public async Task<ActionResult> SaveInfo(RichmonddayInfoModel data, string sortOrder = "", int pageNumber = 1)
        {
            try
            {
                ViewBag.PageNumber = pageNumber;
                ViewBag.CurrentSort = sortOrder;
                if (ModelState.IsValid)
                {
                    int recordId = await _info.Save(data);
                }
                else
                {
                    // do business logic
                }
                var allInfo = _info.GetAllInfo(sortOrder);
                return PartialView("~/Views/Partials/_SaveInfo.cshtml", allInfo.ToPagedList(pageNumber, 10));

            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateInfo(RichmonddayInfoModel data,string sortOrder="",int pageNumber = 1)
        {
            ViewBag.PageNumber = pageNumber;
            ViewBag.CurrentSort = sortOrder;
            try
            {
                if (ModelState.IsValid)
                {
                    string result = await _info.Update(data);
                }
                else
                {
                    // do business logic
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
        [HttpPost]
        public async Task<JsonResult> DeleteInfo(int id,string sortOrder = "", int pageNumber = 1)
        {
            ViewBag.PageNumber = pageNumber;
            ViewBag.CurrentSort = sortOrder;
            string result = "";
            try
            {
                if (ModelState.IsValid)
                {
                    result = await _info.Delete(id);
                }
                else
                {
                    // do business logic
                }
                var allInfo = _info.GetAllInfo(sortOrder);
                var html = PartialView("~/Views/Partials/_Info.cshtml").RenderToString(allInfo.ToPagedList(pageNumber, 10));
                if (Request.IsAjaxRequest())
                {
                    return Json(new { message = html });
                }
                else
                {
                    return Json(new {error = "it's not an ajax call", message = html });
                }
                

            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                throw;
            }
        }
    }
}