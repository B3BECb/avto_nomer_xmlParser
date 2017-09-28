using System;
using System.Collections.Generic;

using xmlParser.Framework.Interfaces;

namespace xmlParser.Framework.Providers
{
	/// <summary>Провайдер отчёта в консоли.</summary>
	class ConsoleReportProvider : IReportPorvider
	{
		/// <summary>Сформировать отчёт.</summary>
		/// <param name="counter">Данные отчёта.</param>
		public void Write(IReadOnlyList<IDataStorage> storages)
		{
			Console.ForegroundColor = ConsoleColor.Magenta;

			Console.WriteLine();
			Console.WriteLine("Countries total: " + storages.Count);
			Console.WriteLine();
			
			Console.ResetColor();

			foreach(var storage in storages)
			{
				var strorageReport = storage.GetStorageText();
				foreach(var text in strorageReport)
				{
					Console.ForegroundColor = text.TextColor;
					if(text.NewLine)
						Console.WriteLine();
					else
						Console.WriteLine(text);
					Console.ResetColor();
				}

				Console.WriteLine();
			}
		}
	}
}
