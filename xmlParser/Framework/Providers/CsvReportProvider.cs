using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xmlParser.Framework.Interfaces;

namespace xmlParser.Framework.Providers
{
	class CsvReportProvider : IReportPorvider
	{
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
