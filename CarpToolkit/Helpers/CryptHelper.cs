namespace CarpToolkit.Helpers
{
    public class CryptHelper
    {
        public static string EncryptString(string input)
        {
            char[] charArray = input.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = (char)(charArray[i] + 1);
            }
            return new string(charArray);
        }

        public static string DecryptString(string input)
        {
            char[] charArray = input.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = (char)(charArray[i] - 1);
            }
            return new string(charArray);
        }
    }
}
