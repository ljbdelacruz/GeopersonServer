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
    public class IS_ItemController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var title = Request.Form["title"];
                var description = Request.Form["description"];
                var api = Guid.Parse(Request.Form["api"]);
                var itemCategory = Guid.Parse(Request.Form["ic"]);
                var isCount = Boolean.Parse(Request.Form["isCount"]);
                var storeAPI = Guid.Parse(Request.Form["spi"]);
                if (IS_ItemService.Insert(id, title, description, api, itemCategory,isCount, 0, storeAPI)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var api = Guid.Parse(Request.Form["api"]);
                if (IS_ItemService.Remove(id, api)) {
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
                var title = Request.Form["title"];
                var description = Request.Form["description"];
                var api = Guid.Parse(Request.Form["api"]);
                if (IS_ItemService.Update(id, title, description, api)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByAPI(string id) {
            try {
                var data = IS_ItemService.GetByAPI(Guid.Parse(id));
                return Success(IS_ItemVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByItemCategory(string api, string category) {
            try {
                var data = IS_ItemService.GetByItemCategory(Guid.Parse(api), Guid.Parse(category));
                return Success(IS_ItemVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string api) {
            try {
                var data = IS_ItemService.GetByID(Guid.Parse(id), Guid.Parse(api));
                return Success(IS_ItemVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region util
        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}