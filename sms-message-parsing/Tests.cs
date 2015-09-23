using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace Spikes.SMSMessageParsing
{
    public class Tests
    {
        private const string Message = "abcdefghijklmnopqrstuvwxyz";

        [Test]
        public void ShouldEncode()
        {
            var bytes = Utilities.GetMessageBytes(Message, Encoding.ASCII);
            Utilities.PrintBytes("ASCII", bytes);
            bytes = Utilities.GetMessageBytes(Message, Encoding.UTF8);
            Utilities.PrintBytes("UTF8", bytes);
            bytes = Utilities.GetMessageBytes(Message, Encoding.GetEncoding("iso-8859-1"));
            Utilities.PrintBytes("Latin 1", bytes);
            bytes = Utilities.GetMessageBytes(Message, Encoding.Unicode);
            Utilities.PrintBytes("Unicode", bytes);
            bytes = Utilities.GetMessageBytes(Message, Encoding.BigEndianUnicode);
            Utilities.PrintBytes("Big End", bytes);
        }

        [Test]
        public void ShouldDecodeInUtf8FromLatin1()
        {
            var bytes = Utilities.GetMessageBytes(Message, Encoding.GetEncoding("iso-8859-1"));
            using (var reader = new StreamReader(new MemoryStream(bytes)))
            {
                var contents = reader.ReadToEnd();
                Console.WriteLine(contents);
                CreateXmlDocument(contents);
            }
        }

        [Test]
        public void ShouldDecodeInUtf8FromUnicode()
        {
            var bytes = Utilities.GetMessageBytes(Message, Encoding.Unicode);
            using (var reader = new StreamReader(new MemoryStream(bytes)))
            {
                var contents = reader.ReadToEnd();
                Console.WriteLine(contents);
                CreateXmlDocument(contents);
            }
        }

        [Test]
        public void ShouldDecodeInUtf8FromBigEndianUnicode()
        {
            var bytes = Utilities.GetMessageBytes(Message, Encoding.BigEndianUnicode);
            using (var reader = new StreamReader(new MemoryStream(bytes)))
            {
                var contents = reader.ReadToEnd();
                Console.WriteLine(contents);
                CreateXmlDocument(contents);
            }
        }

        private static void CreateXmlDocument(string contents)
        {
            string xml = $"<?xml version=\"1.0\"?><foo>{contents}</foo>";
            Console.WriteLine($"XML: {xml}");
            var document = new XmlDocument();
            document.LoadXml(xml);
        }
    }
}