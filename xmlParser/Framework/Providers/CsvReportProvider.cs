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
			var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\avtoNomer\\XmlParser\\";

			if(!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			try
			{
				foreach(var storage in storages)
				{
					var file = path + storage.StorageName + "Statistic.csv";
					using(StreamWriter sw = new StreamWriter(file, false, Encoding.GetEncoding("Windows-1251")))
					{
						sw.WriteLine($"\"Countries total\";\"{storages.Count}\"");
						sw.WriteLine();


						var strorageReport = storage.GetStorageText("\"{0}\";\"{1}\"");
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
