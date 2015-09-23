using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Spikes.SMSMessageParsing
{
    internal static class Utilities
    {
        public static byte[] GetMessageBytes(string message, Encoding encoding)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, encoding))
            {
                writer.Write(message);
            }

            return stream.ToArray();
        }

        public static void PrintBytes(string encodingName, IEnumerable<byte> messageBytes)
        {
            Console.Write("{0,-10}", encodingName);
            foreach (var b in messageBytes)
            {
                Console.Write("{0:x} ", b);
            }
            Console.WriteLine();
        }
    }
}