using System;
using System.IO;
using System.Web;
using System.Xml;
using log4net;

namespace StreamSpike
{
    /// <summary>
    ///     Summary description for Foo
    /// </summary>
    public class Foo : IHttpHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Foo));

        public void ProcessRequest(HttpContext context)
        {
            var stream = context.Request.InputStream;
//            var memoryStream = new MemoryStream();
//            stream.CopyTo(memoryStream);
//            LogRequest(memoryStream.ToArray());

//            memoryStream.Position = 0;
//            var document = new XmlDocument();
//            document.Load(memoryStream);

            string readToEnd;
            using (var reader = new StreamReader(stream))
            {
                readToEnd = reader.ReadToEnd();
            }

            var s = ParseString(readToEnd);

            context.Response.Write($"{s}");
            context.Response.ContentType = "text/plain";
        }

        public bool IsReusable => false;

        private string ParseString(string s)
        {
            try
            {
                var document = new XmlDocument();
                document.LoadXml(s);
                return document.SelectSingleNode("/foo").InnerText;
            }
            catch (Exception e)
            {
                throw new ParseException(s, e);
            }
        }

        private static void LogRequest(byte[] stream)
        {
            foreach (var b in stream)
            {
                Log.DebugFormat($"{b:X} ");
            }
        }
    }

    internal class ParseException : Exception
    {
        public ParseException(string s, Exception exception) : base(s, exception)
        {
        }
    }
}