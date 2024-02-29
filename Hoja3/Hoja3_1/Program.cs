using System.Numerics;

namespace Hoja3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Complejo complejo = new Complejo();
            Complejo complejo1 = new Complejo();

            complejo.ReadComplejo();
            complejo1.ReadComplejo();

            Complejo complejo3 = complejo + complejo1;
            Complejo complejo4 = complejo - complejo1;
            Complejo complejo5 = complejo * complejo1;
   
            Console.WriteLine("Suma:");
            complejo3.WriteComplejo();

            Console.WriteLine("\nResta:");
            complejo4.WriteComplejo();

            Console.WriteLine("\nMultiplicación:");
            complejo5.WriteComplejo();
        }
    }
}
