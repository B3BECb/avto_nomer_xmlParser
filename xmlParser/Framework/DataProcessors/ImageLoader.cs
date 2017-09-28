using System;
using System.IO;
using System.Net;

namespace xmlParser.Framework.DataProcessors
{
	class ImageLoader
	{
		public void LoadImage(string imageUri, string fileName, string direcoryName)
		{
			if(!Directory.Exists(direcoryName))
			{
				Directory.CreateDirectory(direcoryName);
			}

			try
			{

				//using(var client = new WebClient())
				//{
				//	client.DownloadFile(imageUri, direcoryName + "\\" + fileName);
				//}
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
