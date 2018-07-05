using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.QuestionMaker
{
    public static class QuizTakersService
    {
        public static bool Insert(Guid id, Guid qiid, Guid uid, DateTime ca, int tp) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = QuizTakersVM.set(id, qiid, uid, ca, tp);
                    context.QuizTakersDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid qiid, int tp) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizTakersDB where i.ID == i.ID && i.QuizInfoID == qiid select i).FirstOrDefault();
                    query.TotalPoints = tp;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static bool Remove(Guid id, Guid qiid) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizTakersDB where i.ID == id && i.QuizInfoID == qiid select i).FirstOrDefault();
                    context.QuizTakersDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static QuizTakers GetByID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizTakersDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<QuizTakers> GetByQuizInfoID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizTakersDB where i.QuizInfoID == id select i).ToList();
                return query;
            }
        }
        public static List<QuizTakers> GetByUID(Guid uid) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizTakersDB where i.UserID == uid select i).ToList();
                return query;
            }
        }
        public static List<QuizTakers> GetByUIDQuizInfoID(Guid uid, Guid qiid) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizTakersDB where i.UserID == uid && i.QuizInfoID == qiid select i).ToList();
                return query;
            }
        }





    }
}