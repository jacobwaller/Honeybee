using System;
using Newtonsoft.Json;
using RestSharp;

namespace Honeybee.Test.Common
{
    public static class Extensions
    {
        public static T DeserializeContent<T>(this IRestResponse obj)
        {
            return JsonConvert.DeserializeObject<T>(obj.Content);
        }
    }
}
