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
    public class ItemSubCategoryController : Controller
    {
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByCategory(string id) {
            try {
                var data = ItemSubCategoryService.GetByCategoryID(Guid.Parse(id));
                return Success(ItemSubCategoryVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id) {
            try {
                var data = ItemSubCategoryService.GetByID(Guid.Parse(id));
                return Success(ItemSubCategoryVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var name = Request.Form["name"];
                var catID = Guid.Parse(Request.Form["catID"]);
                if (ItemSubCategoryService.Insert(id, name, catID)) {
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
                var archive = Boolean.Parse(Request.Form["archived"]);
                if (ItemSubCategoryService.Update(id, name, archive)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}