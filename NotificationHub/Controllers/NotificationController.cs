using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NotificationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IHubContext<NewsHub> hubContext;

        public NotificationController(ILogger<NotificationController> logger, IHubContext<NewsHub> newsHub)
        {
            _logger = logger;
            this.hubContext = newsHub;
        }

        [HttpPost]
        [Route("Push")]
        public async Task<string> PushNews()
        {
            string secret = "CtIRauGHqD";


            // Get Signature from request
            string signature = Request.Headers["X-Signature"]; //besser vorher schauen ob es existiert

            // Get request body (raw)
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
            }

            // Deserialize json response
            var history = JsonConvert.DeserializeObject<SquidexRequest>(body);

            // Calculate signature -> ToBase64String(Sha256(RequestBody + Secret))
            string calculatedSignature = Sha256Base64(body+secret);

            //Compare signature
            if (calculatedSignature.Equals(signature))
            {
                // Broadcast
                await this.hubContext
                  .Clients
                  .All
                  .SendAsync("BroadcastMessage", history.payload.data.title.iv.ToString());
            }

            return history.payload.data.title.iv.ToString();
        }

        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return "Notification service is up and running...";
        }

        public static string Sha256Base64(string value)
        {
            using (var sha = SHA256.Create())
            {
                var bytesValue = Encoding.UTF8.GetBytes(value);
                var bytesHash = sha.ComputeHash(bytesValue);

                var result = Convert.ToBase64String(bytesHash);

                return result;
            }
        }
    }

    public class SquidexRequest
    {
        public string type { get; set; }
        public Payload payload { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Payload
    {
        public string type { get; set; }
        public string id { get; set; }
        public DateTime created { get; set; }
        public DateTime lastModified { get; set; }
        public string createdBy { get; set; }
        public string lastModifiedBy { get; set; }
        public Data data { get; set; }
        public string status { get; set; }
        public int partition { get; set; }
        public string schemaId { get; set; }
        public string actor { get; set; }
        public string appId { get; set; }
        public DateTime timestamp { get; set; }
        public string name { get; set; }
        public int version { get; set; }
    }

    public class Data
    {
        public Title title { get; set; }
        public Subtitle subtitle { get; set; }
        public Text text { get; set; }
        public Image image { get; set; }
    }

    public class Title
    {
        public string iv { get; set; }
    }

    public class Subtitle
    {
        public string iv { get; set; }
    }

    public class Text
    {
        public string iv { get; set; }
    }

    public class Image
    {
        public string[] iv { get; set; }
    }
}