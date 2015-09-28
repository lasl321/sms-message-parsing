using System;
using System.IO;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace Spikes.SMSMessageParsing
{
    public class EmojiWebTests
    {
        [Test]
        public void ShouldPrintHappyFace()
        {
//            var bytes = new byte[]
//            {
//                0xF0,
//                0x9F,
//                0x98,
//                0x81
//            };

            var bytes = new byte[]
            {
                0x00,
                0x43,
                0x00,
                0x44
            };
            var start = GetStart();
            var end = GetEnd();
            var request = WebRequest.CreateHttp("http://localhost:1335/Foo.ashx");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length + start.Length + end.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(start, 0, start.Length);
                stream.Write(bytes, 0, bytes.Length);
                stream.Write(end, 0, end.Length);
            }

            var response = (HttpWebResponse) request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine(responseString);
        }

        private byte[] GetStart()
        {
            const string s = "<foo>";
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(s);
            }

            return stream.ToArray();
        }

        private byte[] GetEnd()
        {
            const string s = "</foo>";
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(s);
            }

            return stream.ToArray();
        }

        [Test]
        public void ShouldPrintCatFace()
        {
            var bytes = new byte[]
            {
                0xF0,
                0x9F,
                0x98,
                0xBC
            };

            var stream = new MemoryStream(bytes);
            using (var reader = new StreamReader(stream))
            {
                var s = reader.ReadToEnd();
                Console.WriteLine(s);
            }
        }


        [Test]
        public void ShouldPrintC()
        {
            var bytes = new byte[]
            {
                0x00,
                0x43
            };

            var stream = new MemoryStream(bytes);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var s = reader.ReadToEnd();
                Console.WriteLine("'{0}'", s);
            }
        }

        [Test]
        public void ShouldPrint()
        {
            const string value = "😼";

            var bytes = Utilities.GetMessageBytes(value, Encoding.Unicode);
            Utilities.PrintBytes("Unicode", bytes);
        }

        [Test]
        public void ShouldPrint2()
        {
            const string value = " T h a n k s   y o u  Ø&lt;ß9";

            var bytes = Utilities.GetMessageBytes(value, Encoding.Unicode);
            Utilities.PrintBytes("Unicode", bytes);
            bytes = Utilities.GetMessageBytes(value, Encoding.BigEndianUnicode);
            Utilities.PrintBytes("BEUnico", bytes);
        }
    }
}