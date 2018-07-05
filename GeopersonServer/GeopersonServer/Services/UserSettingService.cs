using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class UserSettingService
    {
        public static UserSettings GetByUID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.UserSettingsDB where i.UserID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, Guid uid, Guid aid, DateTime JoinedOn) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = UserSettingsVM.Set(id, uid, aid, JoinedOn);
                    context.UserSettingsDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
    }
}