using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.QuestionMaker
{
    public static class QuizUserAnswerService
    {
        public static bool Insert(Guid id, Guid qtid, Guid qqid, Guid qaid, string otherAnswer, int points) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = QuizUserAnswerVM.set(id, qtid, qqid, qaid, otherAnswer, points);
                    context.QuizUserAnswerDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid qtid, int point) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizUserAnswerDB where i.ID == id && i.QuizTakersID == qtid select i).FirstOrDefault();
                    query.Points = point;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid qtid) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizUserAnswerDB where i.ID == id && i.QuizTakersID == qtid select i).FirstOrDefault();
                    context.QuizUserAnswerDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<QuizUserAnswer> GetByQTID(Guid qtid) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizUserAnswerDB where i.QuizTakersID == qtid select i).ToList();
                return query;
            }
        }
        public static QuizUserAnswer GetByQTIDQQID(Guid qtid, Guid qqid) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizUserAnswerDB where i.QuizTakersID == qtid && i.QuizQuestionID == qqid select i).FirstOrDefault();
                return query;
            }
        }
        public static List<QuizUserAnswer> GetByQuizQuestionIDQuizAnswerID(Guid qqid, Guid qaid) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizUserAnswerDB where i.QuizQuestionID == qqid && i.QuizAnswerID == qaid select i).ToList();
                return query;
            }
        }


    }
}