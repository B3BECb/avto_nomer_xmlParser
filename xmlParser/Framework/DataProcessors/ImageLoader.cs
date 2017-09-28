using System;
using System.IO;
using System.Net;

namespace xmlParser.Framework.DataProcessors
{
	/// <summary>Загрузчик изображений.</summary>
	class ImageLoader
	{
		/// <summary>Загрузить изображение.</summary>
		/// <param name="imageUri">Адрес изображения.</param>
		/// <param name="fileName">Имя сохраняемого изображения.</param>
		/// <param name="direcoryName">Путь к директории куда будет сохранено изображение.</param>
		public void LoadImage(string imageUri, string fileName, string direcoryName)
		{
			if(!Directory.Exists(direcoryName))
			{
				Directory.CreateDirectory(direcoryName);
			}

			try
			{

				using(var client = new WebClient())
				{
					client.DownloadFile(imageUri, direcoryName + "\\" + fileName);
				}
			}
			catch(Exception exc)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(exc.ToString());
				Console.ResetColor();

				using(StreamWriter sw = File.AppendText(direcoryName + "\\Error.log"))
				{
					sw.WriteLine(exc.ToString());
				}
			}
		}
	}
}
