namespace Checkwork.BusinessComponents.Helpers
{
    using Microsoft.IdentityModel.Tokens;

    public class StringHelper
    {
        public static string CombinSignature(string surnamr, string? name, string? patronymic)
        {
            return $"{surnamr} {GetInitial(name)}{GetInitial(patronymic)}";
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
