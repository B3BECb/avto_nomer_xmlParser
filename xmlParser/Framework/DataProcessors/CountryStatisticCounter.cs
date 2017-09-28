using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

using xmlParser.Framework.Interfaces;
using xmlParser.Framework.Entities;

namespace xmlParser.Framework.DataProcessors
{
	/// <summary>Реализует интерфейс <see cref="IDataProcessor"/>.</summary>
	public class CountryStatisticCounter : IDataProcessor
	{
		public CountryReportData DataStorage { get; }

		private readonly string _imageStorageName;

		private readonly ImageLoader _imageLoader;

		public CountryStatisticCounter(string imageStorageName, IDataStorage dataStorage)
		{
			_imageStorageName = imageStorageName;

			if(!(dataStorage is CountryReportData))
				throw new ArgumentException("IDataStorage is not ReportData");

			DataStorage = dataStorage as CountryReportData;

			_imageLoader = new ImageLoader();
		}

		public void Process(IEnumerable<XElement> xmlList)
		{
			Console.WriteLine();

			DataStorage.TotalPlatesReaded = xmlList.Count();
			
			foreach(var plate in xmlList)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				ReadInfo(plate);				
				Console.WriteLine();
			}

		}

		private void ReadInfo(XElement plate)
		{
			var infoElements = plate.Descendants();

			string photo = string.Empty;
			string informer = string.Empty;
			string nomer = string.Empty;
			string country = string.Empty;


			foreach(var infoElement in infoElements)
			{
				switch(infoElement.Name.LocalName)
				{
					case "photo":
						photo = infoElement.Value;
						break;
					case "link":
						var splitetUrl = infoElement.Value.Split('/');
						country = splitetUrl[splitetUrl.Length - 2];
						break;
					case "informer":
						informer = infoElement.Value;
						break;
					case "nomer":
						nomer = infoElement.Value;
						break;
				}
			}
			Console.WriteLine($@"load images for: country - {country}; plate - {nomer}");

			var splitedPhotoUrl = photo.Split('/');
			var photoId = splitedPhotoUrl[splitedPhotoUrl.Length - 1].Split('.')[0];

			if(string.IsNullOrWhiteSpace(DataStorage.StorageName))
				DataStorage.StorageName = country;

			DataStorage.AnalizeTemplate(nomer);

			Console.WriteLine("Car foto " + photo);
			_imageLoader.LoadImage(photo, $@"car{photoId}.jpg", _imageStorageName + "\\" + country);
			Console.ForegroundColor = ConsoleColor.DarkGray;

			Console.WriteLine("Car renderedPlate " + informer);
			_imageLoader.LoadImage(informer, $@"car{photoId}Plate.png", _imageStorageName + "\\" + country);
			Console.ForegroundColor = ConsoleColor.DarkGray;
		}		
	}
}
