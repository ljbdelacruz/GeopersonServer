using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.TimerAppS
{
    public static class TimerAppLimiterService
    {
        public static bool Insert(Guid id, Guid srid, Guid oid, Guid api, int sec) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = TimerAppLimitersVM.set(id, srid, oid, api, sec);
                    context.TimerAppLimitersDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.TimerAppLimitersDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    context.TimerAppLimitersDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid srid, Guid oid, Guid api, int sec) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.TimerAppLimitersDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    query.StatusReferenceID = srid;
                    query.Seconds = sec;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<TimerAppLimiters> GetByOID(Guid oid, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.TimerAppLimitersDB where i.OwnerID == oid && i.API == api select i).ToList();
                return query;
            }
        }
        public static TimerAppLimiters GetByID(Guid id, Guid oid, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.TimerAppLimitersDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                return query;
            }
        }

    }
}