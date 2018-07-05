using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeopersonServer.Context
{
    public class GeopersonContext : DbContext
    {

        public GeopersonContext() : base("name=GeopersonContext") { }
        #region unsorted
        public DbSet<APIAccess> APIAccessDB { get; set; }
        public DbSet<UserSettings> UserSettingsDB { get; set; }
        public DbSet<Connections> ConnectionsDB { get; set; }
        public DbSet<ConnectionMember> ConnectionMemberDB { get; set; }
        public DbSet<Request> RequestDB { get; set; }
        public DbSet<MeetupLocation> MeetupLocationDB { get; set; }
        #endregion
        #region Buy and sell features
        public DbSet<Items> ItemsDB { get; set; }
        public DbSet<ItemsImages> ItemsImagesDB { get; set; }
        public DbSet<ItemImage> ItemImageDB { get; set; }
        public DbSet<ItemCategory> ItemCategoryDB { get; set; }
        public DbSet<ItemSubCategory> ItemSubCategoryDB { get; set; }
        public DbSet<ItemAssignCategory> ItemAssignCategoryDB { get; set; }
        public DbSet<UsersReview> UserReviewDB { get; set; }
        #endregion
        #region LocationTracking
        public DbSet<LocationSession> LocationSessionDB { get; set; }
        public DbSet<LocationSessionParticipants> LocationSessionParticipantsDB { get; set; }
        public DbSet<LocationSessionInvitation> LocationSessionInvitationDB { get; set; }
        public DbSet<LocationStorage> LocationStorageDB { get; set; }
        #endregion
        #region InventorySystem
        public DbSet<IS_Item> IS_ItemDB { get; set; }
        public DbSet<IS_ItemCategory> IS_ItemCategoryDB { get; set; }
        public DbSet<IS_ItemImages> IS_ItemImagesDB { get; set; }
        public DbSet<IS_ItemStock> IS_ItemStockDB { get; set; }
        #endregion
        #region StatusTypeReference
        public DbSet<StatusTypesReferences> StatusTypesReferencesDB { get; set; }
        #endregion
        #region ClientOrder
        public class ClientOrderDeliveryPersonVM {
            public string ID { get; set; }
            public string ClientOrderID { get; set; }
            public string DeliveryPersonID { get; set; }
            public string CODeliveryStatusID { get; set; }
            #region static methods
            public static ClientOrderDeliveryPerson Set(Guid id, Guid coid, string dpid, Guid codsid) {
                try {
                    return new ClientOrderDeliveryPerson() {
                        ID = id,
                        ClientOrderID = coid,
                        DeliveryPersonID = dpid,
                        CODeliveryStatusID = codsid
                    };
                } catch { return null; }
            }
            public static ClientOrderDeliveryPersonVM MToVM(ClientOrderDeliveryPerson model) {
                try {
                    return new ClientOrderDeliveryPersonVM() {
                        ID = model.ID.ToString(),
                        ClientOrderID = model.ClientOrderID.ToString(),
                        DeliveryPersonID = model.DeliveryPersonID.ToString(),
                        CODeliveryStatusID = model.CODeliveryStatusID.ToString()
                    };
                } catch { return null; }
            }
            public static List<ClientOrderDeliveryPersonVM> MsToVMs(List<ClientOrderDeliveryPerson> models) {
                var list = new List<ClientOrderDeliveryPersonVM>();
                foreach (var model in models) {
                    list.Add(MToVM(model));
                }
                return list;
            }
            #endregion
        }
        public class ClientOrderVM {
            public string ID { get; set; }
            public string StoreID { get; set; }
            public string UserID { get; set; }
            public string ClientOrderStatusID { get; set; }
            #region static methods
            public static ClientOrder Set(Guid id, Guid appID, Guid storeID, Guid userID, Guid cosID) {
                try {
                    return new ClientOrder() {
                        ID = id,
                        ApplicationID = appID,
                        StoreID = storeID,
                        UserID = userID,
                        ClientOrderStatusID = cosID
                    };
                } catch { return null; }
            }
            public static ClientOrderVM MToVM(ClientOrder model) {
                try {
                    return new ClientOrderVM() {
                        ID = model.ID.ToString(),
                        StoreID = model.StoreID.ToString(),
                        UserID = model.UserID.ToString(),
                        ClientOrderStatusID = model.ClientOrderStatusID.ToString(),
                    };
                } catch { return null; }
            }
            public static List<ClientOrderVM> MsToVMs(List<ClientOrder> models) {
                var list = new List<ClientOrderVM>();
                foreach (var model in models) {
                    list.Add(MToVM(model));
                }
                return list;
            }
            #endregion
        }
        public class ClientOrderItemsVM {
            public string ID { get; set; }
            public string ClientOrderID { get; set; }
            public string IS_ItemStockID { get; set; }
            public string ClientOrderItemStatusID { get; set; }
            #region static methods
            public static ClientOrderItems Set(Guid id, Guid coid, Guid itemStockID, Guid status) {
                try {
                    return new ClientOrderItems() {
                        ID = id,
                        ClientOrderID = coid,
                        IS_ItemStockID = itemStockID,
                        ClientOrderItemStatusID = status,
                    };
                } catch { return null; }
            }
            public static ClientOrderItemsVM MToVM(ClientOrderItems model) {
                try {
                    return new ClientOrderItemsVM() {
                        ID = model.ID.ToString(),
                        ClientOrderID = model.ID.ToString(),
                        ClientOrderItemStatusID = model.ClientOrderItemStatusID.ToString(),
                        IS_ItemStockID = model.IS_ItemStockID.ToString()
                    };
                } catch { return null; }
            }
            public static List<ClientOrderItemsVM> MsToVMs(List<ClientOrderItems> models) {
                var list = new List<ClientOrderItemsVM>();
                foreach (var model in models) {
                    list.Add(MToVM(model));
                }
                return list;
            }
            #endregion
        }
        #endregion
        #region QuizMaker
        public DbSet<QuizInfo> QuizInfoDB { get; set; }
        public DbSet<QuizQuestions> QuizQuestionsDB { get; set; }
        #region multiple choice
        public DbSet<QuizQuestionAnswer> QuizQuestionAnswerDB { get; set; }
        #endregion
        #region user answers
        public DbSet<QuizTakers> QuizTakersDB { get; set; }
        public DbSet<QuizUserAnswer> QuizUserAnswerDB { get; set; }
        #endregion
        #endregion
        #region TimerApp
        public DbSet<TimerApp> TimerAppDB { get; set; }
        public DbSet<TimerAppLimiters> TimerAppLimitersDB { get; set; }
        public DbSet<TimerLeft> TimerLeftDB { get; set; }

        #endregion
    }
}