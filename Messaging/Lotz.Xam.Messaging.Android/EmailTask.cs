using System;
using Android.Content;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class EmailTask : IEmailTask
    {
        private readonly MessagingContext _context;

        public EmailTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        #region IEmailTask Members

        public bool CanSendEmail { get { return true; } }

        public void SendEmail(EmailMessageRequest email)
        {
            // NOTE: http://developer.xamarin.com/recipes/android/networking/email/send_an_email/

            if (email == null)
                throw new ArgumentNullException("email");

            if (CanSendEmail)
            {
                Intent emailIntent = new Intent(Intent.ActionSend);
                emailIntent.SetType("message/rfc822");

                emailIntent.PutExtra(Intent.ExtraEmail, email.Recipients.ToArray());
                emailIntent.PutExtra(Intent.ExtraCc, email.RecipientsCc.ToArray());
                emailIntent.PutExtra(Intent.ExtraBcc, email.RecipientsBcc.ToArray());
                emailIntent.PutExtra(Intent.ExtraSubject, email.Subject);
                emailIntent.PutExtra(Intent.ExtraText, email.Message);

                _context.Activity.StartActivity(emailIntent);
            }
        }

        #endregion
    }
}