using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace IBANLib;

/// <summary>
/// Diese Klasse implementiert grundlegende RSA-Verschlüsselung und -Entschlüsselung.
/// </summary>
public static class Rsa
{
    /// <summary>
    /// Wandelt einen String in ein Array von ASCII-Zahlen um.
    /// </summary>
    /// <param name="message">Der zu konvertierende String.</param>
    /// <returns>Ein Array von ASCII-Zahlen.</returns>
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

    /// <summary>
    /// Verschlüsselt eine Nachricht mit dem angegebenen öffentlichen Schlüssel (e, n).
    /// </summary>
    /// <param name="message">Die zu verschlüsselnde Nachricht.</param>
    /// <param name="e">Der öffentliche Schlüssel Exponent.</param>
    /// <param name="n">Der Modul des öffentlichen Schlüssels.</param>
    /// <returns>Eine Liste von BigInteger, die die verschlüsselte Nachricht repräsentiert.</returns>
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

    /// <summary>
    /// Entschlüsselt eine verschlüsselte Nachricht mit dem angegebenen privaten Schlüssel (d, n).
    /// </summary>
    /// <param name="message">Die zu entschlüsselnde Nachricht als Liste von BigInteger.</param>
    /// <param name="d">Der private Schlüssel Exponent.</param>
    /// <param name="n">Der Modul des privaten Schlüssels.</param>
    /// <returns>Die entschlüsselte Nachricht als String.</returns>
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

    /// <summary>
    /// Generiert ein Paar von öffentlichen und privaten Schlüsseln.
    /// </summary>
    /// <param name="p">Eine Primzahl.</param>
    /// <param name="q">Eine andere Primzahl.</param>
    /// <returns>Ein Array, das den öffentlichen Schlüssel Exponent (e), den privaten Schlüssel Exponent (d) und den Modul (n) enthält.</returns>
    public static int[] GenerateKeys(int p, int q)
    {
        int n = p * q;
        int phi = (p - 1) * (q - 1);
        int e = phi - 1;

        while (e > 1 && Euklid.Gcd(e, phi) != 1)
        {
            e--;
        }

        int d = 2;
        while (Modulo.Mod(d * e, phi) != 1)
        {
            d++;
        }

        return new int[] { e, d, n };
    }

    /// <summary>
    /// Berechnet (baseValue^exponent) % modulus effizient.
    /// </summary>
    /// <param name="baseValue">Die Basis.</param>
    /// <param name="exponent">Der Exponent.</param>
    /// <param name="modulus">Der Modul.</param>
    /// <returns>Das Ergebnis der Modulo-Exponentiation.</returns>
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
