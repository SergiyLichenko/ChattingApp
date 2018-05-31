using System;

namespace ChattingApp.Repository.Helpers
{
    public static class StringExtensions
    {
        private const string ImageHeader = "data:image";

        public static bool IsUrl(this string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException(nameof(input));

            var result = Uri.TryCreate(input, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        public static string RemoveBase64Header(this string input)
        {
            if(string.IsNullOrEmpty(input)) throw new ArgumentException(nameof(input));

            if (input.Contains(ImageHeader))
                input = input.Remove(0, input.IndexOf(",", StringComparison.Ordinal) + 1);

            return input;
        }
    }
}