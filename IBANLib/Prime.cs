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
    
}
