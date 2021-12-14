using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace captchademov3.Models
{
    public class GoogleCaptchaService
    {
        public async Task<bool> verifycaptcha(string _Token)
        {
            
            using(var client = new HttpClient())
            {
              var values=new Dictionary<string, string>
                {
                  { "response" , _Token },
                  { "secret" , "6LezVY0dAAAAAHXWLv68LypHJd6NYloRbcgNBLeQ" }
                };
                var content = new FormUrlEncodedContent(values);
                var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                var captcharesponsejson=await verify.Content.ReadAsStringAsync();
                var captcharesult=JsonConvert.DeserializeObject<GoogleCaptchaRespo>(captcharesponsejson);
                return captcharesult.success  && captcharesult.score > 0.5;
            }
        }
    }

    public class GoogleCaptchaData
    {
        public string response { get; set; }
        public string secret { get; set; }
    }
    public class GoogleCaptchaRespo
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
