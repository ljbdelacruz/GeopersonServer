using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.TimerAppS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.TimerAppAPI
{
    public class TimerAppController : Controller
    {
        #region util
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = MessageUtility.ServerError() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region functionalities
        #region request post
        public async Task<JsonResult> CheckIsAccessible(){
            try {
                //this checks if timerApp is accessible or not
                var taid = Guid.Parse(Request.Form["id"]);
                var api = Guid.Parse(Request.Form["api"]);
                var tz = Guid.Parse(Request.Form["tz"]);
                var timeNow = DateTime.Now;
                if (TimerAppService.IsAccessible(taid, api, timeNow)) {
                    return Success(true);
                }
                return Failed(MessageUtility.Unaccessible());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        //get the remaining time
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> GetTimeRemaining() {
            try {
                //timeLeft id
                var tlid = Guid.Parse(Request.Form["id"]);
                var talid = Guid.Parse(Request.Form["talid"]);
                //this will determine the dateTime it will get based on this
                var timeZone = Request.Form["tz"];
                //time started-timeNow, then get number of seconds passed return it as the remaining time
                return Success("");
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
        #region TimerApp
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TAInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                var ca = DateTime.Now;
                var et = DateTime.Parse(Request.Form["et"]);
                var dt = DateTime.Parse(Request.Form["dt"]);
                if (TimerAppService.Insert(id, oid, api, ca, et, dt)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TARemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                if (TimerAppService.Remove(id, oid, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TAUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                var et = DateTime.Parse(Request.Form["et"]);
                var dt = DateTime.Parse(Request.Form["dt"]);
                if (TimerAppService.Update(id, oid, api, et, dt)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> TAGetByOID(string id, string aid) {
            try {
                var data = TimerAppService.GetByOID(Guid.Parse(id), Guid.Parse(aid));
                return Success(TimerAppVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        #endregion
        #endregion
        #region timerAppLimiters
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TALInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var srid = Guid.Parse(Request.Form["srid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                var sec = int.Parse(Request.Form["sec"]);
                if (TimerAppLimiterService.Insert(id, srid, oid, api, sec)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TALRemove(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                if (TimerAppLimiterService.Remove(id, oid, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TALUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var srid = Guid.Parse(Request.Form["srid"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var api = Guid.Parse(Request.Form["aid"]);
                var sec = int.Parse(Request.Form["sec"]);
                if (TimerAppLimiterService.Update(id, srid, oid, api, sec)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> TALGetByID(string id, string oid, string aid) {
            try {
                var data = TimerAppLimiterService.GetByID(Guid.Parse(id), Guid.Parse(oid), Guid.Parse(aid));
                return Success(TimerAppLimitersVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> TALGetByOID(string id, string aid) {
            try {
                var data = TimerAppLimiterService.GetByOID(Guid.Parse(id), Guid.Parse(aid));
                return Success(TimerAppLimitersVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        #endregion
        #endregion
        #region timerLeft
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TLInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var taid = Guid.Parse(Request.Form["taid"]);
                var api = Guid.Parse(Request.Form["api"]);
                var ca = DateTime.Now;
                var ts = DateTime.Parse(Request.Form["ts"]);
                var te = DateTime.Parse(Request.Form["te"]);
                if (TimerLeftService.Insert(id, oid, taid, api, ca, ts, te)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> TLRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var taid = Guid.Parse(Request.Form["taid"]);
                var api = Guid.Parse(Request.Form["api"]);
                if (TimerLeftService.Remove(id, oid, taid, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> TLGetByTAIDOID(string id, string oid, string api) {
            try {
                var data = TimerLeftService.GetByTAIDOID(Guid.Parse(id), Guid.Parse(oid), Guid.Parse(api));
                return Success(TimerLeftVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
    }
}