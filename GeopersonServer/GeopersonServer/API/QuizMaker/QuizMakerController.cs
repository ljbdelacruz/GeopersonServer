using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.QuestionMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.QuizMaker
{
    public class QuizMakerController : Controller
    {
        #region QuizInfo
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIInsert() {
            try {
                var id = Guid.NewGuid();
                var name = Request.Form["name"];
                var oid = Guid.Parse(Request.Form["oid"]);
                var appID = Guid.Parse(Request.Form["aid"]);
                var quizCode = Request.Form["qc"];
                var status = Guid.Parse(Request.Form["status"]);
                var htl = Boolean.Parse(Request.Form["htl"]);
                if (QuizInfoService.Insert(id, name, oid, appID, quizCode, status, Guid.Parse("7b3f5e5b-744a-4471-8392-5aa525627547"), htl)){
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIUpdateQuizStats() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var stats = Guid.Parse(Request.Form["stats"]);
                if (QuizInfoService.UpdateQuizStatus(id, oid, stats)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qc = Request.Form["qc"];
                var name = Request.Form["name"];
                if (QuizInfoService.Update(id, qc, name)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QIRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var appID = Guid.Parse(Request.Form["aid"]);
                if (QuizInfoService.Remove(id, oid, appID)) {
                    return Success("");
                }
                return Failed("");
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region Get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QIGetByQC(string id) {
            try {
                var data = QuizInfoService.GetByQC(id);
                return Success(QuizInfoVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QIGetID(string id) {
            try {
                var data = QuizInfoService.GetID(Guid.Parse(id));
                return Success(QuizInfoVM.MToVM(data));
            } catch { return null; }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QIGetByID(string id, string oid, string aid) {
            try {
                var data = QuizInfoService.GetByID(Guid.Parse(id), Guid.Parse(oid), Guid.Parse(aid));
                return Success(QuizInfoVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QIGetByOID(string oid, string api) {
            try {
                var data = QuizInfoService.GetByOID(Guid.Parse(oid), Guid.Parse(api));
                return Success(QuizInfoVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
        #region QuizQuestions
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var quest = Request.Form["quest"];
                var qi = Guid.Parse(Request.Form["qi"]);
                var order = int.Parse(Request.Form["order"]);
                var point = int.Parse(Request.Form["point"]);
                var status = Guid.Parse(Request.Form["status"]);
                if (QuizQuestionService.Insert(id, quest, qi, order, point, status)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());

            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQUpdate() {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var qi = Guid.Parse(Request.Form["qi"]);
                var question = Request.Form["quest"];
                var point = int.Parse(Request.Form["point"]);
                if (QuizQuestionService.Update(id, qi, question, point))
                {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                if (QuizQuestionService.Remove(id, qiid))
                {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region Get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QQGetByQuizInfo(string id) {
            try {
                var data = QuizQuestionService.GetByQuizInfo(Guid.Parse(id));
                return Success(QuizQuestionsVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
        #region QuizQuestionAnswer
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQAInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var desc = Request.Form["desc"];
                var isCorrect = Boolean.Parse(Request.Form["isCorrect"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                if (QuizQuestionAnswerService.Insert(id, desc, isCorrect, qqid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQAUpdate()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var desc = Request.Form["desc"];
                var isCorrect = Boolean.Parse(Request.Form["ic"]);
                if (QuizQuestionAnswerService.Update(id, desc, isCorrect)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QQARemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qqid = Guid.Parse(Request.Form["qqid"]);
                if (QuizQuestionAnswerService.Remove(id, qqid)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QQAGetByQQID(string id) {
            try {
                var data = QuizQuestionAnswerService.GetByQQID(Guid.Parse(id));
                return Success(QuizQuestionAnswerVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QQAGetByID(string id, string qqid) {
            try {
                var data = QuizQuestionAnswerService.GetByID(Guid.Parse(id), Guid.Parse(qqid));
                return Success(QuizQuestionAnswerVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
        #region QuizTakers
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var tp = int.Parse(Request.Form["tp"]);
                if (QuizTakersService.Insert(id, qiid, uid, DateTime.Now, tp)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTRemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                if (QuizTakersService.Remove(id, qiid)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QTUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var qiid = Guid.Parse(Request.Form["qiid"]);
                var tp = int.Parse(Request.Form["tp"]);
                if (QuizTakersService.Update(id, qiid, tp)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QTGetByID(string id) {
            try {
                var data = QuizTakersService.GetByID(Guid.Parse(id));
                return Success(QuizTakersVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QTGetByQuizInfoID(string id) {
            try {
                var data = QuizTakersService.GetByQuizInfoID(Guid.Parse(id));
                return Success(QuizTakersVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QTGetByUID(string id) {
            try {
                var data = QuizTakersService.GetByUID(Guid.Parse(id));
                return Success(QuizTakersVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QTGetByUIDQuizInfoID(string id, string qiid) {
            try {
                var data = QuizTakersService.GetByUIDQuizInfoID(Guid.Parse(id), Guid.Parse(qiid));
                return Success(QuizTakersVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
        #region QuizUserAnswer
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUAInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var questionTakerID = Guid.Parse(Request.Form["qtid"]);
                var questionQuizID = Guid.Parse(Request.Form["qqid"]);
                var quizAnswerID = Guid.Parse(Request.Form["qaid"]);
                var otherAnswer = Request.Form["oa"];
                var points = int.Parse(Request.Form["point"]);
                if (QuizUserAnswerService.Insert(id, questionTakerID, questionQuizID, quizAnswerID, otherAnswer, points)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUAInsertEss()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var questionTakerID = Guid.Parse(Request.Form["qtid"]);
                var questionQuizID = Guid.Parse(Request.Form["qqid"]);
                var quizAnswerID = Guid.NewGuid();
                var otherAnswer = Request.Form["oa"];
                var points = int.Parse(Request.Form["point"]);
                if (QuizUserAnswerService.Insert(id, questionTakerID, questionQuizID, quizAnswerID, otherAnswer, points))
                {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUAUpdate() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var quizTaker = Guid.Parse(Request.Form["qtid"]);
                var point = int.Parse(Request.Form["point"]);
                if (QuizUserAnswerService.Update(id, quizTaker, point)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> QUARemove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var quizTakerID = Guid.Parse(Request.Form["qtid"]);
                if (QuizUserAnswerService.Remove(id, quizTakerID)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region request Get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QUAGetByQTID(string id) {
            try {
                var data = QuizUserAnswerService.GetByQTID(Guid.Parse(id));
                return Success(QuizUserAnswerVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QUAGetByQQID(string id, string qqid) {
            try {
                var data = QuizUserAnswerService.GetByQTIDQQID(Guid.Parse(id), Guid.Parse(qqid));
                return Success(QuizUserAnswerVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> QUAGetByQQIDQAID(string id, string qid){
            try {
                var data = QuizUserAnswerService.GetByQuizQuestionIDQuizAnswerID(Guid.Parse(id), Guid.Parse(qid));
                return Success(QuizUserAnswerVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
        #region util
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}