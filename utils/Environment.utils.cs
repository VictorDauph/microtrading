namespace microTrading.utils
{
    public static class EnvironmentUtils
    {
        public static string getEnvironmentVariable(string key)
        {
            string? value = Environment.GetEnvironmentVariable(key);
            if (value != null) {
                return value;
            }
            else
            {
                return "";
            }
        }
    }
}
