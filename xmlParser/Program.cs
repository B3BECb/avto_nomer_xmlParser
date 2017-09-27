using System;

using xmlParser.Framework.DataProcessors;
using xmlParser.Framework.Providers;
using xmlParser.Framework.Writers;

namespace xmlParser
{
	class Program
	{
		static void Main(string[] args)
		{
			var path = @"storage";
			var imagePath = @"imageStorage";
			
			Console.WriteLine("Data path is " + path);

			new DataProcessor().ProcessData(path, imagePath);

			//var csvProvider = new CsvReportProvider();
			//new ReportWriter(csvProvider, areaCounter).Write();
			//Console.WriteLine(Environment.NewLine + "File Report.csv saved in " + csvProvider.Folder);
						
			Console.ReadKey();
		}
	}
}
