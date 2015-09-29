using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Spikes.SMSMessageParsing
{
    public class EmojiTests
    {
        [Test]
        public void ShouldPrintHappyFace()
        {
            var bytes = new byte[]
            {
                0xF0,
                0x9F,
                0x98,
                0x81
            };

            var stream = new MemoryStream(bytes);
            using (var reader = new StreamReader(stream))
            {
                var s = reader.ReadToEnd();
                Console.WriteLine(s);
            }
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