namespace Spikes
{
    public class SmsPayload
    {
        private string _operation;
        private string _recipientPhoneNumber;
        private string _senderPhoneNumber;

        public string Operation
        {
            get { return _operation.ToUpperInvariant(); }
            set { _operation = value; }
        }

        public string SenderPhoneNumber
        {
            get { return FormatPhoneNumber(_senderPhoneNumber); }
            set { _senderPhoneNumber = value; }
        }

        public string SenderText { get; set; }

        public string RecipientPhoneNumber
        {
            get { return FormatPhoneNumber(_recipientPhoneNumber); }
            set { _recipientPhoneNumber = value; }
        }

        private static string FormatPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Replace("+1", string.Empty);
        }
    }
}