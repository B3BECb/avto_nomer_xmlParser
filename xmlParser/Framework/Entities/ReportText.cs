using System;

namespace xmlParser.Framework.Entities
{
	public class ReportText
	{
		public ConsoleColor TextColor { get; }

		public string Text { get; }

		public bool NewLine { get; set; }

		public ReportText(string text = "", ConsoleColor textColor = ConsoleColor.White, bool isNewLine = false)
		{
			Text = text;
			TextColor = textColor;
			NewLine = isNewLine;
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
