using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AwesomeDistributed.Site.Caching
{
    public static class CompressionUtilities
    {
        private static readonly JsonSerializer serializer = new();

        public static byte[] Zip<T>(T value)
        {
            using (var mso = new MemoryStream())
            using (var gs = new GZipStream(mso, CompressionLevel.Fastest))
            using (var sw = new StreamWriter(gs, Encoding.UTF8))
            using (var jtw = new JsonTextWriter(sw))
            {
                serializer.Serialize(jtw, value);
                jtw.Flush();
                return mso.ToArray();
            }
        }

        public static T Unzip<T>(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            using (var sr = new StreamReader(gs, Encoding.UTF8))
            using (var jtr = new JsonTextReader(sr))
            {
                return (T)serializer.Deserialize(jtr, typeof(T));
            }
        }
    }
}
