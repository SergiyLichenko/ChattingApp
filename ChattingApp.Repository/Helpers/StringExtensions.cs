using System;

namespace ChattingApp.Repository.Helpers
{
    public static class StringExtensions
    {
        public static bool IsUrl(this string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException(nameof(input));

            var result = Uri.TryCreate(input, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }
    }
}