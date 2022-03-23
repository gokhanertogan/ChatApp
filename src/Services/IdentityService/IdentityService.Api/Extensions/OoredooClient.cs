using IdentityService.Api.Helpers;
using IdentityService.Api.Settings;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;

namespace IdentityService.Api.Extensions
{
    public interface IOoredooClient
    {
        SmsResponseModel Send(SmsModel model);
    }

    public class OoredooClient : IOoredooClient
    {
        private readonly IGeneralConfigs _generalConfigs;
        private readonly HttpClient _httpClient;

        OoredooClient(IGeneralConfigs generalConfigs)
        {
            _httpClient = new HttpClient();
            _generalConfigs = generalConfigs;
        }

        private string CleanXml(string xml)
        {
            string pattern = "(xmlns=\")([a-zA-Z]|[^a-z])+(\")";
            return Regex.Replace(xml, pattern, "", RegexOptions.IgnoreCase);
        }

        public SmsResponseModel Send(SmsModel model)
        {
            var uri = new UriBuilder(_generalConfigs.OoredooSettings.ApiUrl);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["customerID"] = _generalConfigs.OoredooSettings.CustomerId;
            query["userName"] = _generalConfigs.OoredooSettings.Username;
            query["userPassword"] = _generalConfigs.OoredooSettings.Password;
            query["originator"] = _generalConfigs.OoredooSettings.Originator;
            query["smsText"] = model.Text;
            query["recipientPhone"] = model.To;
            query["messageType"] = "Latin";
            query["defDate"] = DateTime.Now.AddDays(-1).ToString("yyyyMMddhhmmss");
            query["blink"] = "false";
            query["flash"] = "false";
            query["Private"] = "false";

            uri.Query = query.ToString();

            var result = _httpClient.GetAsync(uri.ToString()).Result;

            if (result.IsSuccessStatusCode)
            {
                var response = result.Content.ReadAsStringAsync().Result;
                response = CleanXml(response);
                var serializer = new XmlSerializer(typeof(SmsResponsemodel));
                using (StringReader sr = new StringReader(response))
                {
                    var smsResult = (SmsResponsemodel)serializer.Deserialize(sr);
                    if (smsResult.Result == "OK")
                    {
                        return new SmsResponseModel() { IsError = false };
                    }
                    return new SmsResponseModel() { IsError = true, ErrorMessage = smsResult.Result };
                }
            }
            else
            {
                var response = result.Content.ReadAsStringAsync().Result;
                return new SmsResponseModel() { IsError = true, ErrorMessage = response };
            }
        }
    }

    [XmlRoot("SendResult")]
    public class SmsResponsemodel
    {
        [XmlElement("NetPoints")]
        public string NetPoints { get; set; }

        [XmlElement("Result")]
        public string Result { get; set; }

        [XmlElement("TransactionID")]
        public string TransactionID { get; set; }
    }
}
