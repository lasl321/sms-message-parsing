﻿using System;
using System.IO;
using System.Xml;

namespace Spikes
{
    public class SmsPayloadParser : IDisposable
    {
        private readonly XmlReader _reader;
        private bool _inSenderElement;
        private bool _isIdElement;
        private bool _isRecipientElement;
        private bool _isTextElement;

        public SmsPayloadParser(Stream stream)
        {
            var settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true
            };

            _reader = XmlReader.Create(stream, settings);
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public SmsPayload Parse()
        {
            var payload = new SmsPayload();

            while (_reader.Read())
            {
                switch (_reader.NodeType)
                {
                    case XmlNodeType.Element:
                        HandleRequestElementStart(payload);
                        HandleRecipientElementStart();
                        HandleSenderElementStart();
                        HandleIdElementStart();
                        HandleTextElementStart();
                        break;
                    case XmlNodeType.Text:
                        HandleText(payload);
                        break;
                    case XmlNodeType.EndElement:
                        HandleRecipientElementEnd();
                        HandleSenderElementEnd();
                        HandleIdElementEnd();
                        HandleTextElementEnd();
                        break;
                }
            }

            return payload;
        }

        private void HandleRecipientElementStart()
        {
            if (_reader.Name == "recipient") { _isRecipientElement = true; }
        }

        private void HandleRecipientElementEnd()
        {
            if (_reader.Name == "recipient") { _isRecipientElement = false; }
        }

        private void HandleTextElementStart()
        {
            if (_reader.Name == "text") { _isTextElement = true; }
        }

        private void HandleTextElementEnd()
        {
            if (_reader.Name == "text") { _isTextElement = false; }
        }

        private void HandleIdElementStart()
        {
            if (_reader.Name == "id") { _isIdElement = true; }
        }

        private void HandleIdElementEnd()
        {
            if (_reader.Name == "id") { _isIdElement = false; }
        }

        private void HandleText(SmsPayload payload)
        {
            if (_inSenderElement && _isIdElement)
            {
                payload.SenderPhoneNumber = _reader.Value.Trim();
                return;
            }

            if (_isRecipientElement && _isIdElement)
            {
                payload.RecipientPhoneNumber = _reader.Value.Trim();
            }

            if (_isTextElement)
            {
                payload.SenderText = _reader.Value;
            }
        }

        private void HandleSenderElementStart()
        {
            if (_reader.Name == "sender") { _inSenderElement = true; }
        }

        private void HandleSenderElementEnd()
        {
            if (_reader.Name == "sender") { _inSenderElement = false; }
        }

        private void HandleRequestElementStart(SmsPayload payload)
        {
            if (_reader.Name == "request")
            {
                payload.Operation = _reader.GetAttribute("type");
            }
        }
    }
}