using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.InventorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.InventorySystem
{
    public class IS_ItemCategoryController : Controller
    {

        #region Request Post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var name = Request.Form["name"];
                var api = Guid.Parse(Request.Form["api"]);
                if (IS_ItemCategoryService.Insert(id, name, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var api = Guid.Parse(Request.Form["api"]);
                if (IS_ItemCategoryService.Remove(id, api)) {
                    return Success("");
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
                var api = Guid.Parse(Request.Form["api"]);
                if (IS_ItemCategoryService.Update(id, name, api)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region Get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByAPI(string api) {
            try {
                var data = IS_ItemCategoryService.GetByAPI(Guid.Parse(api));
                return Success(IS_ItemCategoryVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string api) {
            try {
                var data = IS_ItemCategoryService.GetByID(Guid.Parse(id), Guid.Parse(api));
                return Success(IS_ItemCategoryVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region util
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}