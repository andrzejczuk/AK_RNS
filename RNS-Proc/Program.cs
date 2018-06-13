using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      RNS zpi = Mult(rnsX, rnsY);

      MRS mrs = RnsToMrs(zpi);

      RNS result = MrsToRns(mrs);
    }

    private static RNS ConvertToRNS(double number)
    {
      RNS rns = new RNS();
      int Rf = RNS.CalculateFractionalRange();
      int machineValue = CalculateMachineValue(number, Rf);

      var values = new int[RNS.ModulusCount];

      for (int i = 0; i < RNS.ModulusCount; i++)
      {
        values[i] = machineValue % RNS.Modulus[i];
      }

      rns.Values = values;
      rns.MachineValue = machineValue;

      return rns;
    }

    private static int CalculateMachineValue(double number, int rf)
    {
      return (int)(number * (double)rf);
    }

    private static RNS Mult(RNS rnsX, RNS rnsY)
    {
      RNS zpi = new RNS();

      int machineValueZpi = rnsX.MachineValue * rnsY.MachineValue;
      zpi.MachineValue = machineValueZpi;

      zpi.Values = new int[RNS.Modulus.Length];

      for (int i = 0; i < RNS.Modulus.Length; i++)
      {
        zpi.Values[i] = rnsX.Values[i] * rnsY.Values[i] % RNS.Modulus[i];
      }

      return zpi;
    }

    private static MRS RnsToMrs(RNS rns)
    {
            // TODO
			MRS mrs = new MRS();
            var zdigit = new int[RNS.Modulus.Length];

            zdigit[0] = rns.Values[0];
            int temp = 0;

            for (int i = 1; i < RNS.ModulusCount; i++)
            {
                temp = ModInverse(RNS.Modulus[0], RNS.Modulus[i]) * (rns.Values[i] - zdigit[0]);

                for (int j = 1; j <= i; j++)
                {
                    temp = ModInverse(RNS.Modulus[j], RNS.Modulus[i]) * (temp - zdigit[j]);
                }

                zdigit[i] = temp % RNS.Modulus[i];
            }
            mrs.Values = zdigit;
            mrs.Modulus = RNS.Modulus;

            return mrs;
    }

        private static RNS MrsToRns(MRS mrs)
        {
            RNS rns = new RNS();
            int k = RNS.fractionalModulusCount + 1;

            int value = mrs.Values[k - 1];

            for (int i = k ; i < RNS.Modulus.Length; i++)
            {
                int tempValue = mrs.Values[i];

                for (int j = k; j <= i; j++)
                {
                    tempValue *= RNS.Modulus[j - 1];
                }

                value += tempValue;
            }

            var rnsValue = new int[RNS.Modulus.Length];

            for (int i = 0; i < RNS.Modulus.Length; i++)
            {
                rnsValue[i] = value % RNS.Modulus[i];    
            }

            rns.Values = rnsValue;
            return rns;
        }

        private static int ModInverse(int a, int n)
        {
            int i = n, v = 0, d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }
  }

  class MRS
  {
        public int[] Modulus { get; set; }
        public int[] Values { get; set; }
  }

  class RNS
  {
    public static int[] Modulus => new[] { 4, 3, 5, 7, 11, 13, 17, 19 };
    public static int fractionalModulusCount => 4;
    public static int ModulusCount => 8;
    public int MachineValue { get; set; }
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
