
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility.Strings;

namespace GeopersonServer.Models.Geoperson
{
    //must review and remove class that is not being used 
    #region Unsorted
    public class UserSettingsVM {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string AccessLevelID { get; set; }
        public string JoinedOn { get; set; }
        public static UserSettings Set(Guid id, Guid uid, Guid aid, DateTime JoinedOn) {
            try {
                return new UserSettings()
                {
                    ID=id,
                    UserID=uid,
                    AccessLevelID=aid,
                    JoinedOn=JoinedOn
                };
            } catch { return null; }
        }
        public static UserSettingsVM MToVM(UserSettings model){
            try
            {
                return new UserSettingsVM() {
                    ID = model.ID.ToString(),
                    UserID = model.UserID.ToString(),
                    AccessLevelID = model.AccessLevelID.ToString(),
                    JoinedOn = StringConverters.DateTimeToString(model.JoinedOn)
                };
            } catch { return null; }
        }
        public static List<UserSettingsVM> MsToVMs(List<UserSettings> models){
            var list = new List<UserSettingsVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
    }
    public class UserInformationViewModel {
        public string User { get; set; }
        public string Location { get; set; }
        public List<Connections> Connections { get; set; }
        public UserInformationViewModel() { }

        public static UserInformationViewModel MToVM(string userID, string location, List<Connections> Connections) {
            try
            {
                return new UserInformationViewModel() {
                    User = userID, Location = location, Connections = Connections
                };
            } catch { return null; }
        }

    }
    public class ConnectionViewModel {
        public string ID { get; set; }
        public string GroupName { get; set; }
        public List<ConnectionMemberViewModel> Members { get; set; }
        public ConnectionViewModel() {
            this.ID = "";
            this.Members = new List<ConnectionMemberViewModel>();
        }
        #region static methods
        public void PushMembers(ConnectionMember member, UserInformationViewModel user) {
            this.Members.Add(ConnectionMemberViewModel.MToVM(member, user));
        }
        public static Connections Set(Guid ID, string connName, DateTime createdAt, bool isGroup, Guid API) {
            try {
                return new Connections()
                {
                    ID = ID,
                    ConnectionName = connName,
                    CreatedAt = createdAt,
                    isGroup = isGroup,
                    API = API
                };
            } catch { return null; }
        }


