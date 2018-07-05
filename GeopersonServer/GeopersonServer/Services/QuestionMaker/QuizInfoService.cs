using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.QuestionMaker
{
    public static class QuizInfoService
    {
        public static bool Insert(Guid id, string name, Guid oid, Guid appID, string quizCode, Guid status, Guid qs, bool hasTimeLimit){
            try {
                var data = QuizInfoVM.Set(id, name, oid, appID, quizCode, status, qs, hasTimeLimit);
                using (var context = new GeopersonContext()){
                    context.QuizInfoDB.Add(data);
                    context.SaveChanges();
                    return true;        
                }
            } catch { return false; }
        }
        public static bool UpdateQuizStatus(Guid id, Guid oid, Guid status) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizInfoDB where i.ID == id && i.OwnerID == oid select i).FirstOrDefault();
                    query.QuizStatus = status;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string qc, string name) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizInfoDB where i.ID == id select i).FirstOrDefault();
                    query.Name = name;
                    query.QuizCode = qc;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid oid, Guid appID) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizInfoDB where i.ID == id && i.OwnerID == oid && i.ApplicationID == appID select i).FirstOrDefault();
                    context.QuizInfoDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static QuizInfo GetID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizInfoDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static QuizInfo GetByID(Guid id, Guid oid, Guid appID) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizInfoDB where i.ID == id && i.OwnerID == oid && i.ApplicationID == appID select i).FirstOrDefault();
                return query;
            }
        }
        public static List<QuizInfo> GetByOID(Guid oid, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizInfoDB where i.OwnerID == oid && i.ApplicationID == api select i).ToList();
                return query;
            }
        }
        public static QuizInfo GetByQC(string qc) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizInfoDB where i.QuizCode.Equals(qc) select i).FirstOrDefault();
                return query;
            }
        }
    }
}