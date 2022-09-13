namespace GODCommon.Extensions;

public static class DocumentExtensions
{
        public static bool IsValidDocument(this string document) 
            => ValidNatural(document) || ValidLegal(document);

        public static bool ValidNatural(this string document)
        {
            var m1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var m2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (document.Length != 11)
                return false;

            for (var j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == document)
                    return false;

            var tempDocument = document[..9];
            
            var sum = 0;
            for (var i = 0; i < 9; i++)
                sum += int.Parse(tempDocument[i].ToString()) * m1[i];

            var rest = sum % 11;
                rest = sum % 11 < 2 ? 0 : 11 - rest;

            var digit = rest.ToString();
            tempDocument += digit;
            
            sum = 0;
            for (var i = 0; i < 10; i++)
                sum += int.Parse(tempDocument[i].ToString()) * m2[i];

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;

            return document.EndsWith(digit + rest);
        }

        public static bool ValidLegal(this string document)
        {
            var m1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var m2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (document.Length != 14)
                return false;

            var tempDocument = document[..12];
            var sum = 0;
            for (var i = 0; i < 12; i++)
                sum += int.Parse(tempDocument[i].ToString()) * m1[i];

            var rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;

            var digit = rest.ToString();
            tempDocument += digit;
            
            sum = 0;
            for (var i = 0; i < 13; i++)
                sum += int.Parse(tempDocument[i].ToString()) * m2[i];

            rest = (sum % 11);
            rest = rest < 2 ? 0 : 11 - rest;

            return document.EndsWith(digit + rest);
        }
}