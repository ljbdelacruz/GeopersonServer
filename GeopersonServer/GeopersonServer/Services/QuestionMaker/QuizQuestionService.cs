using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.QuestionMaker
{
    public static class QuizQuestionService
    {
        public static bool Insert(Guid id, string questions, Guid quizInfo, int order, int points, Guid status) {
            try {
                var data = QuizQuestionsVM.Set(id, questions, quizInfo, order, points, status);
                using (var context = new GeopersonContext()) {
                    context.QuizQuestionsDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid qi, string question, int points) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizQuestionsDB where i.ID == id && i.QuizInfo == qi select i).FirstOrDefault();
                    query.Questions = question;
                    query.Points = points;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid quizInfoID) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.QuizQuestionsDB where i.ID == id && i.QuizInfo == quizInfoID select i).FirstOrDefault();
                    context.QuizQuestionsDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<QuizQuestions> GetByQuizInfo(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.QuizQuestionsDB where i.QuizInfo == id orderby i.Order select i).ToList();
                return query;
            }
        }


    }
}