using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorChat
{
    public class BlazorChatSampleHub : Hub
    {
        public const string HubUrl = "/chat";
        static List<User> ConnectedUsers = new List<User>();
        static int TotalTranslations;
        Dictionary<string, string> Translations = new Dictionary<string, string>();

        public async Task Broadcast(string message)
        {
            if (message.StartsWith("[Notice]"))
            {
                await Translate("en", message);
            }
            else
            {
                await Translate(CurrentUser.Language, message);
            }
            foreach (KeyValuePair<string, string> translation in Translations)
            {
                await Clients.Group(translation.Key).SendAsync("Broadcast", $"{CurrentUser.Name} ({CurrentUser.Language})", translation.Value);
            }
        }

        public async Task UserConnected(List<User> user)
        {
            var id = Context.ConnectionId;
            ConnectedUsers.Add(new User() {Name = user[0].Name, ConnectionId = id, Language = user[0].Language});
            await Groups.AddToGroupAsync(id, user[0].Language);
            await Clients.All.SendAsync("UserConnected", ConnectedUsers);
            Console.WriteLine($"Adding... id: {id}, user: {user[0].Name}, language: {user[0].Language}");
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            // Remove user on disconnect
            var user = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                ConnectedUsers.Remove(user);
                await Groups.RemoveFromGroupAsync(user.ConnectionId, user.Language);
                Console.WriteLine($"Removing... id: {user.ConnectionId}, user: {user.Name}, language: {user.Language}");
                await Clients.All.SendAsync("UserConnected", ConnectedUsers);
                await base.OnDisconnectedAsync(e);
            }
        }

        private User CurrentUser
        {
            get
            {
                return ConnectedUsers.FirstOrDefault(i => i.ConnectionId == Context.ConnectionId);
            }
        }

        private async Task Translate(string sourceLanguage, string message)
        {
            TotalTranslations += 1;
            Console.WriteLine("Translation Counter: " + TotalTranslations);

            // Setup and retrieve basic job settings
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("config/appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            AzureSpeechSettings azureSpeechSettings = config.GetRequiredSection("AzureSpeech").Get<AzureSpeechSettings>();
            
            List<string> languages = new List<string> {"zh-Hant", "en", "de", "ru", "es"};
            int i = languages.IndexOf(sourceLanguage);
            if (TotalTranslations < 2000)
            {
                languages.RemoveAt(i);
            }

            StringBuilder route = new StringBuilder("/translate?api-version=3.0&from=" + sourceLanguage, 100);
            route.Append("&to=" + sourceLanguage);
            foreach (var lan in languages)
            {
                route.Append("&to=" + lan);
            }
            Console.WriteLine(route.ToString());

            if (TotalTranslations < 2000)
            {
                object[] body = new object[] { new { Text = message } };
                var requestBody = JsonConvert.SerializeObject(body);
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    // Build the request.
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(azureSpeechSettings.Endpoint + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", azureSpeechSettings.SubscriptionKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", azureSpeechSettings.Location);
            
                    // Send the request and get response.
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    // Read response as a string.
                    string jsonResults = await response.Content.ReadAsStringAsync();

                    jsonResults = jsonResults.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                    var transList = JObject.Parse(jsonResults).SelectToken("translations").ToObject<List<Translation>>();
                    
                    Translations.Clear();
                    foreach (var tr in transList)
                    {
                        Translations.Add(tr.To, tr.Text);
                    }
                    Console.WriteLine(jsonResults);
                }
            }
            else
            {
                Translations.Clear();
                foreach (var tr in languages)
                {
                    Translations.Add(tr, message);
                }
                Console.WriteLine("Translation limit exceeded");
            }
        }
    }

    public class User
    {
        public string Name {get; set;}
        public string Language {get; set;}
        public string ConnectionId {get; set;}
    }

    [JsonObject]
    public class Translation
    {
        [JsonProperty("text")]
        public string Text {get; set;}
        [JsonProperty("to")]
        public string To {get; set;}
    }

     // Collections of job-specific settings
    public class AzureSpeechSettings
    {
        public string SubscriptionKey { get; set; }
        public string Endpoint { get; set; }
        public string Location { get; set; }
    }
}