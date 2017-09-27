using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace xmlParser.Framework.Interfaces
{
	/// <summary>Описывает обработчик данных.</summary>
	interface IDataProcessor
	{
		void Process(IEnumerable<XElement> xmlList);
	}
}
