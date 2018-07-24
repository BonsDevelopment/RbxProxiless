using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RbxProxiless.Core
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class CoreLogin
    {
        static HttpClientHandler handler = new HttpClientHandler() { UseCookies = false };
        private static HttpClient client = new HttpClient(handler);

        public static async Task<string> ProxilessLogin(string username, string password)
        {
            string[] bId = DeviceHandleGenerator.GrabDeviceHandle().Split(':');

            var content = new FormUrlEncodedContent(new[] {

                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),

            });
            var message = new HttpRequestMessage(HttpMethod.Get, "https://api.roblox.com/v2/login");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("RBX-Device-Handle", bId[1]);
            client.DefaultRequestHeaders.Add("Cookie", $"RBXEventTrackerV2=browserid={bId[0]}");

            using (var result = await client.PostAsync("https://api.roblox.com/v2/login", content))
            {
                IEnumerable<string> validResponse;

                var responseHeader = result.Headers.TryGetValues("Set-Cookie", out validResponse);

                if (responseHeader)
                {
                    foreach (var item in validResponse)
                    {
                        if (item.Contains(".ROBLO"))
                            return item.Split(';')[0];                           
                    }
                }
            }

            return null;

        }
    }

}
