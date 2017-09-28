using System;

namespace xmlParser.Framework.Entities
{
	/// <summary>Форматированная строка.</summary>
	public class ReportText
	{
		/// <summary>Цвет строки.</summary>
		public ConsoleColor TextColor { get; }

		/// <summary>Содержание.</summary>
		public string Text { get; }

		/// <summary>Это новая строка.</summary>
		public bool NewLine { get; set; }

		/// <summary>Создаёт и инициализирует форматированную строку.</summary>
		/// <param name="text">Содержание.</param>
		/// <param name="textColor">Цвет.</param>
		/// <param name="isNewLine">Это новая строка.</param>
		public ReportText(string text = "", ConsoleColor textColor = ConsoleColor.White, bool isNewLine = false)
		{
			Text = text;
			TextColor = textColor;
			NewLine = isNewLine;
		}

		/// <summary>Вывести содержание форматированной строки.</summary>
		/// <returns>Содержание форматированной строки.</returns>
		public override string ToString()
		{
			return Text;
		}
	}
}
