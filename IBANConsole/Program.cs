using System;
using IBANLib;

while (true)
{
    Console.WriteLine("Enter mode (1 for Euklid, 2 for Prime, 3 for Iban, 4 for modulo):");
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
            Console.WriteLine("Enter two number:");
            var x = int.Parse(Console.ReadLine());
            var y = int.Parse(Console.ReadLine());
            var factorsX = Prime.PrimeFactorization(x);
            var factorsY = Prime.PrimeFactorization(y);
            Console.WriteLine($"The prime factors of {x} are: {string.Join(", ", factorsX)}");
            Console.WriteLine($"The prime factors of {y} are: {string.Join(", ", factorsY)}");

            //Compare the lists of prime factors
            var commonFactors = factorsX.Intersect(factorsY);
            //multiply the common Factors inside of the list
            if (commonFactors.Any())
            {
                var product = commonFactors.Aggregate((total, next) => total * next);
                Console.WriteLine($"The product of the common factors is {product}.");
            }
            else
            {
                Console.WriteLine("There are no common factors.");
            }

            break;

        case "3":
            Console.WriteLine("You have entered Iban Mode");
            Console.WriteLine("Enter the IBAN:");
            var iban = Console.ReadLine();
            
            var isValid = Iban.ValidateIBAN(iban);

                Console.WriteLine(isValid);
            
            break;
        case "4":
            Console.WriteLine("You have entered Modulo Mode");
            Console.WriteLine("Enter two numbers:");
            var modA = int.Parse(Console.ReadLine());
            var modB = int.Parse(Console.ReadLine());
            var modulo = Modulo.Mod(modA, modB);
            Console.WriteLine($"The modulo of {modA} and {modB} is {modulo}.");
            break;
        
        case "exit":
            
        default:
            Console.WriteLine("Invalid mode. Please enter 1 for Euklid or 2 for Prime.");
            break;
    }
}