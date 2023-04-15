using System.Linq;


namespace RouletteLib
{
	/// <summary>
	/// Содержит в себе массивы с ячейками разбитыми по категориям.
	/// </summary>
	public static class Cells
	{
		public static readonly int[] allCells = Enumerable.Range(0, 37).ToArray();

		public static readonly int[] redCells = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 16, 21, 23, 25, 27, 30, 32, 34, 36 };

		public static readonly int[] blackCells = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

		public static readonly int[] greenCells = { 0 };


		public static readonly int[] smallFigures = Enumerable.Range(1, 17).ToArray();

		public static readonly int[] largeFigures = Enumerable.Range(17, 37).ToArray();


		public static readonly int[] dozen1 = Enumerable.Range(1, 13).ToArray();

		public static readonly int[] dozen2 = Enumerable.Range(13, 25).ToArray();

		public static readonly int[] dozen3 = Enumerable.Range(25, 37).ToArray();


		public static readonly int[] column1 = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };

		public static readonly int[] column2 = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };

		public static readonly int[] column3 = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
	}
}
