using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.StatusTypeReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.StatusTypeReference
{
    public class StatusTypeReferenceController : Controller
    {
        #region web
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> WebInsert(string name, string description) {
            try {
                var id = Guid.NewGuid();
                if (StatusTypeReferenceService.Insert(id, name, description)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region request post

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var name = Request.Form["name"];
                var description=Request.Form["desc"];
                if (StatusTypeReferenceService.Insert(id, name, description))
                {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id){
            try{
                var data = StatusTypeReferenceService.GetByID(Guid.Parse(id));
                return Success(StatusTypesReferencesVM.MToVM(data));
            }catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByList() {
            try {
                var data = StatusTypeReferenceService.GetByList();
                return Success(StatusTypesReferencesVM.MsToVMs(data));
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