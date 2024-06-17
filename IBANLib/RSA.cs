using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace IBANLib;

public class RSA
{
    // message to char array and then every char to ascii number return char array
    public static int[] StringToNumber(string message)
    {
        char[] charArray = message.ToCharArray();
        int[] numberArray = new int[charArray.Length];
        for (int i = 0; i < charArray.Length; i++)
        {
            numberArray[i] = (int)charArray[i];
        }

        return numberArray;
    }

    public static List<BigInteger> Encrypt(string message, int e, int n)
    {
        var numberArray = StringToNumber(message);
        List<BigInteger> encryptedMessage = new List<BigInteger>();
        foreach (int number in numberArray)
        {
            encryptedMessage.Add(BigInteger.ModPow(number, e, n));
        }

        return encryptedMessage;
    }

public static string Decrypt(List<BigInteger> message, int d, int n)
{
    string decryptedMessage = "";
    foreach (var number in message)
    {
        var num = BigInteger.ModPow(number, d, n);
        decryptedMessage += (char)(int)num;
    }

    return decryptedMessage;
}

    public static int[] GenerateKeys(int p, int q)
    {
        int n = p * q;
        int phi = (p - 1) * (q - 1);
        int e = 2;
        while (Euklid.Gcd(e, phi) != 1)
        {
            e++;
        }

        int d = 2;
        while (Modulo.Mod(d * e, phi) != 1)
        {
            d++;
        }

        return new int[] { e, d, n };
    }
    
    public static BigInteger ModPow(BigInteger baseValue, BigInteger exponent, BigInteger modulus)
    {
        BigInteger result = 1;
        while (exponent > 0)
        {
            if (exponent % 2 == 1)
                result = (result * baseValue) % modulus;
            exponent = exponent >> 1;
            baseValue = (baseValue * baseValue) % modulus;
        }
        return result;
    }
}

