using System;
using System.Xml.Linq;
using System.Collections.Generic;

using xmlParser.Framework.Entities;

namespace xmlParser.Framework.Interfaces
{
	/// <summary>Описывает обработчик данных.</summary>
	interface IDataProcessor
	{
		void Process(IEnumerable<XElement> xmlList);		
	}
}
