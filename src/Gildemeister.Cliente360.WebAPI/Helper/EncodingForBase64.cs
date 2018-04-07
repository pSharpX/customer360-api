using System.Text;

namespace Gildemeister.Cliente360.WebAPI.Helper
{
    public static class EncodingForBase64
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string plainText)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(plainText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
