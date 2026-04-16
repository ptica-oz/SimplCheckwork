namespace Checkwork.BusinessComponents.Helpers
{
    using System;
    using Microsoft.IdentityModel.Tokens;

    public class StringHelper
    {
        public static string CombinSignature(string surnamr, string? name, string? patronymic)
        {
            return $"{surnamr} {GetInitial(name)}{GetInitial(patronymic)}";
        }

        public static string GetTime(string value)
        {
            try
            {
                var dateTime = DateTime.Parse(value);
                return dateTime.TimeOfDay.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string GetInitial(string? word)
        {
            if (word.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return $"{word[0]}.";
        }
    }
}
