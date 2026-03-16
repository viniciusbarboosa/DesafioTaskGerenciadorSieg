using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TaskManagerConsole.Helpers
{
    public class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email);
                return emailAddress.Address == email;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
