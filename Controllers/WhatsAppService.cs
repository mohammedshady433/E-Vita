using System;
using System.Net.Http;
using RestSharp;

namespace E_Vita.Controllers
{
    public class WhatsAppService
    {
        private readonly RestClient client;

        public WhatsAppService(string baseUrl, string apiKey)
        {
            var options = new RestClientOptions(baseUrl)
            {
                MaxTimeout = -1, // Keeps the timeout as infinite. Adjust if needed.
            };
            client = new RestClient(options);
            client.AddDefaultHeader("Authorization", $"App {apiKey}");
            client.AddDefaultHeader("Content-Type", "application/json");
            client.AddDefaultHeader("Accept", "application/json");
        }
        // Method to send WhatsApp message
        //public async Task SendTemplateMessage(string from, string to, string templateName, string language, string[] placeholders)
        //{
        //    var request = new RestRequest("/whatsapp/1/message/template", Method.Post);

        //    // Construct the message body
        //    var body = new
        //    {
        //        messages = new[]
        //        {
        //            new
        //            {
        //                from, // Sender phone number
        //                to,   // Recipient phone number
        //                messageId = Guid.NewGuid().ToString(), // Generate unique message ID
        //                content = new
        //                {
        //                    templateName, // Name of the template to use
        //                    templateData = new
        //                    {
        //                        body = new
        //                        {
        //                            placeholders // Placeholder values for the template
        //                        }
        //                    },
        //                    language // Language of the message
        //                }
        //            }
        //        }
        //    };

        //    // Add the JSON body to the request
        //    request.AddJsonBody(body);

        //    // Execute the request and get the response
        //    RestResponse response = await client.ExecuteAsync(request);

        //    // Check if the request was successful
        //    if (response.IsSuccessful)
        //    {
        //        Console.WriteLine("Message sent successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Error: {response.StatusCode} - {response.Content}");
        //    }
        //}

        public async Task<bool> SendTemplateMessageWithName(string from, string to, string templateName, string language, string customerName, string reservationDetails)
        {
            try
            {
                var request = new RestRequest("/whatsapp/1/message/template", Method.Post);

                // Construct the message body with the templateName
                var body = new
                {
                    messages = new[]
                    {
                new
                {
                    from,
                    to,
                    messageId = Guid.NewGuid().ToString(),
                    content = new
                    {
                        templateName, // Use templateName instead of templateId
                        templateData = new
                        {
                            body = new
                            {
                                placeholders = new[] { customerName, reservationDetails } // Dynamic placeholders
                            }
                        },
                        language
                    }
                }
            }
                };

                request.AddJsonBody(body);

                // Execute the request and get the response
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    Console.WriteLine("Message sent successfully!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.Content}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
    }
}