using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace E_Vita.Controllers
{
    public class WhatsApp
    {
        private readonly string accountSid = "AC70b132c4d65b73ab407b4480a50b39cd";
        private readonly string authToken = "3a867e827cabb6f995c6861674bc87aa";
        private readonly string twilioPhoneNumber = "+14155238886";

        public void SendWhatsAppMessage(string toPhoneNumber, string messageBody)
        {
            try
            {
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(new Twilio.Types.PhoneNumber("whatsapp:+2" + toPhoneNumber))
                {
                    From = new Twilio.Types.PhoneNumber("whatsapp:" + twilioPhoneNumber),
                    Body = messageBody
                };

                var message = MessageResource.Create(messageOptions);

                Console.WriteLine($"Message sent successfully. SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending WhatsApp message: {ex.Message}");
            }
        }
    }
}
