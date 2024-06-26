﻿using System;
using IBANLib;

while (true)
{
    // TODO: Add ACSCII logo
    
    Console.WriteLine("1 for Euklid");
    Console.WriteLine("2 for Prime");
    Console.WriteLine("3 for Iban");
    Console.WriteLine("4 for modulo");
    Console.WriteLine("5 for RSA");
    var mode = Console.ReadLine();

    switch (mode)
    {
        case "1":
            Console.WriteLine("You've entered Euklid mode.");
            Console.WriteLine("Enter two numbers:");
            var a = int.Parse(Console.ReadLine());
            var b = int.Parse(Console.ReadLine());
            var gcd = Euklid.Gcd(a, b);
            Console.WriteLine($"The greatest common divisor of {a} and {b} is {gcd}.");
            break;

        case "2":
            Console.WriteLine("You've entered Prime mode.");
            Console.WriteLine("Enter numbers separated by commas (e.g., 1,2,5):");
            var input = Console.ReadLine();
            if (input != null)
            {
                var numbers = input.Split(',').Select(int.Parse).ToList();

                var factors = new Dictionary<int, List<int>>();
                foreach (var number in numbers)
                {
                    factors[number] = Prime.PrimeFactorization(number);
                    Console.WriteLine($"The prime factors of {number} are: {string.Join(", ", factors[number])}");
                }

                for (int i = 0; i < numbers.Count; i++)
                {
                    for (int j = i + 1; j < numbers.Count; j++)
                    {
                        var commonFactors = factors[numbers[i]].Intersect(factors[numbers[j]]);
                        if (commonFactors.Any())
                        {
                            var product = commonFactors.Aggregate((total, next) => total * next);
                            Console.WriteLine(
                                $"The product of the common factors of {numbers[i]} and {numbers[j]} is {product}.");
                        }
                        else
                        {
                            Console.WriteLine($"There are no common factors between {numbers[i]} and {numbers[j]}.");
                        }
                    }
                }
            }

            break;

        case "3":
            Console.WriteLine("You have entered Iban Mode");
            Console.WriteLine("Enter the IBAN:");
            
            var iban = Console.ReadLine();
            if (iban != null)
            {
                iban = iban.Trim();
                var isValid = Iban.ValidateIBAN(iban);
                
                Console.WriteLine(isValid);
            }

            break;
        case "4":
            Console.WriteLine("You have entered Modulo Mode");
            Console.WriteLine("Enter two numbers:");
            var modA = int.Parse(Console.ReadLine());
            var modB = int.Parse(Console.ReadLine());
            var modulo = Modulo.Mod(modA, modB);
            Console.WriteLine($"The modulo of {modA} and {modB} is {modulo}.");
            break;

        case "5":
            Console.WriteLine("You have entered RSA Mode");
            Console.WriteLine("Do you have e? (y/n)");
            var response = Console.ReadLine();
            int[] keys;
            if (response.ToLower() == "y")
            {
                Console.WriteLine("Enter e:");
                var e = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter p:");
                var p = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter q:");
                var q = int.Parse(Console.ReadLine());
                keys = Rsa.GenerateKeys(e, p, q);

            }
            else
            {
                Console.WriteLine("Enter two prime numbers:");
                var p = int.Parse(Console.ReadLine());
                var q = int.Parse(Console.ReadLine());
                keys = Rsa.GenerateKeys(p, q);

            }
            Console.WriteLine($"Public key: {keys[0]}, Private key: {keys[1]}, N: {keys[2]}");
            Console.WriteLine("Enter a message to encrypt:");
            var message = Console.ReadLine();
            var encrypted = Rsa.Encrypt(message, keys[0], keys[2]);
            foreach (var c in encrypted)
            {
                Console.Write($"{c} ");
            }
            var decrypted = Rsa.Decrypt(encrypted, keys[1], keys[2]);
            Console.WriteLine($"Decrypted message: {decrypted}");
            break;
        case "exit":

        default:
            Console.WriteLine("Invalid mode");
            break;
    }
}