using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using xmlParser.Framework.Interfaces;

namespace xmlParser.Framework.Entities
{
	/// <summary>Реализует <see cref="IDataStorage"/>.</summary>
	public class CountryReportData : IDataStorage
	{
		#region Properties

		/// <summary>Имя хранилища.</summary>
		public string StorageName { get; set; }

		/// <summary>Всего номеров в файле.</summary>
		public int TotalPlatesReaded { get; set; }

		/// <summary>Словарь номеров файла.</summary>
		public Dictionary<string, PlateInfo> Plates { get; } = new Dictionary<string, PlateInfo>();

		#endregion

		#region Methods

		/// <summary>Проанализировать номер.</summary>
		/// <param name="template">Номер.</param>
		/// <remarks>Производит разбор номера и его шаблонизацию.</remarks>
		public void AnalizeTemplate(string template)
		{
			var plateMask = new Regex(@"[^\d\s-.·]").Replace(template.ToLower(), "X");
			plateMask = new Regex(@"[\d]").Replace(plateMask, "9");

			if(!Plates.ContainsKey(plateMask))
				Plates.Add(plateMask, new PlateInfo());

			var plate = Plates.First(p => p.Key == plateMask);
			var plateValue = plate.Value;
			plateValue.Count++;

			Plates.Remove(plateMask);
			Plates.Add(plateMask, plateValue);

			foreach(var letter in template.ToLower())
			{
				if(char.IsLetterOrDigit(letter))
				{
					if(plate.Value.TemplateLetters.ContainsKey(letter))
					{
						var value = plate.Value.TemplateLetters.Where(x => x.Key == letter).First().Value;
						plate.Value.TemplateLetters.Remove(letter);
						plate.Value.TemplateLetters.Add(letter, ++value);
					}
					else
					{
						plate.Value.TemplateLetters.Add(letter, 1);
					}
				}
			}
		}

		private Dictionary<char, int> CountCountryLetters()
		{
			var lettersDict = new Dictionary<char, int>();

			if(Plates.Count == 1)
				lettersDict = Plates.First().Value.TemplateLetters;
			else
				foreach(var plate in Plates)
				{
					foreach(var letter in plate.Value.TemplateLetters)
					{
						if(char.IsLetterOrDigit(letter.Key))
						{
							if(lettersDict.ContainsKey(letter.Key))
							{
								var value = lettersDict.Where(x => x.Key == letter.Key).FirstOrDefault().Value;
								lettersDict.Remove(letter.Key);
								lettersDict.Add(letter.Key, letter.Value + value);
							}
							else
							{
								lettersDict.Add(letter.Key, 1);
							}
						}
					}
				}

			return lettersDict;
		}

		#endregion

		#region IDataStorage implementation

		/// <summary>Получить отчёт хранилища.</summary>
		/// <returns>Список форматированных строк.</returns>
		public IReadOnlyList<ReportText> GetStorageText()
		{
			List<ReportText> list = new List<ReportText>()
			{
				new ReportText($@"Country: {StorageName}" , ConsoleColor.Magenta),
				new ReportText($@"Total plates readed: {TotalPlatesReaded}"),
			};

			var orederedPlates = Plates.OrderBy(key => key.Key);

			foreach(var plate in orederedPlates)
			{
				list.Add(new ReportText($@"Plate template: {plate.Key}", ConsoleColor.Gray));
				list.Add(new ReportText(plate.Value.Count.ToString(), ConsoleColor.DarkGray));

				foreach(var letter in plate.Value.TemplateLetters.OrderBy(key => key.Key))
				{
					list.Add(new ReportText($@"{letter.Key} - {letter.Value}", ConsoleColor.DarkCyan));
				}

				list.Add(new ReportText(isNewLine: true));
				list.Add(new ReportText(isNewLine: true));
			}

			foreach(var plate in orederedPlates)
			{
				list.Add(new ReportText($@"{plate.Key}: {plate.Value.Count.ToString()}", ConsoleColor.Gray));
			}

			list.Add(new ReportText(isNewLine: true));
			list.Add(new ReportText(isNewLine: true));

			var lettersDict = CountCountryLetters();

			foreach(var letter in lettersDict.OrderBy(key => key.Key))
			{
				list.Add(new ReportText($@"{letter.Key}: {letter.Value}", ConsoleColor.DarkCyan));
			}

			return list;
		}

		/// <summary>Получить отчёт хранилища.</summary>
		/// <param name="format">Формат строки.</param>
		/// <returns>Список форматированных строк.</returns>
		/// <remarks>В вормате строки должны быть только 2 параметра - описание и значение.</remarks>
		public IReadOnlyList<ReportText> GetStorageText(string format)
		{
			List<ReportText> list = new List<ReportText>()
			{
				new ReportText(string.Format(format, "Country", StorageName) , ConsoleColor.Magenta),
				new ReportText(string.Format(format, "Total plates readed", TotalPlatesReaded)),
				new ReportText(isNewLine: true),
			};

			var orederedPlates = Plates.OrderBy(key => key.Key);

			foreach(var plate in orederedPlates)
			{
				list.Add(new ReportText(string.Format(format, "Plate template", plate.Key), ConsoleColor.Gray));
				list.Add(new ReportText(string.Format(format, "Plates with this template", plate.Value.Count.ToString()), ConsoleColor.DarkGray));

				foreach(var letter in plate.Value.TemplateLetters.OrderBy(key => key.Key))
				{
					list.Add(new ReportText(string.Format(format, letter.Key, letter.Value), ConsoleColor.DarkCyan));
				}

				list.Add(new ReportText(isNewLine: true));
				list.Add(new ReportText(isNewLine: true));
			}

			foreach(var plate in orederedPlates)
			{
				list.Add(new ReportText(string.Format(format, plate.Key, plate.Value.Count.ToString()), ConsoleColor.Gray));
			}

			list.Add(new ReportText(isNewLine: true));
			list.Add(new ReportText(isNewLine: true));

			var lettersDict = CountCountryLetters();

			foreach(var letter in lettersDict.OrderBy(key => key.Key))
			{
				list.Add(new ReportText(string.Format(format, letter.Key, letter.Value), ConsoleColor.DarkCyan));
			}

			return list;
		}

		#endregion
	}
}
