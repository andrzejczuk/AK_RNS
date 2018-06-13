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

    private static MRS RnsToMrs()
    {

    }
  }

  class MRS
  {

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
