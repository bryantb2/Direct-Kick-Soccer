using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;
using PayPalHttp;

namespace dropShippingApp.HelperUtilities
{
    public class PayPalClient
    {
        public static PayPalEnvironment Environment(string sandboxId, string sandboxSecret)
        {
            // fix this to switch based on environment
            return new SandboxEnvironment(sandboxId, sandboxSecret);
        }

        public static HttpClient Client(IConfiguration config)
        {
            var sandboxID = config["PaypalCredentials:SandBoxClientID"];
            var sandboxSecret = config["PaypalCredentials:Secret"];
            return new PayPalHttpClient(Environment(sandboxID, sandboxSecret));
        }

        public static HttpClient Client(IConfiguration config, string refreshToken)
        {
            var sandboxID = config["PaypalCredentials:SandBoxClientID"];
            var sandboxSecret = config["PaypalCredentials:Secret"];
            return new PayPalHttpClient(Environment(sandboxID, sandboxSecret), refreshToken);
        }

        /*public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }*/
    }
}
