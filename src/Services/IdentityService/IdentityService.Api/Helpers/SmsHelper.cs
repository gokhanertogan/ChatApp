using IdentityService.Api.Extensions;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace IdentityService.Api.Helpers
{
    public interface ISmsHelper
    {
        void Send(SmsModel model);
    }

    public class SmsHelper : ISmsHelper
    {
        private readonly IOoredooClient _ooredooClient; 

        public SmsHelper()
        {

        }

        private SmsResponseModel sendSms(SmsModel model)
        {
            //return UseTwilioClient(model);
            return UserOoredooClient(model);
        }

        /// <summary>
        /// ooredoo sms client
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private SmsResponseModel UserOoredooClient(SmsModel model)
        {
            return _ooredooClient.Send(model);
        }

        /// <summary>
        /// twilio sms client
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private SmsResponseModel UseTwilioClient(SmsModel model)
        {
            const string accountSid = "AC9e01ff5e9ca62ca8e3102db15cfbbfcd";
            const string authToken = "a963725094cd759d7fd7d0c294cbbd49";
            TwilioClient.Init(accountSid, authToken);
            var result = MessageResource.Create(
                body: model.Text,
                from: new Twilio.Types.PhoneNumber("+442033221529"),
                to: new Twilio.Types.PhoneNumber(model.To)
            );
            return new SmsResponseModel() { IsError = result.ErrorCode.GetValueOrDefault(0) > 0, ErrorMessage = result.ErrorMessage };
        }

        private void sendSmsWithLog(SmsModel model, string smsLogName)
        {
            Task.Run(() =>
            {
                //ServiceLogCommand commandRequest = new ServiceLogCommand();
                //Guid correlationId = Guid.NewGuid();
                //commandRequest.QueryId = Guid.NewGuid();

                //commandRequest.AggregateId = correlationId;
                //commandRequest.RawRequest = JsonConvert.SerializeObject(model);
                //commandRequest.IntegrationName = "Sms_Request";
                //commandRequest.CreatedOn = DateTime.Now;
                //commandRequest.ExtraMessage = smsLogName;

                //CommandService.Provider.Send(commandRequest);

                //var message = "success";

                //try
                //{
                //    SmsResponseModel result = sendSms(model);
                //    if (result.IsError)
                //        message = result.ErrorMessage;
                //}
                //catch (Exception exc)
                //{
                //    message = exc.Message + " StackTrace : " + exc.StackTrace;
                //}

                //ServiceLogCommand commandResponse = new ServiceLogCommand();

                //commandResponse.QueryId = Guid.NewGuid();

                //commandResponse.AggregateId = correlationId;
                //commandResponse.RawResponse = message;
                //commandResponse.IntegrationName = "Sms_Response";
                //commandResponse.CreatedOn = DateTime.Now;
                //commandResponse.ExtraMessage = smsLogName;

                //CommandService.Provider.Send(commandResponse);
            }
            );
        }

        public void Send(SmsModel model)
        {
            sendSmsWithLog(model, null);
        }
    }

    public class SmsModel
    {
        public string Text { get; set; }
        public string To { get; set; }
    }

    public class SmsResponseModel
    {
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
    }
}
