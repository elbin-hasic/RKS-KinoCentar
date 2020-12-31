using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace KinoCentar.Shared.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static string HandleResponseMessage(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var detailMsg = response.ReasonPhrase;
                try
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    if (jsonResult != null && !IsValidJson(jsonResult))
                    {
                        detailMsg = jsonResult;
                    }
                }
                catch
                { }

                return $"Error Code: { response.StatusCode }; Message: { detailMsg }";
            }

            return null;
        }

        public static T GetResponseResult<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var jsonObject = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonObject.Result);
            }
            else
            {
                return default(T);
            }
        }

        public static string ReadLastExceptionMessage(this Exception error)
        {
            Exception realerror = error.ReadLastException();
            return realerror.Message;
        }

        public static Exception ReadLastException(this Exception error)
        {
            Exception realerror = error;
            
            while (realerror.InnerException != null)
                realerror = realerror.InnerException;

            return realerror;
        }

        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
