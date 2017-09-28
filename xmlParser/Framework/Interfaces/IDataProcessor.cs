using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace xmlParser.Framework.Interfaces
{
	/// <summary>Описывает обработчик данных.</summary>
	interface IDataProcessor
	{
		/// <summary>Обработать данные.</summary>
		/// <param name="xmlList">Список данных в формате xml.</param>
		void Process(IEnumerable<XElement> xmlList);		
	}
}
