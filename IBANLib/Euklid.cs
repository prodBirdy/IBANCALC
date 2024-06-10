namespace IBANLib;

public class Euklid
{
    // This method calculates the greatest common divisor (GCD) of two numbers
    public static int Gcd(int a, int b)
    {
        // While b is not zero
        while (b != 0)
        {
            // Store the value of b in a temporary variable
            int temp = b;
            // Set b to the remainder of the division of a by b
            b = a % b;
            // Set a to the value of the temporary variable (the old value of b)
            a = temp;
        }
        // Return the value of a, which is now the GCD of the original a and b
        return a;
    }
}