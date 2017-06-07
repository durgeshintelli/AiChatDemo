using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ApiAiSDK.Model;
using Newtonsoft.Json.Linq;


namespace AiChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "ValidateUser/{sUserName}/{sPassword}")]
        List<ValidateUser> ValidateUser(string sUserName, string sPassword);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "GetTestResponse")]
        AIResponse GetTestResponse();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ValidateUser
    {
        Int32 id = 0;


        [DataMember]
        public Int32 ID
        {
            get { return id; }
            set { id = value; }
        }


    }
      # region AI


    public class AIResponse
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Lang { get; set; }
        public Result Result { get; set; }
        public Status Status { get; set; }
        public string SessionId { get; set; }
        public Fulfillment Fulfillment { get; set; }
        public bool IsError
        {
            get
            {
                if (Status != null && Status.Code.HasValue && Status.Code >= 400)
                {
                    return true;
                }

                return false;
            }
        }
    }
    public class Status
    {
        public int? Code { get; set; }
        public string ErrorType { get; set; }
        public string ErrorDetails { get; set; }
        public string ErrorID { get; set; }
        public Status()
        {
        }
    }


    public class Result
    {
        String action;
        public Boolean ActionIncomplete { get; set; }
        public String Action
        {
            get
            {
                if (string.IsNullOrEmpty(action))
                {
                    return string.Empty;
                }
                return action;
            }
            set
            {
                action = value;
            }
        }

        public Dictionary<string, object> Parameters { get; set; }
        public AIOutputContext[] Contexts { get; set; }
        public Metadata Metadata { get; set; }
        public String ResolvedQuery { get; set; }
        public Fulfillment Fulfillment { get; set; }
        public string Source { get; set; }
        public float Score { get; set; }
        public bool HasParameters
        {
            get
            {
                return Parameters != null && Parameters.Count > 0;
            }
        }

        public string GetStringParameter(string name, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (Parameters.ContainsKey(name))
            {
                return Parameters[name].ToString();
            }

            return defaultValue;
        }

        public int GetIntParameter(string name, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (Parameters.ContainsKey(name))
            {
                var parameterValue = Parameters[name].ToString();
                int result;
                if (int.TryParse(parameterValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }

                float floatResult;
                if (float.TryParse(parameterValue, NumberStyles.Float, CultureInfo.InvariantCulture, out floatResult))
                {
                    result = Convert.ToInt32(floatResult);
                    return result;
                }
            }

            return defaultValue;
        }

        public float GetFloatParameter(string name, float defaultValue = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (Parameters.ContainsKey(name))
            {
                var parameterValue = Parameters[name].ToString();
                float result;
                if (float.TryParse(parameterValue, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public JObject GetJsonParameter(string name, JObject defaultValue = null)
        {
            if (string.IsNullOrEmpty("name"))
            {
                throw new ArgumentNullException(name);
            }

            if (Parameters.ContainsKey(name))
            {
                var parameter = Parameters[name] as JObject;
                if (parameter != null)
                {
                    return parameter;
                }
            }

            return defaultValue;
        }

        public AIOutputContext GetContext(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must be not empty", name);
            }

            return Contexts.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public Result()
        {
        }
    }

    #endregion


   
}
