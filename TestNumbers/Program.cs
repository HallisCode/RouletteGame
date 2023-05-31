using System.Numerics;
using System.Security.Cryptography;

namespace TestNumbers
{
	internal class Program
	{
		public static readonly int[] dozen1 = Enumerable.Range(1, 12).ToArray();

		public static readonly int[] dozen2 = Enumerable.Range(13, 12).ToArray();

		public static readonly int[] dozen3 = Enumerable.Range(25, 12).ToArray();

		static void Main(string[] args)
		{
			int Dozen1 = 0;
			int Dozen2 = 0;
			int Dozen3 = 0;

			for (int i = 0; i < 100000000; i++)
			{
				int number = RandomNumberGenerator.GetInt32(0, 37);


				if (dozen1.Contains(number))
				{
					Dozen1++;
				}
				else if (dozen2.Contains(number))
				{
					Dozen2++;
				}
				else  if (dozen3.Contains(number))
				{
					Dozen3++;
				}

			}

			Console.WriteLine($"Dozen1 - {Dozen1}\nDozen2 - {Dozen2}\nDozen3 - {Dozen3}\n");
		}
	}
}