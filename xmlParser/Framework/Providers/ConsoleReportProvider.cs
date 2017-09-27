using System;
using System.Collections.Generic;
using System.Linq;
using xmlParser.Framework.DataProcessors;
using xmlParser.Framework.Interfaces;

namespace xmlParser.Framework.Providers
{
	/// <summary>Провайдер отчёта в консоли.</summary>
	class ConsoleReportProvider : IReportPorvider
	{
		/// <summary>Сформировать отчёт.</summary>
		/// <param name="counter">Данные отчёта.</param>
		public void Write(IReadOnlyList<CountryStatisticCounter> counters)
		{
			Console.ForegroundColor = ConsoleColor.Magenta;

			Console.WriteLine();
			Console.WriteLine("Countries total: " + counters.Count);

			Console.ForegroundColor = ConsoleColor.DarkYellow;

			foreach(var counter in counters)
			{
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Total plates: " + counter.TotalPlatesReaded);
				//Console.ForegroundColor = ConsoleColor.DarkYellow;
				//foreach(var tag in tagGroup)
				//{
				//	Console.WriteLine(tag.DisplayName + ": " + tag.Count);
				//}
			}

			Console.ResetColor();
		}
	}
}
