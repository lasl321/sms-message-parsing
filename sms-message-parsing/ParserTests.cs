using System.IO;
using NUnit.Framework;

namespace Spikes.SMSMessageParsing
{
    public class ParserTests
    {
        [Test]
        public void ShouldParseMessageXml()
        {
            var stream = new FileStream("sample-message-sms.xml", FileMode.Open);
            SmsPayload payload;
            using (var parser = new SmsPayloadParser(stream))
            {
                payload = parser.Parse();
            }

            Assert.That(payload.Operation, Is.EqualTo("MESSAGE"));
            Assert.That(payload.SenderPhoneNumber, Is.EqualTo("9496132979"));
            Assert.That(payload.SenderText, Is.EqualTo("This is a test"));
        }

        [Test]
        public void ShouldParseBlockXml()
        {
            var stream = new FileStream("sample-block-sms.xml", FileMode.Open);
            SmsPayload payload;
            using (var parser = new SmsPayloadParser(stream))
            {
                payload = parser.Parse();
            }

            Assert.That(payload.Operation, Is.EqualTo("BLOCK"));
            Assert.That(payload.RecipientPhoneNumber, Is.EqualTo("5555555555"));
        }
    }
}