using System;
using System.Collections.Generic;

namespace xmlParser.Framework.Entities
{
	public class PlateInfo
	{
		public int Count { get; set; }

		public Dictionary<char, int> TemplateLetters { get; } = new Dictionary<char, int>();
	}
}
