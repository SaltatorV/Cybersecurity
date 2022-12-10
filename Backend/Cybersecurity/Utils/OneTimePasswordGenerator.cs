namespace Cybersecurity.Utils
{
    public static class OneTimePasswordGenerator
    {
        private static int min = 1;
        private static int max = 10;
        public static string generate(int idLength)
        {
            int random = Random.Shared.Next(min, max);
            double value = Math.Exp(-idLength * random);
            string oneTimePassword = value.ToString("E5");

            return oneTimePassword;
        }
    }
}
