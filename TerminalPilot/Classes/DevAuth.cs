using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Pastel;
using System.Drawing;
using System.Diagnostics;

namespace TerminalPilot.Classes
{
    public class ApiGetExchangeToken
    {
        public string ExchangeToken { get; set; }
    }
    public class ApiGetUserToken
    {
        public string ExchangeToken { get; set; }
        public string UserToken { get; set; }
        public string Status { get; set; }

    }
    public class AuthMethods
    {
        //create a share httpclient class
        private static HttpClient _client = new HttpClient();
        public static async Task<ApiGetExchangeToken> GetExchangeToken()
        {
            var response = await _client.GetAsync("https://auth.axell.me/authapi/getexchangetoken");
            var responseString = await response.Content.ReadAsStringAsync();
            var exchangeToken = JsonSerializer.Deserialize<ApiGetExchangeToken>(responseString);
            return exchangeToken;
        }
        //send a get request to the auth server to get a user token with an exchange token
        public static async Task<ApiGetUserToken> GetUserToken(string exchangeToken)
        {
            var response = await _client.GetAsync("https://auth.axell.me/authapi/getusertoken?exchangetoken=" + exchangeToken);
            var responseString = await response.Content.ReadAsStringAsync();
            var userToken = JsonSerializer.Deserialize<ApiGetUserToken>(responseString);
            return userToken;
        }
        //run getusertoken every 5 seconds until the user has authenticated
        public static async Task<ApiGetUserToken> GetUserTokenLoop(string exchangeToken)
        {
            var userToken = await GetUserToken(exchangeToken);
            Console.WriteLine(userToken.UserToken);
            while (userToken.Status != "Success")
            {
                await Task.Delay(5000);
                userToken = await GetUserToken(exchangeToken);
            }
            return userToken;
        }

        //tie everything together
        public static void InitAuth(string command)
        {
            if (ConfigManager.GetUserToken() != String.Empty)
            {
                Console.WriteLine("You are already authenticated!".Pastel(Color.FromArgb(255, 115, 96, 223)));
                return;
            }
            if (command.Split(' ').Length > 2)
            {
                if (command.Split(' ')[2] == "logout")
                {
                    ConfigManager.SetUserToken(null);
                    Console.WriteLine("You have been logged out.".Pastel(Color.FromArgb(255, 115, 96, 223)));
                    return;
                }
            }
            //get an exchange token
            var exchangeTokenpromise = GetExchangeToken();
            exchangeTokenpromise.Wait();
            var exchangeToken = exchangeTokenpromise.Result.ExchangeToken;
            var psi = new ProcessStartInfo
            {
                FileName = "https://auth.axell.me/github/welcome?exchangetoken=" + exchangeToken,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
            Console.WriteLine();
            Console.WriteLine("Head over to your browser window and complete the authentication.".Pastel(Color.FromArgb(255, 115, 96, 223)));
            var usertokenpromise = GetUserTokenLoop(exchangeToken);
            usertokenpromise.Wait();
            var usertoken = usertokenpromise.Result.UserToken;
            Console.WriteLine();
            Console.WriteLine("Authentication complete!".Pastel(Color.FromArgb(255, 115, 96, 223)));
            //save the user token with the config manager
            ConfigManager.SetUserToken(usertoken);
        }
        


    }
}
