using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API
{
    public class ItemCategoryController : Controller
    {
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id) {
            try {
                var data = ItemCategoryService.GetByID(Guid.Parse(id));
                return Success(ItemCategoryVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetAll() {
            try {
                var data = ItemCategoryService.GetAll();
                return Success(ItemCategoryVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var Name = Request.Form["name"].ToString();
                if (ItemCategoryService.Insert(id, Name)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Update() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var name = Request.Form["name"];
                var archive = Boolean.Parse(Request.Form["archive"]);
                if (ItemCategoryService.Update(id, name, archive)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message=message}, JsonRequestBehavior.AllowGet);
        }
    }
}