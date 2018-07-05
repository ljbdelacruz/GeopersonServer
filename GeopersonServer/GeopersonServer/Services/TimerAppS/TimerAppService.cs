using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.TimerAppS
{
    public static class TimerAppService
    {
        public static bool Insert(Guid id, Guid oid, Guid api, DateTime ca, DateTime et, DateTime dt) {
            try {
                var model = TimerAppVM.set(id, oid, api, et, dt, ca);
                using (var context = new GeopersonContext()) {
                    context.TimerAppDB.Add(model);
                    context.SaveChanges();
                }
                return true;
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.TimerAppDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    context.TimerAppDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid oid, Guid api, DateTime et, DateTime dt) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.TimerAppDB where i.ID == id && i.OwnerID == oid && i.API == api select i).FirstOrDefault();
                    query.EnabledTime = et;
                    query.DisabledTime = dt;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<TimerApp> GetByOID(Guid oid, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.TimerAppDB where i.OwnerID == oid && i.API == api select i).ToList();
                return query;
            }
        }
        #region functions
        //determine if still accessible
        public static bool IsAccessible(Guid id, Guid api, DateTime timeNow)
        {
            try
            {
                using (var context = new GeopersonContext())
                {
                    var query = (from i in context.TimerAppDB where i.ID == id && i.API == api select i).FirstOrDefault();
                    if (query.EnabledTime.Year == timeNow.Year && query.EnabledTime.Month == timeNow.Month && query.EnabledTime.Day == timeNow.Day)
                    {
                        //check time 
                        if (timeNow.Hour >= query.EnabledTime.Hour && timeNow.Hour <= query.DisabledTime.Hour)
                        {
                            if (timeNow.Minute >= query.EnabledTime.Minute)
                            {
                                return true;
                            }
                        }

                    }
                    return false;
                }
            }
            catch { return false; }
        }
        //get seconds remaining 
        public static int CalculateRemainingTimeSeconds(Guid id, Guid api, DateTime timeNow) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.TimerAppDB where i.ID == id && i.API == api select i).FirstOrDefault();
                return 0;
            }
        }
        #endregion
    }
}