using System.Linq;

namespace TrackPing
{
    class Parse
    {
        public bool ValidateIPv4(string ipString)
        {
            if (string.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }
            byte parsedByte;

            return splitValues.All(r => byte.TryParse(r, out parsedByte));
        }
    }
}
