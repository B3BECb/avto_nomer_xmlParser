using System;

namespace xmlParser
{
	class Program
	{
		static void Main(string[] args)
		{
			var path = @"storage";
			var imagePath = @"imageStorage";
			
			Console.WriteLine("Data path is: " + path);

			new DataProcessor().ProcessData(path, imagePath);

			Console.WriteLine("Done.");
			Console.ReadKey();
		}
	}
}
