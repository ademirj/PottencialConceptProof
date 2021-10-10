using System;
using System.Linq;

namespace Pottencial.Infrastructure.CrossCutting.Extension
{
    public static class StringExtension
    {
        public static long ToLong(this String value)
        {
            var numbers = new String(value.Where(Char.IsDigit).ToArray());

            return string.IsNullOrEmpty(numbers) ? 0 : long.Parse(numbers);
        }
    }
}
