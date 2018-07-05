using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GeopersonServer.API
{
    public class ConnectionController : Controller
    {
        #region HttpGet
        //get connection by memberID single
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetConnectionSingleByMID(string ID) {
            try {
                var data = ConnectionServices.GetByMemberSingleID(ID);
                var vmModel = new List<ConnectionViewModel>();
                foreach (var model in data) {
                    var temp = new ConnectionViewModel() {
                        ID=model.ID.ToString(),
                        GroupName=model.ConnectionName
                    };
                    foreach (var member in model.Members) {
                        //assign userinformationmodel
                        var vm=new UserInformationViewModel() {
                            User= member.UserID.ToString()
                        };
                        temp.PushMembers(member, vm);
                    }
                    vmModel.Add(temp);
                }
                return Json(new { success = true, data=vmModel }, JsonRequestBehavior.AllowGet);
            } catch(Exception e) {
                Console.Write(e);
                return Json(new { success = false, message = MessageUtility.ServerError() }, JsonRequestBehavior.AllowGet); }
        }
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        //this one gets the user information of the group members belong to the connection
        //update the location information of this user
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetConnectionMemberUpdatedLocation(string connection) {
            try {
                ConnectionViewModel param = json_serializer.Deserialize<ConnectionViewModel>(connection);
                var connectionData = ConnectionServices.GetByID(param.ID);
                var temp = new ConnectionViewModel()
                {
                    ID = param.ID,
                    GroupName = param.GroupName
                };
                foreach (var member in connectionData.Members) {
                    if (member.isArchived == false)
                    {
                        var vm = new UserInformationViewModel()
                        {
                            User = member.UserID.ToString(),
                        };
                        temp.PushMembers(member, vm);
                    }
                }
                return Json(new { success = true, data=temp }, JsonRequestBehavior.AllowGet);
            } catch { return Json(new { success = false, message=MessageUtility.ServerError() }, JsonRequestBehavior.AllowGet); }
        }
        //create connection
        //not group
        //user who created the group will be the first member
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> CreateConnection() {
            try {
                var uid = Request.Form["UID"];
                var conName = Request.Form["CNAME"];
                var API=Guid.Parse(Request.Form["API"]);
                var cid = Guid.NewGuid();
                ConnectionServices.Insert(cid, true, conName, DateTime.Now, API);
                var mid = Guid.NewGuid();
                ConnectionMemberService.InsertMember(mid, Guid.Parse(uid), cid.ToString(), DateTime.Now, false, true);
                return Json(new { success = true });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> LeaveConnection() {
            try {
                var mid = Request.Form["MID"];
                var uid = Request.Form["UID"];
                var cid = Request.Form["CID"];

                var conn = ConnectionServices.GetByID(cid);
                ConnectionMemberService.UpdateMemberStatus(mid, true);
                //if there no active users in this connection then remove this data from the database
                if (conn.Members.Where(x => x.isArchived == false).Count() <= 0)
                {
                    //remove data from database
                    ConnectionMemberService.RemoveMembersByConnectionID(cid);
                    ConnectionServices.RemoveConnection(cid);
                }
                return Json(new { success = true });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }
        #endregion
    }
}