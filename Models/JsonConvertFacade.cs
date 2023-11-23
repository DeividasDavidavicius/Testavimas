using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [ExcludeFromCodeCoverage]
    public class JsonConvertFacade : IJsonConvertFacade
    {
        public JsonSerializerSettings settings;

        public JsonConvertFacade()
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public string Serialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize, settings);
        }

        public T? Deserialize<T>(string stringToDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(stringToDeserialize, settings);
        }
    }
}
