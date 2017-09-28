using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using xmlParser.Framework.DataProcessors;
using xmlParser.Framework.Entities;
using xmlParser.Framework.Providers;
using xmlParser.Framework.Writers;

namespace xmlParser
{
	/// <summary>Обработчик данных.</summary>
	class DataProcessor
	{
		private readonly ReportWriter _reportWriter;

		public DataProcessor()
		{
			_reportWriter = new ReportWriter(new ConsoleReportProvider());
		}

		/// <summary>Обработать данные.</summary>
		/// <param name="dataPath">Путь к каталогу с данными.</param>
		/// <param name="dataProcessor">Обработчик данных.</param>
		public void ProcessData(string dataPath, string imagePath)
		{
			var files = Directory.EnumerateFiles(dataPath);

			Console.WriteLine("Files to read: " + files.Count());

			var tasksList = new List<Task>();

			foreach(var file in files)
			{
				Console.WriteLine();
				Console.WriteLine("Reading file: " + file);

				string xmlString = File.ReadAllText(file);

				var xdoc = XDocument.Load(new StringReader(xmlString));

				var xmlList = xdoc.Descendants("plate");

				var dataStorage = new CountryReportData();

				tasksList.Add(Task.Run(() =>
				{
					var countryStatisticCounter = new CountryStatisticCounter(imagePath, dataStorage);

					countryStatisticCounter.Process(xmlList);

					_reportWriter.Storages.Add(dataStorage);

					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(file + " done.");
					Console.ResetColor();

				}));
			}

			Task.WaitAll(tasksList.ToArray());
			_reportWriter.Write();

			new ReportWriter(new CsvReportProvider()) { Storages = _reportWriter.Storages }.Write();
		}
	}
}
