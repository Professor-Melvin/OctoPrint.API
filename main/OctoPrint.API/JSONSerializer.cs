using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrint.API
{
    public class JSONSerializer
    {
        public static bool Serialize<T>(T dataObject, out string jsonString)
        {
            jsonString = "";
            try
            {
                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                //Use the WriteObject method to write JSON data to the stream.
                ser.WriteObject(stream1, dataObject);
                //Show the JSON output.
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                //JSON form of T object
                jsonString = sr.ReadToEnd();
                return true;
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public static bool Deserialize<T>(string jsonString, out T dataObject)
        {
            dataObject = default(T);
            try
            {
                MemoryStream stream1 = new MemoryStream();
                StreamWriter sw = new StreamWriter(stream1);
                sw.Write(jsonString);
                sw.Flush();

                stream1.Position = 0;

                DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
                settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss.fff");
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T), settings);

                dataObject = (T)ser.ReadObject(stream1);
                return true;
            }
            catch (Exception ex)
            {
                //Logger.LogError("Deserialize Failed.", ex);
            }
            return false;
        }
    }
}
