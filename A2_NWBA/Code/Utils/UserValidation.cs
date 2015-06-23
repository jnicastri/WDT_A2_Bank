using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Data;

namespace A2_NWBA.Code.Utils
{
    public class UserValidation
    {
        public static int? ValidateUserCredentials(string UserId, string HashedPwd){

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@UserId", SqlDbType.NVarChar, UserId);
            paramList.Add("@HashedPwd", SqlDbType.NVarChar, HashedPwd);

            int? customerId = (int?)SqlTools.ExecuteScalar("dbo.User_Validate", paramList);

            return customerId;
        }

        public static string GetHashedPassword(string password)
        {
            MD5CryptoServiceProvider crypterService = new MD5CryptoServiceProvider();
            byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(password);
            
            passwordBytes = crypterService.ComputeHash(passwordBytes);

            return System.Text.Encoding.ASCII.GetString(passwordBytes);
        }

        public static void UpdateUserPassword(string UserId, string HashedPwd, int CustomerNumber)
        {

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@UserId", SqlDbType.NVarChar, UserId);
            paramList.Add("@CustomerNumber", SqlDbType.Int, CustomerNumber);
            paramList.Add("@NewPassword", SqlDbType.NVarChar, HashedPwd);
            
            SqlTools.ExecuteNonQuery("dbo.User_ChangePassword", paramList);
        }
 
    }
}