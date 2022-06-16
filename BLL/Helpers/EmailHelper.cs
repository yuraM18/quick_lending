using System.Net.Mail;

namespace BLL.Helpers
{
    public static class EmailHelper
    {
        public static bool EmailValidate(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
