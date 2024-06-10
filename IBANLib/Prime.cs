namespace IBANLib;

public class Prime
{
    // Diese Methode berechnet die Primfaktorzerlegung einer gegebenen Zahl
    public static List<int> PrimeFactorization(int number)
    {
        // Initialisiere eine Liste zur Speicherung der Faktoren
        var factors = new List<int>();

        // Schleife von 2 bis zur Quadratwurzel der Zahl
        for (int div = 2; div * div <= number; div++)
        {
            // Solange die Zahl durch den aktuellen Divisor teilbar ist
            while (number % div == 0)
            {
                // Füge den Divisor zur Liste der Faktoren hinzu
                factors.Add(div);
                // Teile die Zahl durch den Divisor
                number /= div;
            }
        }

        // Wenn die Zahl nach dem obigen Prozess größer als 1 ist
        if (number > 1)
        {
            // Füge die Zahl zur Liste der Faktoren hinzu
            factors.Add(number);
        }

        // Gib die Liste der Faktoren zurück
        return factors;
    }
    
    // Hole alle Primzahlen von 2 bis n
    public static List<int> GetPrimes(int n)
    {
        // Initialisiere eine Liste zur Speicherung der Primzahlen
        var primes = new List<int>();

        // Schleife von 2 bis n
        for (int i = 2; i <= n; i++)
        {
            // Nimm an, die Zahl ist prim
            bool isPrime = true;

            // Schleife durch alle Zahlen von 2 bis zur Quadratwurzel von i
            for (int j = 2; j * j <= i; j++)
            {
                // Wenn i durch j teilbar ist
                if (i % j == 0)
                {
                    // i ist nicht prim
                    isPrime = false;
                    break;
                }
            }

            // Wenn i prim ist
            if (isPrime)
            {
                // Füge i zur Liste der Primzahlen hinzu
                primes.Add(i);
            }
        }

        // Gib die Liste der Primzahlen zurück
        return primes;
    }
}
