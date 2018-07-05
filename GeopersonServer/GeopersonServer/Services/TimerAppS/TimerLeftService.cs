using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.TimerAppS
{
    public static class TimerLeftService
    {
        public static bool Insert(Guid id, Guid oid, Guid taid, Guid api, DateTime ca, DateTime ts, DateTime te) {
            try {
                var model = TimerLeftVM.set(id, oid, taid, api, ca, ts, te);
                using (var context = new GeopersonContext()) {
                    context.TimerLeftDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid taid, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.TimerLeftDB where i.ID == id && i.OwnerID == oid && i.TimerAppID == taid && i.API == api select i).FirstOrDefault();
                    context.TimerLeftDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<TimerLeft> GetByTAIDOID(Guid id, Guid oid, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.TimerLeftDB where i.TimerAppID == id && i.OwnerID == oid && i.API == api select i).ToList();
                return query;
            }
        }

    }
}