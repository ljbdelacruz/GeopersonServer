using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class ConnectionMemberService
    {
        public static ConnectionMember GetByID(string ID) {
            using (var context = new GeopersonContext()) {
                var data = (from cm in context.ConnectionMemberDB where cm.ID.ToString().Equals(ID)
                            select new { cm.ID, cm.CreatedAt, cm.UserID, cm.Connection, cm.isArchived }).FirstOrDefault();
                return new ConnectionMember() {
                    ID=data.ID,
                    CreatedAt=data.CreatedAt,
                    UserID=data.UserID,
                    Connection=data.Connection,
                    isArchived=data.isArchived
                };
            }
        }
        public static List<ConnectionMember> GetByConnectionID(string CID) {
            using (var context = new GeopersonContext()) {
                var data = (from cm in context.ConnectionMemberDB
                            where cm.Connection.ID.ToString().Equals(CID)
                            select new { cm.ID, cm.CreatedAt, cm.UserID, cm.Connection, cm.isArchived }).ToList();
                var list = new List<ConnectionMember>();
                foreach (var model in data) {
                    list.Add(new ConnectionMember() {
                        ID = model.ID,
                        CreatedAt = model.CreatedAt,
                        UserID = model.UserID,
                        Connection = model.Connection,
                        isArchived = model.isArchived
                    });
                }
                return list;
            }
        }

        public static bool InsertMember(Guid ID, Guid UID, string ConnID, DateTime createdAt, bool isArchived, bool isAdmin)
        {
            try
            {
                using (var context = new GeopersonContext())
                {
                    var model = new ConnectionMember()
                    {
                        ID = ID,
                        UserID = UID,
                        Connection = context.ConnectionsDB.Where(x => x.ID.ToString().Equals(ConnID)).FirstOrDefault(),
                        CreatedAt = createdAt,
                        isArchived = isArchived,
                        isAdmin=isAdmin
                    };
                    context.ConnectionMemberDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
        public static bool UpdateMemberStatus(string ID, bool isArchived)
        {
            try
            {
                using (var context = new GeopersonContext())
                {
                    var data = (from c in context.ConnectionMemberDB where c.ID.ToString().Equals(ID) select c).FirstOrDefault();
                    data.isArchived = isArchived;
                    context.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public static bool RemoveMembersByConnectionID(string connID) {
            try {
                using (var context = new GeopersonContext()) {
                    var members = (from m in context.ConnectionMemberDB where m.Connection.ID.ToString().Equals(connID) select m).ToList();
                    foreach (var model in members) {
                        context.ConnectionMemberDB.Remove(model);
                    }
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}