using System;
using System.IO;
using System.Web;
using System.Xml;
using log4net;
using Spikes;

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
            SmsPayload payload;
            using (var parser = new SmsPayloadParser(stream))
            {
                payload = parser.Parse();
            }
            
            context.Response.Write($"{payload.SenderPhoneNumber}");
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