using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class MessageUtility
    {
        public static string EmailAddressAlreadyBeingUsed() {
            return SorryCannotCreate() + " " + YourAccount() + " the email address you entered is already registered";
        }
        public static string ErrorClientOrderNotAvailable() {
            return "Sorry client order not available anymore";
        }

        public static string SorryCannotCreate() {
            return "Sorry we cannot create";
        }
        public static string YourAccount() {
            return "your account";
        }

        #region ClientOrders
        public static string RequestTaken() {
            return "Sorry request is already taken";
        }
        public static string ClientOrderCannotBeProcessed() {
            return "Sorry client order request cannot be processed";
        }
        #endregion
        public static string LoginAuthError() {
            return "Login authorization error please login valid account";
        }
        public static string ServerError() {
            return "Server error occured please try again later";
        }
        public static string Unaccessible() {
            return "Sorry Service Unaccessible";
        }



        #region Connections
        public static string AlreadyMember() {
            return "Already a member of this group";
        }
        #endregion
    }
}