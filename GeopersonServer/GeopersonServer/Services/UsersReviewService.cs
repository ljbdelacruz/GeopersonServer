using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class UsersReviewService
    {
        public static UsersReview GetByID(Guid id, Guid API) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.UserReviewDB  where i.ID == id && i.API==API select i).FirstOrDefault();
                return query;
            }
        }
        public static List<UsersReview> GetByUserID(Guid id, Guid API) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.UserReviewDB where i.UserID == id && i.API==API orderby i.UpdatedAt descending select i).ToList();
                return query;
            }
        }
        public static bool Insert(Guid id, string comment, Guid userID, Guid senderID, Guid api, DateTime updatedAt, int stars) {
            try{
                using (var context = new GeopersonContext()) {
                    var model = UsersReviewVM.Set(id, comment, userID, senderID, api, updatedAt, stars);
                    context.UserReviewDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid aid) {
            try {
                using (var context = new GeopersonContext())
                {
                    var query = (from i in context.UserReviewDB where i.ID == id && i.API == aid select i).FirstOrDefault();
                    context.UserReviewDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}