using System.Linq;


namespace RouletteLib
{
	/// <summary>
	/// Содержит в себе массивы с ячейками разбитыми по категориям.
	/// </summary>
	public static class Cells
	{
		public static readonly int[] all = new int[] { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26 };

		public static readonly int[] red = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 16, 21, 23, 25, 27, 30, 32, 34, 36 };

		public static readonly int[] black = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

		public static readonly int[] green = { 0 };


		public static readonly int[] smallFigures = Enumerable.Range(1, 18).ToArray();

		public static readonly int[] largeFigures = Enumerable.Range(19, 18).ToArray();


		public static readonly int[] dozen1 = Enumerable.Range(1, 12).ToArray();

		public static readonly int[] dozen2 = Enumerable.Range(13, 12).ToArray();

		public static readonly int[] dozen3 = Enumerable.Range(25, 12).ToArray();


		public static readonly int[] column1 = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };

		public static readonly int[] column2 = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };

		public static readonly int[] column3 = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
	}
}
