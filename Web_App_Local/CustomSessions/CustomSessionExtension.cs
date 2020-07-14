using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_App_Local.CustomSessions
{
    public static class CustomSessionExtension
    {
        public static void SetSessionData<T>(this ISession session, string sessionKey, T sessionValue)
        {
            session.SetString(sessionKey, JsonConvert.SerializeObject(sessionValue));
        }

        public static T GetSessionData<T>(this ISession session, string sessionKey)
        {
            var Data = session.GetString(sessionKey);

            if (Data == null)
            {
                 return default(T);
            }
            else 
            {
                return JsonConvert.DeserializeObject<T>(Data);
            }
        }
    }
}
