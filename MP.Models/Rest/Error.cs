using System.Runtime.Serialization;

namespace MP.Models.Rest
{
    [DataContract(Namespace = "")]
    public class Error
    {
        private string _stack = null;

        [DataMember(Order = 0)]
        public ErrorLevel Level { get; set; }

        [DataMember(Order = 1)]
        public ErrorCode Code { get; set; }

        [DataMember(Order = 2)]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 3)]
        public string Exception { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 4)]
        public string Stack
        {
            get
            {
                if (Settings.DebugMode)
                {
                    return _stack;
                }

                return null;
            }

            set
            {
                _stack = value;
            }
        }
    }
}
