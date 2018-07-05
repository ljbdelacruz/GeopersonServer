using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeopersonServer.Services
{
    public static class RequestService
    {
        public static List<Request> GetByReceipentID(string ID) {
            using (var context = new GeopersonContext()) {
                var data = (from c in context.RequestDB
                            where c.RequestTo.ToString().Equals(ID) &&
                            c.isArchived==false
                            select new { c.ID, c.RequestTo, c.RequestFrom, c.ConnectionRequest}).ToList();
                var requests = new List<Request>();
                foreach (var model in data) {
                    requests.Add(new Request()
                    {
                        ID = model.ID,
                        RequestTo = model.RequestTo,
                        RequestFrom = model.RequestFrom,
                        ConnectionRequest = model.ConnectionRequest
                    });
                }
                return requests;
            }
        }
        public static Request GetByID(string ID) {
            using (var context = new GeopersonContext()) {
                var data = (from c in context.RequestDB where c.ID.ToString().Equals(ID) select c).FirstOrDefault();
                return data;
            }
        }
        public static Request GetByConnectionIDRTID(string CID,string RTID) {
            using (var context = new GeopersonContext()) {
                var data = (from r in context.RequestDB where r.ConnectionRequest.ID.ToString().Equals(CID) && r.RequestTo.ToString().Equals(RTID) select r).FirstOrDefault();
                return data;
            }
        }

        public static bool Insert(Guid ID, string rf, Guid rt, Guid connID, bool isArchived) {
            try {
                using (var context = new GeopersonContext())
                {
                    var model = new Request()
                    {
                        ID = ID,
                        ConnectionRequest = context.ConnectionsDB.Where(x=>x.ID==connID).FirstOrDefault(),
                        RequestFrom = rf,
                        RequestTo = rt,
                        isArchived = isArchived
                    };
                    context.RequestDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateStatus(bool isArchived, string ID) {
            using (var context = new GeopersonContext()) {
                var data = (from r in context.RequestDB where r.ID.ToString().Equals(ID) select r).FirstOrDefault();
                data.isArchived = isArchived;
                context.SaveChanges();
                return true;
            }
        }

    }
}