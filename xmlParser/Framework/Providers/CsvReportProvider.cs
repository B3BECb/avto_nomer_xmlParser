using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using xmlParser.Framework.Interfaces;

namespace xmlParser.Framework.Providers
{
	/// <summary>Провайдер отчёта в csv файл.</summary>
	class CsvReportProvider : IReportPorvider
	{
		/// <summary>Сформировать отчёт.</summary>
		/// <param name="counter">Данные отчёта.</param>
		public void Write(IReadOnlyList<IDataStorage> storages)
		{

			foreach(var storage in storages)
			{
				var path = $"imageStorage\\{storage.StorageName}\\";

				if(!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				var file = path + "Statistic.csv";

				try
				{
					using(StreamWriter sw = new StreamWriter(file, false, Encoding.GetEncoding("Windows-1251")))
					{
						sw.WriteLine($"Countries total,{storages.Count}");
						sw.WriteLine();


						var strorageReport = storage.GetStorageText("{0},{1}");
						foreach(var text in strorageReport)
						{
							if(text.NewLine)
								sw.WriteLine();
							else
								sw.WriteLine(text);
						}

						sw.WriteLine();
					}
				}
				catch(Exception exc)
				{
					using(var sw = File.AppendText(path + "Error.log"))
					{
						sw.WriteLine(exc);
					}
				}
			}
		}
	}
}
