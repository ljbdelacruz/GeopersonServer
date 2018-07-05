
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeopersonServer.Models.Geoperson
{
    #region Unassigned Use
    public class APIAccess {
        public Guid ID { get; set; }
        public Guid CompanyID { get; set; }
    }
    public class UserSettings
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string ProfileImage { get; set; }
        public Guid AccessLevelID { get; set; }
        public DateTime JoinedOn { get; set; }
    }
    public class Connections
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string ConnectionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ConnectionMember> Members { get; set; }
        //this one identifies if this connection is group or not
        public bool isGroup { get; set; }
        public Guid API { get; set; }
    }
    public class ConnectionMember
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public virtual Connections Connection { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isArchived { get; set; }
        public bool isAdmin { get; set; }
    }
    #endregion
    #region Request
    public class Request {
        public Guid ID { get; set; }
        //contains the name of the sender of the request
        //if its a user firstname etc... if it is a group then name of the group should be the data here
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string RequestFrom { get; set; }
        //RequestID of the receipent
        public Guid RequestTo { get; set; }
        public virtual Connections ConnectionRequest { get; set; }
        public bool isArchived { get; set; }
    }
    #endregion
    #region MeetupLocation
    public class Position {
        public float longitude { get; set; }
        public float latitude { get; set; }
    }
    public class MeetupLocation : Position
    {
        public Guid ID { get; set; }
        public Connections Connection { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Note { get; set; }
        //userID
        public Guid UpdatedBy { get; set; }
        public bool isEnabled { get; set; }
    }
    #endregion
    #region Recommended Places
    public class UsersReview {
        public Guid ID { get; set; }
        public string comment { get; set; }
        //review stars
        public int Stars { get; set; }
        public Guid UserID { get; set; }
        public Guid SenderID { get; set; }
        public Guid API { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    #endregion
    #region BuyAndSell
    public class Items : Position {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public float Price { get; set; }
        public Guid OwnerID { get; set; }
        public bool isArchived { get; set; }
        //records the location of where this ad is created
        public Guid API { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //1-Selling, 2-Looking, 3-For Trade
        public int PostType { get; set; }
        public int TimesViewed { get; set; }

    }
    public class ItemsImages {
        public Guid ID { get; set; }
        public Guid ItemID { get; set; }
        public Guid ImageLinkStorageID { get; set; }
    }

    public class ItemImage {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Source { get; set; }
        public virtual Items Item { get; set; }
    }

    // gadgets/food/Applliances
    public class ItemCategory {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        public bool isArchived { get; set; }
    }
    public class ItemSubCategory {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        public Guid Category { get; set; }
        public bool isArchived { get; set; }
    }
    public class ItemAssignCategory {
        public Guid ID { get; set; }
        public Guid ItemID { get; set; }
        //sub categoryID
        public Guid SubCategory { get; set; }
    }

    #endregion
    #region Seller Review
    public class SellerReview {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Comment { get; set; }
        public int review { get; set; }
    }
    #endregion
    #region Location Tracking
    public class LocationSession {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Name { get; set; }
        public Guid API { get; set; }
    }
    public class LocationSessionParticipants {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid LocationSessionID { get; set; }
        public bool isAdmin { get; set; }
    }
    public class LocationSessionInvitation {
        public Guid ID { get; set; }
        public Guid LocationSessionID { get; set; }
        public Guid UserID { get; set; }
        public Guid API { get; set; }
    }
    public class LocationStorage {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string OwnerID { get; set; }
        public Guid API { get; set; }
        //can be users or items and etc
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string LocationCategory { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        //store anything in here
        //either nearest city etc data, ex. Davao City, General Santos City
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
    }
    #endregion
    #region Inventory System
    public class IS_ItemCategory {
        public Guid ID { get; set; }
        public string Name { get; set; }
        //this will determine where this data comes from
        public Guid API { get; set; }
    }
    public class IS_Item {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //this api refers to application api
        public Guid API { get; set; }
        //this one is for which store this item belongs to
        public Guid StoreAPI { get; set; }
        public Guid IS_ItemCategoryID { get; set; }
        //2 types of items with serial or just countable
        //if its just count then it does not need particular identifier to each item ex. eggs, foods etc
        //serial are items with specific identity means each item can be identified using its serial or barcode number
        public bool isCount { get; set; }
        public int Quantity { get; set; }
    }
    public class IS_ItemImages {
        public Guid ID { get; set; }
        public string Source { get; set; }
        public Guid IS_ItemID { get; set; }
    }
    //this will determine the stock of this item
    //will either contain the SKU
    public class IS_ItemStock {
        public Guid ID { get; set; }
        public string BarcodeNumber { get; set; }
        //this will determine which item this belongs to
        public Guid IS_ItemID { get; set; }
        public Guid IS_ItemStockStatus { get; set; }
    }
    #endregion
    #region Status Types
    //client order statuses
    //on hold, on process, complete
    //delivered, preordered, backordered, delayed
    //cancelled
    //status of all client order tables are stored
    //stock status for 

    public class StatusTypesReferences {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
    }
    #endregion
    #region OrderingSystem
    public class ClientOrderDeliveryPerson
    {
        public Guid ID { get; set; }
        public Guid ClientOrderID { get; set; }
        //userID
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string DeliveryPersonID { get; set; }
        public Guid CODeliveryStatusID { get; set; }

    }
    public class ClientOrder
    {
        public Guid ID { get; set; }
        public Guid ApplicationID { get; set; }
        //this will determine which store this client order belongs
        public Guid StoreID { get; set; }
        public Guid UserID { get; set; }
        public Guid ClientOrderStatusID { get; set; }
    }
    public class ClientOrderItems
    {
        public Guid ID { get; set; }
        public Guid ClientOrderID { get; set; }
        public Guid IS_ItemStockID { get; set; }
        public Guid ClientOrderItemStatusID { get; set; }
    }
    #endregion
    #region QuizMaker
    public class QuizInfo{
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        //states owner of this Question
        public Guid OwnerID { get; set; }
        public Guid ApplicationID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string QuizCode { get; set; }
        public bool hasTimeLimit { get; set; }
        //Questionaire, Survey
        public Guid Status { get; set; }
        public Guid QuizStatus { get; set; }
    }
    public class QuizQuestions{
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Questions { get; set; }
        public Guid QuizInfo { get; set; }
        public int Order { get; set; }
        public int Points { get; set; }
        //identifies if this question is wether a normal question or bonus question
        public Guid Status { get; set; }
    }
    #region Multiple choices 
    public class QuizQuestionAnswer{
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public bool isCorrect { get; set; }
        public Guid QuizQuestionsID { get; set; }
    }
    #endregion
    #region UserAnswers
    public class QuizTakers {
        public Guid ID { get; set; }
        public Guid QuizInfoID { get; set; }
        public Guid UserID { get; set; }
        public int TotalPoints { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class QuizUserAnswer{
        public Guid ID { get; set; }
        public Guid QuizTakersID { get; set; }
        public Guid QuizQuestionID { get; set; }
        public Guid QuizAnswerID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string OtherAnswer { get; set; }
        //this refers to points earned for answering this question
        public int Points { get; set; }
    }
    #endregion
    #endregion
    #region RequestingSystem
    public class RS_Request {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string OtherInformation { get; set; }
        public Guid UserID { get; set; }
        public Guid AppID { get; set; }
    }
    #endregion
    #region DeliverySystem
    public class DeliveryPerson
    {
        public Guid ID { get; set; }
        public Guid DriverID { get; set; }
        public Guid BranchID { get; set; }
        //determines if order is complete, incomplete, on process, on the way, etc
        public Guid StatusID { get; set; }
    }
    public class DeliveryPersonItems
    {
        public Guid ID { get; set; }
        public Guid ItemID { get; set; }
        public Guid DeliveryPersonID { get; set; }
        //determines if the item is marked delivered, available or out of stock
        public Guid StatusID { get; set; }
    }


    #endregion
    #region DeliverySystem
    public class DS_ClientOrder{
        public Guid ID { get; set; }
        public Guid StoreRequestID { get; set; }
        public Guid DeliveryPersonID { get; set; }
        public Guid API { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid DeliveryLocationID { get; set; }
        //complete, on the way, on process, etc
        public Guid Status { get; set; }
        public string TrackingCode { get; set; }
    }
    public class DS_ClientOrderItems{
        public Guid ID { get; set; }
        public Guid ItemID { get; set; }
        public int Quantity { get; set; }
        public Guid DS_ClientOrderID { get; set; }
        //this can either be process, cancelled, on-hold 
        public Guid Status { get; set; }
    }
    #endregion
    #region TimerApp
    //this will deal anything related to timers, quiz timers anything related to timers 
    //if you want to get the time allotted on the specific stuff then get data here 
    public class TimerApp {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        //this will determine enable time, disabled time 
        public DateTime EnabledTime { get; set; }
        public DateTime DisabledTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    //this is where you will store the time limit
    //ex. Lunch breaks 15 mins except it store seconds and convert into minutes and hours
    public class TimerAppLimiters {
        public Guid ID { get; set; }
        //description is stored in status type reference
        //lunch breaks, breaks, etc, anything with limit its up to the user
        public Guid StatusReferenceID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public int Seconds { get; set; }
    }
    //contains the time left based on the timerApp
    //if you want to get the time remaining then get data here
    //basically its purpose is to store time started and time ended
    public class TimerLeft {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid TimerAppID { get; set; }
        public Guid API { get; set; }
        public DateTime CreatedAt { get; set; }
        //this will record the time started the test and time ended
        public DateTime TimeStarted { get; set; }
        public DateTime TimeEnded { get; set; }
    }
    #endregion

}