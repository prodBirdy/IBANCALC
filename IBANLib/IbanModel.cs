using System;
using System.Collections.Generic;
using System.Numerics;

namespace IBANLib
{
    public class Iban
    {
        static Dictionary<string, int> ibanLengths = new Dictionary<string, int>
        {
            { "EG", 29 }, { "AL", 28 }, { "DZ", 24 }, { "AD", 24 }, { "AO", 25 },
            { "AZ", 28 }, { "BH", 22 }, { "BY", 28 }, { "BE", 16 }, { "BJ", 28 },
            { "BA", 20 }, { "BR", 29 }, { "VG", 24 }, { "BG", 22 }, { "BF", 27 },
            { "BI", 27 }, { "CR", 22 }, { "CI", 28 }, { "DK", 18 }, { "DE", 22 },
            { "DO", 28 }, { "SV", 28 }, { "EE", 20 }, { "FK", 18 }, { "FO", 18 },
            { "FI", 18 }, { "FR", 27 }, { "GA", 27 }, { "GE", 22 }, { "GI", 23 },
            { "GR", 27 }, { "GL", 18 }, { "GT", 28 }, { "IQ", 23 }, { "IR", 26 },
            { "IE", 22 }, { "IS", 26 }, { "IL", 23 }, { "IT", 27 }, { "JO", 30 },
            { "CM", 27 }, { "CV", 25 }, { "KZ", 20 }, { "QA", 29 }, { "CG", 27 },
            { "XK", 20 }, { "HR", 21 }, { "KW", 30 }, { "LV", 21 }, { "LY", 25 },
            { "LB", 28 }, { "LI", 21 }, { "LT", 20 }, { "LU", 20 }, { "MG", 27 },
            { "ML", 28 }, { "MT", 31 }, { "MR", 27 }, { "MU", 30 }, { "MD", 24 },
            { "MC", 27 }, { "MN", 20 }, { "ME", 22 }, { "MZ", 25 }, { "NL", 18 },
            { "MK", 19 }, { "NI", 24 }, { "NO", 15 }, { "AT", 20 }, { "TL", 23 },
            { "PK", 24 }, { "PS", 29 }, { "PL", 28 }, { "PT", 25 }, { "RO", 24 },
            { "RU", 33 }, { "SM", 27 }, { "ST", 25 }, { "SA", 24 }, { "SE", 24 },
            { "CH", 21 }, { "SN", 28 }, { "RS", 22 }, { "SC", 31 }, { "SD", 18 },
            { "SK", 24 }, { "SI", 19 }, { "ES", 24 }, { "LC", 32 }, { "CZ", 24 },
            { "TN", 24 }, { "TR", 26 }, { "UA", 29 }, { "HU", 28 }, { "VA", 22 },
            { "AE", 23 }, { "GB", 22 }, { "CY", 28 }, { "CF", 27 }
        };

        public static Dictionary<string, int> Alphabet = new Dictionary<string, int>
        {
            { "A", 10 }, { "B", 11 }, { "C", 12 }, { "D", 13 }, { "E", 14 },
            { "F", 15 }, { "G", 16 }, { "H", 17 }, { "I", 18 }, { "J", 19 },
            { "K", 20 }, { "L", 21 }, { "M", 22 }, { "N", 23 }, { "O", 24 },
            { "P", 25 }, { "Q", 26 }, { "R", 27 }, { "S", 28 }, { "T", 29 },
            { "U", 30 }, { "V", 31 }, { "W", 32 }, { "X", 33 }, { "Y", 34 },
            { "Z", 35 }
        };

        public static string ConvertIBANToNumberString(string iban)
        {
            string rearrangedIban = iban.Substring(4) + iban.Substring(0, 4);
            string numberString = "";

            foreach (char c in rearrangedIban)
            {
                if (Char.IsLetter(c))
                {
                    numberString += Alphabet[c.ToString()];
                }
                else
                {
                    numberString += c;
                }
            }

            return numberString;
        }

        public static string ValidateIBAN(string iban)
        {
            if (checkLength(iban))
            {
                string numberString = ConvertIBANToNumberString(iban);
                BigInteger ibanNumber = BigInteger.Parse(numberString);
                if (ibanNumber % 97 == 1)
                {
                    return "The IBAN is valid.";
                }
                else
                {
                    var correctedIban = CalculateCorrectChecksum(iban).ToCharArray();
                    // add a whitespace every 4 chars
                    for (int i = 4; i < correctedIban.Length; i += 5)
                    {
                        correctedIban[i] = ' ';
                    }
                    
                    return $"The IBAN is invalid. Corrected IBAN: {correctedIban}";
                }
            }
            return "The IBAN length is invalid.";
        }

        public static bool checkLength(string iban)
        {
            string countryCode = iban.Substring(0, 2);
            return iban.Length == ibanLengths[countryCode];
        }

        public static string CalculateCorrectChecksum(string iban)
        {
            string modifiedIban = iban.Substring(0, 2) + "00" + iban.Substring(4);
            string numberString = ConvertIBANToNumberString(modifiedIban);
            BigInteger ibanNumber = BigInteger.Parse(numberString);
            int checksum = 98 - (int)(ibanNumber % 97);
            string checksumStr = checksum.ToString("D2"); // Ensure two digits

            return iban.Substring(0, 2) + checksumStr + iban.Substring(4);
        }
    }
}
