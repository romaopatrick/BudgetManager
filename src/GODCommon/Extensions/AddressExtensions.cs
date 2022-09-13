using System;

namespace GODCommon.Extensions;

public static class AddressExtensions
{
    public static bool ValidZipCode(this string zipCode)
    {
            if (zipCode.Length == 8)
                zipCode = string.Concat(zipCode[..5], "-", zipCode.AsSpan(5, 3));
            
            return System.Text.RegularExpressions.Regex.IsMatch(zipCode, ("[0-9]{5}-[0-9]{3}"));
    }
}