using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class ConnectionServices
    {
        public static Connections GetByID(string ID) {
            using (var context = new GeopersonContext()) {
                var data = (from c in context.ConnectionsDB where c.ID.ToString().Equals(ID) select new { c.ID, c.ConnectionName, c.CreatedAt, c.Members}).FirstOrDefault();
                return new Connections() {
                    ID=data.ID,
                    ConnectionName=data.ConnectionName,
                    CreatedAt=data.CreatedAt,
                    Members=data.Members
                };
            }
        }
        //gets the connections of that member ID
        public static List<Connections> GetByMemberSingleID(string MID) {
            using (var context = new GeopersonContext()) {
                var data = (from c in context.ConnectionsDB
                            where c.Members.Where(x => x.UserID.ToString().Equals(MID) && x.isArchived==false).ToList().Count > 0 && c.isGroup==true
                            select new { c.ID, c.ConnectionName, c.CreatedAt, c.Members }).ToList();
                var list = new List<Connections>();
                foreach (var model in data) {
                    list.Add(new Connections()
                    {
                        ID = model.ID,
                        ConnectionName=model.ConnectionName,
                        CreatedAt = model.CreatedAt,
                        Members = model.Members.Where(x=>x.isArchived==false).ToList()
                    });
                }
                return list;
            }
        }
        public static List<Connections> GetByMemberGroupID(string MID)
        {
            using (var context = new GeopersonContext())
            {
                var data = (from c in context.ConnectionsDB
                            where c.Members.Where(x => x.UserID.ToString().Equals(MID)).ToList().Count > 0 && c.isGroup==true
                            select new { c.ID, c.CreatedAt, c.Members }).ToList();
                var list = new List<Connections>();
                foreach (var model in data)
                {
                    list.Add(new Connections()
                    {
                        ID = model.ID,
                        CreatedAt = model.CreatedAt,
                        Members = model.Members
                    });
                }
                return list;
            }
        }
        //connectionMember
        public static ConnectionMember GetByUIDConnectionID(string UID, string connID)
        {
            using (var context = new GeopersonContext()) {
                var data = (from c in context.ConnectionMemberDB
                            where c.UserID.ToString().Equals(UID) && c.Connection.ID.ToString().Equals(connID)
                            select c).FirstOrDefault();
                return data;
            }
        }
        //insert connection
        public static bool Insert(Guid CID, bool isGroup, string cName, DateTime createdAt, Guid API) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = ConnectionViewModel.Set(CID, cName, createdAt, isGroup, API);
                    context.ConnectionsDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }


        public static bool RemoveConnection(string ID) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = (from gc in context.ConnectionsDB where ID.ToString().Equals(ID) select gc).FirstOrDefault();
                    context.ConnectionsDB.Remove(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false;}
        }
    }
}