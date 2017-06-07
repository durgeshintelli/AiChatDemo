using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ApiAiSDK.Model;
using ApiAiSDK;
using Newtonsoft.Json;

namespace AiChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string ACCESS_TOKEN = "a7487a128bf54f599a084c067cdfd5ce";
        Service1()
        {
        }

        public List<ValidateUser> ValidateUser(string sUserName, string sPassword)
        {
            GetTestResponse();
            var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.English);

            var apiAi = new ApiAi(config);

            var response = apiAi.TextRequest("hh");
            List<ValidateUser> objvalidate = new List<ValidateUser>();
            return objvalidate.ToList();
        }

        # region AI function

        public AIResponse GetTestResponse()
        {
            var testObject = new
            {
                id = "2d2d947b-6ccd-4615-8f16-59b8bfc0fa6b",
                timestamp = "2017-07-13T11:03:43.023Z",
                result = new
                {
                    source = "agent",
                    resolvedQuery = "test params 1.23",
                    speech = "manoj",
                    action = "test_params",
                    parameters = new
                    {
                        number = "1.23",
                        integer = "17",
                        str = "string value",
                        complex_param = new { nested_key = "nested_value" }
                    },
                    metadata = new
                    {
                        intentId = "46a278fb-0ffc-4748-aa9a-5563d89199ee",
                        intentName = "test params"
                    }
                },
                fulfillment = new { speech = "chandan" },
                status = new { code = 200, errorType = "success" }
            };

            var jsonText = JsonConvert.SerializeObject(testObject);
            return JsonConvert.DeserializeObject<AIResponse>(jsonText);
        }

        # endregion
    }
}
