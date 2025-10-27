
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
     
    #endregion Static Class

}
