using System;
using System.Text.RegularExpressions;

namespace ASNC.Helper
{
    internal static class InputHelper
    {
        public static readonly Regex OnlyNumbersRegEx = new("^[0-9]+$");

        public static string GetLengthAdjustedInput(string input, int maxLength)
        {
            if (input.Length > maxLength)
            {
                return input[..maxLength];
            }

            return input;
        }

        public static (bool IsNumber, long Result) GetRangedNumericInput(string input, int maxLength, long min, long max)
        {
            input = GetLengthAdjustedInput(input, maxLength);
            if (OnlyNumbersRegEx.IsMatch(input))
            {
                return (true, Math.Clamp(Convert.ToInt64(input), min, max));
            }

            return (false, 0);
        }
    }
}
