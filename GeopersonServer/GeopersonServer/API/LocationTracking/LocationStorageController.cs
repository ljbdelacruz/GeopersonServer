using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.LocationTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static GeopersonServer.Models.Geoperson.UsersReviewVM;

namespace GeopersonServer.API.LocationTracking
{
    public class LocationStorageController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {

            try {
                var id = Guid.Parse(Request.Form["id"]);
                var ownerID = Request.Form["oid"];
                var api = Guid.Parse(Request.Form["api"]);
                var longitude = float.Parse(Request.Form["longitude"]);
                var latitude = float.Parse(Request.Form["latitude"]);
                var locationCategory = Request.Form["cat"];
                var desc = Request.Form["desc"];
                if (LocationStorageService.Insert(id, ownerID, api, longitude, latitude, locationCategory, desc)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch {
                return Failed(MessageUtility.ServerError());
            }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UpdateLocation(){
            try {
                var oid = Request.Form["oid"];
                var api = Guid.Parse(Request.Form["api"]);
                var longitude = float.Parse(Request.Form["longitude"]);
                var latitude = float.Parse(Request.Form["latitude"]);
                if (LocationStorageService.UpdateLocation(oid, api, longitude, latitude)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> GetByIDAdmin()
        {
            try
            {
                var ownerID = Request.Form["oid"];
                //category
                var cat = Request.Form["cat"];
                var data = LocationStorageService.GetByOwnerIDCat(ownerID, cat);
                return Success(LocationStorageVM.MToVM(data));
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByOwnerID(string id, string api) {
            try {
                var data = LocationStorageService.GetByOwnerIDAPI(id, Guid.Parse(api));
                return Success(LocationStorageVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string api) {
            try {
                var data = LocationStorageService.GetByIDAPI(Guid.Parse(id), Guid.Parse(api));
                return Success(LocationStorageVM.MToVM(data));
            }catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByCategory(string cat, string api) {
            try {
                var data = LocationStorageService.GetByCategory(cat, Guid.Parse(api));
                return Success(LocationStorageVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByCategoryLocationRadius(string lng, string lat, string cat, string aid, string rad) {
            try {
                var data = LocationStorageService.GetByContainCategoryLocationRadius(float.Parse(lng)+float.Parse(rad), float.Parse(lat)+float.Parse(rad), cat, Guid.Parse(aid));
                return Success(LocationStorageVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByLocationRadius(string lon, string lat, string cat, string api, string rad) {
            try {
                var data = LocationStorageService.GetByCategoryLocationRadius(float.Parse(lon)+float.Parse(rad), float.Parse(lat)+float.Parse(rad), cat, Guid.Parse(api));
                return Success(LocationStorageVM.MsToVMs(data));
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