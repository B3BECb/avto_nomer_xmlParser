using System;
using System.Collections.Generic;

namespace xmlParser.Framework.Entities
{
	/// <summary>Информация о шаблоне номера.</summary>
	public class PlateInfo
	{
		/// <summary>Встречено таких шаблонов.</summary>
		public int Count { get; set; }

		/// <summary>Символы шаблона.</summary>
		public Dictionary<char, int> TemplateLetters { get; } = new Dictionary<char, int>();
	}
}
