using System;

namespace RNS_Proc
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Wprowadź liczbę X:");
            double x = double.Parse(Console.ReadLine());

            Console.WriteLine("Wprowadź liczbę Y:");
            double y = double.Parse(Console.ReadLine());

            RNS rnsX = ConvertToRNS(x);
            RNS rnsY = ConvertToRNS(y);
        }

        private static RNS ConvertToRNS(double number)
        {
            int Rf = RNS.CalculateFractionalRange();
            int machineValue = CalculateMachineValue(number, Rf);

            var values = new int[RNS.ModulusCount];

            for (int i = 0; i < RNS.ModulusCount; i++)
            {
                values[i] = machineValue % RNS.Modulus[i];
            }

            return new RNS()
            {
                Values = values
            };
        }

        private static int CalculateMachineValue(double number, int rf)
        {
            return  (int)(number * (double)rf);    
        }

    }

    class RNS
    {
        public static int[] Modulus => new[] { 4, 3, 5, 7, 11, 13, 17, 19 };
        public static int fractionalModulusCount => 4;
        public static int ModulusCount => 8;
        public int[] Values { get; set; }

        public static int CalculateFractionalRange()
        {
            int Rf = Modulus[0];

            for (int i = 1; i < fractionalModulusCount; i++)
            {
                Rf *= Modulus[i];
            }

            return Rf;
        }


    }
}
