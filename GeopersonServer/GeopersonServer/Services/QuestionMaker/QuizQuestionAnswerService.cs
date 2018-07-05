using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.QuestionMaker
{
    public static class QuizQuestionAnswerService
    {
        public static bool Insert(Guid id, string description, bool isCorrect, Guid qqid) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = QuizQuestionAnswerVM.set(id, description, isCorrect, qqid);
                    context.QuizQuestionAnswerDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string desc, bool isCorrect) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizQuestionAnswerDB where i.ID == id select i).FirstOrDefault();
                    query.Description = desc;
                    query.isCorrect = isCorrect;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid qqid) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizQuestionAnswerDB where i.ID == id && i.QuizQuestionsID == qqid select i).FirstOrDefault();
                    context.QuizQuestionAnswerDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<QuizQuestionAnswer> GetByQQID(Guid qqid) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizQuestionAnswerDB where i.QuizQuestionsID == qqid select i).ToList();
                return query;
            }
        }
        public static QuizQuestionAnswer GetByID(Guid id, Guid qqid) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizQuestionAnswerDB where i.ID == id && i.QuizQuestionsID == qqid select i).FirstOrDefault();
                return query;
            }
        }

    }
}