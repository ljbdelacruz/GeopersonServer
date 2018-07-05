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
    public class ItemAssignCategoryController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var itemID = Guid.Parse(Request.Form["iid"]);
                var subCat = Guid.Parse(Request.Form["subCat"]);
                if (ItemAssignCategoryService.Insert(id, itemID, subCat)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Update() {
            try {
                var itemID = Guid.Parse(Request.Form["id"]);
                var subCatID = Guid.Parse(Request.Form["subCat"]);
                if (ItemAssignCategoryService.UpdateByItemID(itemID, subCatID)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }

        }

        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByItemID(string id) {
            try {
                var data = ItemAssignCategoryService.GetByItemID(Guid.Parse(id));
                return Success(ItemAssignCategoryVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message){
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}