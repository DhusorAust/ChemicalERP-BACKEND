
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ChemicalERP.Models
{
    public static class CommonHelper
    {
        public static string PasswordEncrypt(string Password)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(Reverse_String(Password));
            string EncryptedPassword = Convert.ToBase64String(plainTextBytes);
            return EncryptedPassword;
        }
        public static string Reverse_String(string Password)
        {
            char[] charArray = Password.ToCharArray();
            Array.Reverse(charArray);
            string strReversePassword = new string(charArray);
            return strReversePassword;
        }
    }
    #region Static Class

    public static class AppGlobalFields
    {
        public const string NEW = "<<--System Value-->>";
        public const string ProjectID = "5";
        public const string AdminMessage = "Please contact with IT department.";
    }
    public static class DBSource
    {
        public const string ChemicalERP = "ChemicalERP";
        public const string PDLHRM = "PDLHRM";
        public const string ERPUSERDB = "ERPUSERDB";
    }
    public class DropDownList
    {
        public int? id { get; set; }
        public int? itemId { get; set; }
        public string ids { get; set; }
        public int? virtualID { get; set; }
        public string text { get; set; }
        public string desc { get; set; }
        public string additionalValue { get; set; }
        public int? additionalID { get; set; }
        public string valueA { get; set; }
        public string valueB { get; set; }
        public string valueC { get; set; }
        public string valueD { get; set; }
        public string valueE { get; set; }
        public string valueF { get; set; }
        public string valueG { get; set; }
        public string valueH { get; set; }
        public string valueI { get; set; }
        public DateTime? dateA { get; set; }
        public DateTime? dateB { get; set; }

    }
    public enum Status
    {
        ALL = 1,
        NEW = 2,
        EDIT = 3,
        PENDING = 4,
        COMPLETED = 5,
        PARTIALLY_COMPLETED = 6,
        APPROVED = 7,
        PROPOSED_FOR_APPROVAL = 8,
        UN_APPROVE = 9,
        ACKNOWLEDGE = 10,
        PROPOSED_FOR_ACKNOWLEDGE = 11,
        UN_ACKNOWLEDGE = 12,
        ACKNOWLEDGE_ACCEPTENCE = 13,
        PROPOSED_FOR_ACKNOWLEDGE_ACCEPTENCE = 14,
        REJECT = 15,
        REVISE = 16,
        CHECK = 17,
        CHECK_REJECT = 18,
        ACTIVE = 19,
        IN_ACTIVE = 20,
        RECEIVED = 21,
        ISSUED = 22,
        CANCEL = 23,
        RUNNING = 24,
        CLOSE = 25,

        FILE = 26,
        INVOICE = 27,
        GP_NO = 28,
        INT_CHALLAN = 29,

        RETURN = 29,
        OTHERS = 29,

        LIST_1 = 40,
        LIST_2 = 41,
        LIST_3 = 42,
        LIST_4 = 43,
        LIST_5 = 44,
        LIST_6 = 45,
    }


    #endregion Static Class

}
