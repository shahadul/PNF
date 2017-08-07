using System;
using System.Text.RegularExpressions;
using System.Web;

namespace PNF
{
    class Validator
    {
        DatabaseAccess dba = new DatabaseAccess();
        private string stringSanatizer(string value)
        {
            string sanatized = "";
            sanatized = HttpUtility.UrlEncode(value);
            return sanatized;
        }

        public bool CheckCredentials(string username, string password)
        {
            string result;

            if (username.Length > 0 && password.Length > 0)
            {
                if (Regex.IsMatch(username,
                                                   "^[a-zA-Z0-9'-.]{1,40}$") && Regex.IsMatch(password,
                                                   "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$"))
                {
                    String query = "EXEC spLoginChecker '" + username.ToString() + "','" + password.ToString() + "'";
                    result = dba.getObjectDataStr(query, "ConnDB230");
                    if (result == "1")
                    { return true; }
                    else
                        return false;

                }
                else
                {

                    return false;
                }

            }
            else
                return false;

        }



        public bool CheckCredentials(string password)
        {
            string result;

            if (password.Length > 0)
            {
                if (Regex.IsMatch(password,
                                                   "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$"))
                {

                    return true;


                }
                else
                {

                    return false;
                }

            }
            else
                return false;

        }

        public bool UserNameChecker(string username)
        {
            string result;

            if (username.Length > 0)
            {
                try
                {
                    String query = "EXEC UserNameChecker '" + username.ToString() + "'";
                    result = dba.getObjectDataStr(query, "ConnDB230");
                    if (result == "1")
                    { return true; }
                    else
                        return false;
                }
                catch (Exception ex)
                { throw ex; }
            }
            else
                return false;

        }

        public bool SOUserIDChecker(string username)
        {
            string result;

            if (username.Length > 0)
            {
                try
                {
                    String query = "SELECT UserLoginID from Sales_Officer WHERE UserLoginID = '" + username + "'";
                    result = dba.getObjectDataStr(query, "ConnDB230");
                    if (result != "")
                    { return true; }
                    else
                        return false;
                }
                catch (Exception ex)
                { throw ex; }
            }
            else
                return false;

        }
    }
}
