using System.Text;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class HexadecimalController : IHexadecimalController
    {
        public string ConvertAsciiToHexadecimal(string asciiString)
        {
            if (string.IsNullOrEmpty(asciiString))
            {
                throw new ArgumentException("The ASCII string cannot be null or empty.");
            }

            char[] chars = asciiString.ToCharArray();
            StringBuilder hex = new StringBuilder();

            foreach (char c in chars)
            {
                hex.AppendFormat("{0:X2}", (int)c);
            }

            string hexString = hex.ToString();
            return hexString;
        }
    }
}