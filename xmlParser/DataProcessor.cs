using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using xmlParser.Framework.Writers;
using xmlParser.Framework.Entities;
using xmlParser.Framework.Providers;
using xmlParser.Framework.DataProcessors;

namespace xmlParser
{
	/// <summary>Обработчик данных.</summary>
	class DataProcessor
	{
		#region Data

		private readonly ReportWriter _reportWriter;

		#endregion

		#region .ctor

		public DataProcessor()
		{
			_reportWriter = new ReportWriter(new ConsoleReportProvider());
		}

		#endregion

		#region Methods

		/// <summary>Обработать данные.</summary>
		/// <param name="dataPath">Путь к каталогу с данными.</param>
		/// <param name="dataProcessor">Обработчик данных.</param>
		public void ProcessData(string dataPath, string imagePath)
		{
			if(!Directory.Exists(dataPath))
			{
				Directory.CreateDirectory(dataPath);

				Console.ForegroundColor = ConsoleColor.Yellow;
				var errorMessage = $@"Directory ({dataPath}) does not exist.";
				Console.WriteLine($@"{errorMessage} Directory will be created. Put data files in directory and restart program.");
				Console.ResetColor();

				return;
			}

			var files = Directory.EnumerateFiles(dataPath);

			if(files.Count() == 0)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($@"Directory ({dataPath}) is empty. Nothing to read. Put data files in directory and restart program.");
				Console.ResetColor();

				return;
			}

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

		#endregion
	}
}
