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
    public class ItemController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var title = Request.Form["title"];
                var description = Request.Form["description"];
                var price = float.Parse(Request.Form["price"]);
                var ownerID = Guid.Parse(Request.Form["ownerID"]);
                var categoryID = Guid.Parse(Request.Form["catID"]);
                var longitude = float.Parse(Request.Form["longitude"]);
                var latitude = float.Parse(Request.Form["latitude"]);
                var api = Guid.Parse(Request.Form["API"]);
                var postType = int.Parse(Request.Form["ptype"]);
                if (ItemService.Insert(id, title, description, price, ownerID, categoryID, longitude, latitude, false, api, DateTime.Now, DateTime.Now, postType)) {
                    return Json(new { success = true, data = id });
                }
                return Json(new { success = false, message = MessageUtility.ServerError() });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Archive() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var api = Guid.Parse(Request.Form["api"]);
                var archive = Boolean.Parse(Request.Form["archive"]);
                if (ItemService.Archive(id, archive))
                {
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
                var price = float.Parse(Request.Form["price"]);
                var catID = Guid.Parse(Request.Form["catID"]);
                var archive = Boolean.Parse(Request.Form["archive"]);
                var postType = int.Parse(Request.Form["ptype"]);
                if (ItemService.Update(id, title, description, catID, archive, price, postType)){
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UpdateTimesViewed()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var addon = int.Parse(Request.Form["count"]);
                if (ItemService.UpdateTimesViewed(id, addon))
                {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string api, string ia) {
            try {
                var data = ItemService.GetByID(Guid.Parse(id), Guid.Parse(api), Boolean.Parse(ia));
                return Success(ItemsViewModel.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByOwnerID(string id, string archived) {
            try {
                var data = ItemService.GetByOwnerID(Guid.Parse(id), Boolean.Parse(archived));
                return Success(ItemsViewModel.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        //create a method that will posts within your area/ radius
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByLocationRadius(float longitude, float latitude, float radius) {
            try {
                var data = ItemService.GetByLocationRadius(longitude + radius, latitude + radius);
                return Json(new { success = true, data=ItemsViewModel.MsToVMs(data) }, JsonRequestBehavior.AllowGet);
            } catch { return Json(new { success = false }, JsonRequestBehavior.AllowGet); }
        }
        //[AllowCrossSiteJson]
        //[HttpGet]
        //public async Task<JsonResult> GetByIDStatus(string id) {
        //    try {
        //        var data = ItemService.Get(Guid.Parse(id));
        //        return Success(data.isArchived);
        //    } catch { return Failed(MessageUtility.ServerError()); }
        //}

        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByMostViewed(string take, string longi, string lat) {
            try {
                //most viewed ads near your location
                var data = ItemService.GetByMostViewedItem(int.Parse(take), float.Parse(longi), float.Parse(lat));
                return Success(ItemsViewModel.MsToVMs(data));
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