        public static ConnectionViewModel MToVM(Connections model) {
            try {
                return new ConnectionViewModel() {
                    ID = model.ID.ToString(),
                    GroupName = model.ConnectionName,
                };
            } catch { return null; }
        }
        public static List<ConnectionViewModel> MsToVM(List<Connections> models) {
            var list = new List<ConnectionViewModel>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class ConnectionMemberViewModel
    {
        public string ID { get; set; }
        public UserInformationViewModel Member { get; set; }
        public bool isAdmin { get; set; }
        public static ConnectionMemberViewModel MToVM(ConnectionMember member, UserInformationViewModel user) {
            return new ConnectionMemberViewModel() {
                ID = member.ID.ToString(),
                Member = user,
                isAdmin = member.isAdmin
            };
        }
    }
    public class RequestViewModel {
        public string ID { get; set; }
        public string RequestFrom { get; set; }
        public string ConnectionID { get; set; }
        public static RequestViewModel MToVM(Request model) {
            try {
                return new RequestViewModel()
                {
                    ID = model.ID.ToString(),
                    RequestFrom = model.RequestFrom,
                    ConnectionID = model.ConnectionRequest != null ? model.ConnectionRequest.ID.ToString() : null
                };
            } catch { return null; }

        }
    }
    public class MeetupLocationViewModel : Position {
        public string ID { get; set; }
        public string Note { get; set; }
        public string UID { get; set; }
        public string connectionID { get; set; }
        public static MeetupLocationViewModel MToVM(MeetupLocation model) {
            return new MeetupLocationViewModel() {
                ID = model.ID.ToString(),
                longitude = model.longitude,
                latitude = model.latitude,
                Note = model.Note,
                UID = model.UpdatedBy.ToString(),
                connectionID = model.Connection != null ? model.Connection.ID.ToString() : null
            };
        }
    }
    #endregion
    #region buy and sell
    public class ItemsViewModel : Position {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string OwnerID { get; set; }
        public int PostType { get; set; }
        public string UpdatedAt { get; set; }
        public int TimesViewed { get; set; }
        //this will identify if item is sold or not
        public bool isArchived { get; set; }
        #region static methods
        public static Items Set(Guid id, string title, string description, float price, Guid OwnerID, Guid CategoryID, float longitude, float latitude, bool isArchived, Guid API, DateTime createdAt, DateTime updatedAt, int postType) {
            try {
                return new Geoperson.Items()
                {
                    ID = id,
                    Title = title,
                    Description = description,
                    OwnerID = OwnerID,
                    Price = price,
                    longitude = longitude,
                    latitude = latitude,
                    isArchived = isArchived,
                    API = API,
                    CreatedAt=createdAt,
                    UpdatedAt=updatedAt,
                    PostType=postType,
                    TimesViewed=0
                };
            } catch { return null; }
        }

        public static ItemsViewModel MToVM(Items model) {
            try {
                return new ItemsViewModel() {
                    ID = model.ID.ToString(),
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    OwnerID = model.OwnerID.ToString(),
                    longitude = model.longitude,
                    latitude = model.latitude,
                    PostType=model.PostType,
                    UpdatedAt= StringConverters.DateTimeToString(model.UpdatedAt),
                    TimesViewed=model.TimesViewed,
                    isArchived=model.isArchived
                };
            } catch { return null; }
        }
        public static List<ItemsViewModel> MsToVMs(List<Items> models) {
            var list = new List<ItemsViewModel>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class ItemsImagesVM {
        public string ID { get; set; }
        public string ItemID { get; set; }
        public string ImageLinkStorageID { get; set; }
        #region static methods
        public static ItemsImages set(Guid id, Guid iid, Guid ilid) {
            try {
                return new ItemsImages() {
                    ID=id,
                    ItemID=iid,
                    ImageLinkStorageID=ilid
                };
            } catch { return null; }
        }
        public static ItemsImagesVM MToVM(ItemsImages model) {
            try {
                return new ItemsImagesVM() {
                    ID=model.ID.ToString(),
                    ItemID=model.ItemID.ToString(),
                    ImageLinkStorageID=model.ImageLinkStorageID.ToString()
                };
            } catch { return null; }
        }
        public static List<ItemsImagesVM> MsToVMs(List<ItemsImages> models) {
            var list = new List<ItemsImagesVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    public class ItemImageVM {
        public string ID { get; set; }
        public string Source { get; set; }
        #region static methods
        public static ItemImage Set(Guid id, string source, Items item) {
            try {
                return new ItemImage() {
                    ID = id,
                    Source = source,
                    Item = item
                };
            } catch { return null; }
        }
        public static ItemImageVM MToVM(ItemImage model) {
            try {
                return new ItemImageVM() {
                    ID = model.ID.ToString(),
                    Source = model.Source
                };
            } catch { return null; }
        }
        public static List<ItemImageVM> MsToVMs(List<ItemImage> models) {
            var list = new List<ItemImageVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    public class ItemCategoryVM {
        public string ID { get; set; }
        public string Name { get; set; }
        #region static methods
        public static ItemCategory Set(Guid id, string Name) {
            try {
                return new ItemCategory() {
                    ID = id,
                    Name = Name
                };
            } catch { return null; }
        }
        public static ItemCategoryVM MToVM(ItemCategory model) {
            try {
                return new ItemCategoryVM() {
                    ID = model.ID.ToString(),
                    Name = model.Name
                };
            } catch { return null; }
        }
        public static List<ItemCategoryVM> MsToVMs(List<ItemCategory> models) {
            var list = new List<ItemCategoryVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class ItemSubCategoryVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        #region static methods
        public static ItemSubCategory Set(Guid id, string name, Guid catID, bool isArchived)
        {
            try
            {
                return new ItemSubCategory()
                {
                    ID = id,
                    Name = name,
                    Category = catID,
                    isArchived= isArchived,
                };
            }
            catch { return null; }
        }
        public static ItemSubCategoryVM MToVM(ItemSubCategory model) {
            try {
                return new ItemSubCategoryVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name,
                    Category=model.Category.ToString()
                };
            } catch { return null; }
        }
        public static List<ItemSubCategoryVM> MsToVMs(List<ItemSubCategory> models) {
            var list = new List<ItemSubCategoryVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
        #endregion
    }

    public class ItemAssignCategoryVM{
        public string ID { get; set; }
        public string ItemID { get; set; }
        public string CategoryID { get; set; }
        #region static methods
        public static ItemAssignCategory Set(Guid id, Guid itemID, Guid catID) {
            try {
                return new ItemAssignCategory() {
                    ID = id,
                    ItemID=itemID,
                    SubCategory=catID
                };
            } catch { return null; }
        }
        public static ItemAssignCategoryVM MToVM(ItemAssignCategory model) {
            try {
                return new ItemAssignCategoryVM() {
                    ID=model.ID.ToString(),
                    ItemID=model.ItemID.ToString(),
                    CategoryID=model.SubCategory.ToString()
                };
            } catch { return null; }
        }
        public static List<ItemAssignCategoryVM> MsToVMs(List<ItemAssignCategory> models) {
            var list = new List<ItemAssignCategoryVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    #region UserReview
    public class UsersReviewVM {
        public string ID { get; set; }
        public string Comment { get; set; }
        public string UserID { get; set; }
        public string SenderID { get; set; }
        public string UpdatedAt { get; set; }
        public int Stars { get; set; }
        public string API { get; set; }
        
        #region static methods
        public static UsersReview Set(Guid id, string comment, Guid uid, Guid senderID, Guid api, DateTime updateAt, int stars) {
            try {
                return new UsersReview() {
                    ID=id,
                    comment=comment,
                    UserID=uid,
                    SenderID=senderID,
                    API=api,
                    UpdatedAt=updateAt,
                    Stars=stars
                    
                };
            } catch { return null; }
        }
        public static UsersReviewVM MToVM(UsersReview model) {
            try {
                return new UsersReviewVM() {
                    ID=model.ID.ToString(),
                    Comment=model.comment,
                    UserID=model.UserID.ToString(),
                    SenderID=model.SenderID.ToString(),
                    UpdatedAt=StringConverters.DateTimeToString(model.UpdatedAt),
                    Stars=model.Stars
                };
            } catch { return null; }
        }
        public static List<UsersReviewVM> MsToVMs(List<UsersReview> models) {
            var list = new List<UsersReviewVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion

    }
    #endregion
    #region LocationTracking
    public class LocationSessionVM
    {
        public string ID { get; set; }
        public string Name { get; set; }
        #region static methods
        public static LocationSession Set(Guid id, string name, Guid api)
        {
            try
            {
                return new LocationSession()
                {
                    ID = id,
                    Name = name,
                    API = api
                };
            }
            catch { return null; }
        }
        public static LocationSessionVM MToVM(LocationSession model)
        {
            try
            {
                return new LocationSessionVM()
                {
                    ID = model.ID.ToString(),
                    Name = model.Name
                };
            }
            catch { return null; }
        }
        public static List<LocationSessionVM> MsToVMs(List<LocationSession> models)
        {
            var list = new List<LocationSessionVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class LocationSessionParticipantsVM
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string LocationSessionID { get; set; }
        public bool isAdmin { get; set; }
        #region static methods
        public static LocationSessionParticipants Set(Guid id, Guid uid, Guid lsID, bool isAdmin)
        {
            try
            {
                return new LocationSessionParticipants()
                {
                    ID = id,
                    UserID = uid,
                    LocationSessionID = lsID,
                    isAdmin = isAdmin
                };
            }
            catch { return null; }
        }
        public static LocationSessionParticipantsVM MToVM(LocationSessionParticipants model)
        {
            try
            {
                return new LocationSessionParticipantsVM()
                {
                    ID = model.ID.ToString(),
                    UserID = model.UserID.ToString(),
                    LocationSessionID = model.LocationSessionID.ToString(),
                    isAdmin = model.isAdmin
                };
            }
            catch { return null; }
        }
        public static List<LocationSessionParticipantsVM> MsToVMs(List<LocationSessionParticipants> models)
        {
            var list = new List<LocationSessionParticipantsVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class LocationSessionInvitationVM
    {
        public string ID { get; set; }
        public string LocationSessionID { get; set; }
        public string UserID { get; set; }
        #region static methods
        public static LocationSessionInvitation Set(Guid id, Guid lsID, Guid uid, Guid api)
        {
            try
            {
                return new LocationSessionInvitation()
                {
                    ID = id,
                    LocationSessionID = lsID,
                    UserID = uid
                };
            }
            catch { return null; }
        }
        public static LocationSessionInvitationVM MToVM(LocationSessionInvitation model)
        {
            try
            {
                return new LocationSessionInvitationVM()
                {
                    ID = model.ID.ToString(),
                    LocationSessionID = model.LocationSessionID.ToString(),
                    UserID = model.UserID.ToString(),

                };
            }
            catch { return null; }
        }
        public static List<LocationSessionInvitationVM> MsToVMs(List<LocationSessionInvitation> models)
        {
            var list = new List<LocationSessionInvitationVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class LocationStorageVM
    {
        public string ID { get; set; }
        public string OwnerID { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public string Description { get; set; }
        #region static methods
        public static LocationStorage Set(Guid id, string oid, Guid api, float longitude, float latitude, string lc, string desc)
        {
            try
            {
                return new LocationStorage()
                {
                    ID = id,
                    OwnerID = oid,
                    API = api,
                    Longitude = longitude,
                    Latitude = latitude,
                    LocationCategory = lc,
                    Description=desc
                };
            }
            catch { return null; }
        }
        public static LocationStorageVM MToVM(LocationStorage model)
        {
            try
            {
                return new LocationStorageVM()
                {
                    ID = model.ID.ToString(),
                    OwnerID = model.OwnerID,
                    longitude = model.Longitude,
                    latitude = model.Latitude,
                    Description=model.Description
                };
            }
            catch { return null; }
        }
        public static List<LocationStorageVM> MsToVMs(List<LocationStorage> models)
        {
            var list = new List<LocationStorageVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region Inventory System
    public class IS_ItemVM {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ItemCategoryID { get; set; }
        public bool isCount { get; set; }
        public int Quantity { get; set; }
        #region static methods
        public static IS_Item Set(Guid id, string title, string description, Guid api, Guid itemCategoryID, bool isCount, int quantity, Guid storeAPI) {
            try {
                return new IS_Item() {
                    ID=id,
                    Title=title,
                    Description=description,
                    API=api,
                    IS_ItemCategoryID=itemCategoryID,
                    isCount=isCount,
                    Quantity=quantity,
                    StoreAPI=storeAPI
                };
            } catch { return null; }
        }
        public static IS_ItemVM MToVM(IS_Item model) {
            try {
                return new IS_ItemVM() {
                    ID=model.ID.ToString(),
                    Title=model.Title,
                    Description=model.Description,
                    ItemCategoryID=model.IS_ItemCategoryID.ToString(),
                    isCount=model.isCount,
                    Quantity=model.Quantity
                };
            } catch { return null; }
        }
        public static List<IS_ItemVM> MsToVMs(List<IS_Item> models) {
            var list = new List<IS_ItemVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class IS_ItemImagesVM{
        public string ID { get; set; }
        public string Source { get; set; }
        #region static methods
        public static IS_ItemImages Set(Guid id, string source, Guid itemID) {
            try {
                return new IS_ItemImages(){
                    ID=id,
                    Source=source,
                    IS_ItemID=itemID
                };
            } catch {
                return null;
            }
        }
        public static IS_ItemImagesVM MToVM(IS_ItemImages model){
            try {
                return new IS_ItemImagesVM() {
                    ID = model.ID.ToString(),
                    Source = model.Source
                };
            } catch { return null; }
        }
        public static List<IS_ItemImagesVM> MsToVMs(List<IS_ItemImages> models) {
            var list = new List<IS_ItemImagesVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class IS_ItemCategoryVM {
        public string ID { get; set; }
        public string Name { get; set; }
        #region static methods
        public static IS_ItemCategory Set(Guid id, string Name, Guid api) {
            try {
                return new IS_ItemCategory() {
                    ID=id,
                    Name=Name,
                    API=api
                };
            } catch { return null; }
        }
        public static IS_ItemCategoryVM MToVM(IS_ItemCategory model) {
            try {
                return new IS_ItemCategoryVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name
                };
            } catch { return null; }
        }
        public static List<IS_ItemCategoryVM> MsToVMs(List<IS_ItemCategory> models) {
            var list = new List<IS_ItemCategoryVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class IS_ItemStockVM {
        public string ID { get; set; }
        public string BarcodeNumber { get; set; }
        #region static methods
        public static IS_ItemStock set(Guid id, string bcode, Guid itemID, Guid status) {
            try {
                return new IS_ItemStock() {
                    ID=id,
                    BarcodeNumber=bcode,
                    IS_ItemID=itemID,
                    IS_ItemStockStatus=status
                };
            } catch { return null; }
        }
        public static IS_ItemStockVM MToVM(IS_ItemStock model) {
            try {
                return new IS_ItemStockVM() {
                    ID=model.ID.ToString(),
                    BarcodeNumber=model.BarcodeNumber
                };
            } catch { return null; }
        }
        public static List<IS_ItemStockVM> MsToVMs(List<IS_ItemStock> models) {
            var list = new List<IS_ItemStockVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region StatusReferences
    public class StatusTypesReferencesVM {
        public string Name { get; set; }
        public string Description { get; set; }
        #region static methods
        public static StatusTypesReferences Set(Guid id, string name, string description) {
            try {
                return new StatusTypesReferences() {
                    ID=id,
                    Name=name,
                    Description=description
                };
            } catch { return null; }
        }
        public static StatusTypesReferencesVM MToVM(StatusTypesReferences model) {
            try {
                return new StatusTypesReferencesVM() {
                    Name=model.Name,
                    Description=model.Description
                };
            } catch { return null; }
        }
        public static List<StatusTypesReferencesVM> MsToVMs(List<StatusTypesReferences> models) {
            var list = new List<StatusTypesReferencesVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region QuestionMaker
    public class QuizInfoVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string QuizCode { get; set; }
        public string Status { get; set; }
        public string QuizStatus { get; set; }
        #region static methods
        public static QuizInfo Set(Guid id, string name, Guid oid, Guid appID, string quizCode, Guid status, Guid qs, bool hasTimeLimit) {
            try {
                return new QuizInfo() {
                    ID=id,
                    Name=name,
                    OwnerID=oid,
                    ApplicationID=appID,
                    QuizCode =quizCode,
                    Status=status,
                    QuizStatus=qs,
                    hasTimeLimit=hasTimeLimit,
                };
            } catch { return null; }
        }
        public static QuizInfoVM MToVM(QuizInfo model) {
            try {
                return new QuizInfoVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name,
                    QuizCode=model.QuizCode,
                    Status=model.Status.ToString(),
                    QuizStatus=model.QuizStatus.ToString()
                };
            } catch { return null; }
        }
        public static List<QuizInfoVM> MsToVMs(List<QuizInfo> models) {
            var list = new List<QuizInfoVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class QuizQuestionsVM {
        public string ID { get; set; }
        public string Questions { get; set; }
        public int Points { get; set; }
        public int Order { get; set; }
        //this will determine if this question is either essay or multiple choice
        public string Status { get; set; }
        #region static methods
        public static QuizQuestions Set(Guid id, string questions, Guid quizInfo, int order, int points, Guid status) {
            try {
                return new QuizQuestions() {
                    ID = id,
                    Questions = questions,
                    QuizInfo = quizInfo,
                    Order = order,
                    Points=points,
                    Status=status
                };
            } catch { return null; }
        }
        public static QuizQuestionsVM MToVM(QuizQuestions model) {
            try {
                return new QuizQuestionsVM() {
                    ID=model.ID.ToString(),
                    Questions=model.Questions,
                    Points=model.Points,
                    Order=model.Order,
                    Status=model.Status.ToString()
                };
            } catch { return null; }
        }
        public static List<QuizQuestionsVM> MsToVMs(List<QuizQuestions> models) {
            var list = new List<QuizQuestionsVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    #region Multiple Choice
    public class QuizQuestionAnswerVM {
        public string ID { get; set; }
        public string Description { get; set; }
        public bool isCorrect { get; set; }
        #region static methods
        public static QuizQuestionAnswer set(Guid id, string description, bool isCorrect, Guid quizQuestionID) {
            try {
                return new QuizQuestionAnswer() {
                    ID=id,
                    Description=description,
                    isCorrect=isCorrect,
                    QuizQuestionsID=quizQuestionID
                };
            } catch { return null; }
        }
        public static QuizQuestionAnswerVM MToVM(QuizQuestionAnswer model) {
            try {
                return new QuizQuestionAnswerVM() {
                    ID=model.ID.ToString(),
                    Description=model.Description,
                    isCorrect=model.isCorrect
                };
            } catch { return null; }
        }
        public static List<QuizQuestionAnswerVM> MsToVMs(List<QuizQuestionAnswer> models) {
            var list = new List<QuizQuestionAnswerVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region UserAnswers
    public class QuizTakersVM {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string CreatedAt { get; set; }
        public string QuizInfoID { get; set; }
        public int TotalPoints { get; set; }
        #region static methods
        public static QuizTakers set(Guid id, Guid quizInfoID, Guid uid, DateTime time, int tp) {
            try {
                return new QuizTakers() {
                    ID=id,
                    QuizInfoID=quizInfoID,
                    UserID=uid,
                    CreatedAt=time,
                    TotalPoints=tp
                };
            } catch { return null; }
        }
        public static QuizTakersVM MToVM(QuizTakers model) {
            try {
                return new QuizTakersVM(){
                    ID=model.ID.ToString(),
                    UserID=model.UserID.ToString(),
                    CreatedAt=Utility.Strings.StringConverters.DateTimeToString(model.CreatedAt),
                    TotalPoints=model.TotalPoints,
                    QuizInfoID=model.QuizInfoID.ToString(),
                };
            } catch { return null; }
        }
        public static List<QuizTakersVM> MsToVMs(List<QuizTakers> models) {
            var list = new List<QuizTakersVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class QuizUserAnswerVM {
        public string ID { get; set; }
        public string QuizQuestionID { get; set; }
        public string QuizAnswerID{ get; set; }
        public string OtherAnswer { get; set; }
        public int Points { get; set; }
        #region static methods
        public static QuizUserAnswer set(Guid id, Guid qtid, Guid quizQID, Guid quizAID, string otherAnswer, int points) {
            try {
                return new QuizUserAnswer() {
                    ID=id,
                    QuizTakersID= qtid,
                    QuizQuestionID =quizQID,
                    QuizAnswerID=quizAID,
                    OtherAnswer=otherAnswer,
                    Points=points
                };
            } catch { return null; }
        }
        public static QuizUserAnswerVM MToVM(QuizUserAnswer model) {
            try {
                return new QuizUserAnswerVM() {
                    ID=model.ID.ToString(),
                    QuizAnswerID=model.QuizAnswerID.ToString(),
                    QuizQuestionID=model.QuizQuestionID.ToString(),
                    OtherAnswer=model.OtherAnswer,
                    Points=model.Points
                };
            } catch { return null; }
        }
        public static List<QuizUserAnswerVM> MsToVMs(List<QuizUserAnswer> models) {
            var list = new List<QuizUserAnswerVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #endregion
    #region timerApp
    public class TimerAppVM {
        public string ID { get; set; }
        public string OwnerID { get; set; }
        public DateTime CreatedAt { get; set; }
        #region static methods
        public static TimerApp set(Guid id, Guid oid, Guid api, DateTime etime, DateTime dtime, DateTime ca) {
            try {
                return new TimerApp() {
                    ID=id,
                    OwnerID=oid,
                    API=api,
                    EnabledTime=etime,
                    DisabledTime=dtime,
                    CreatedAt=ca
                };
            } catch { return null; }
        }
        public static TimerAppVM MToVM(TimerApp model) {
            try {
                return new TimerAppVM() {
                    ID=model.ID.ToString(),
                    OwnerID=model.OwnerID.ToString(),
                    CreatedAt=model.CreatedAt,
                };
            } catch { return null; }
        }
        public static List<TimerAppVM> MsToVMs(List<TimerApp> models) {
            var list = new List<TimerAppVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class TimerAppLimitersVM {
        public string ID { get; set; }
        public string StatusReferenceID { get; set; }
        public int Seconds { get; set; }
        #region static methods
        public static TimerAppLimiters set(Guid id, Guid srid, Guid oid, Guid api, int seconds) {
            try {
                return new TimerAppLimiters() {
                    ID=id,
                    StatusReferenceID=srid,
                    OwnerID=oid,
                    API=api,
                    Seconds=seconds
                };
            } catch { return null; }
        }
        public static TimerAppLimitersVM MToVM(TimerAppLimiters model) {
            try {
                return new TimerAppLimitersVM() {
                    ID=model.ID.ToString(),
                    StatusReferenceID=model.StatusReferenceID.ToString(),
                    Seconds=model.Seconds
                };
            } catch { return null; }
        }
        public static List<TimerAppLimitersVM> MsToVMs(List<TimerAppLimiters> models) {
            var list = new List<TimerAppLimitersVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    public class TimerLeftVM {
        public string ID { get; set; }
        public string TimerAppID { get; set; }
        public DateTime CreatedAt { get; set; }
        #region static methods
        public static TimerLeft set(Guid id, Guid oid, Guid taid, Guid api, DateTime ca, DateTime ts, DateTime te) {
            try {
                return new TimerLeft() {
                    ID=id,
                    OwnerID=oid,
                    TimerAppID=taid,
                    API=api,
                    CreatedAt=ca,
                    TimeStarted=ts,
                    TimeEnded=te
                };
            } catch { return null; }
        }
        public static TimerLeftVM MToVM(TimerLeft model) {
            try {
                return new TimerLeftVM() {
                    ID=model.ID.ToString(),
                    TimerAppID=model.TimerAppID.ToString(),
                    CreatedAt=model.CreatedAt,
                };
            } catch { return null; }
        }
        public static List<TimerLeftVM> MsToVMs(List<TimerLeft> models) {
            var list = new List<TimerLeftVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion


}