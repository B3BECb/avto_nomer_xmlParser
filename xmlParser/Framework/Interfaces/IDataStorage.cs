using System;
using System.Collections.Generic;

using xmlParser.Framework.Entities;

namespace xmlParser.Framework.Interfaces
{
	/// <summary>Хранилище обработанных данных.</summary>
	public interface IDataStorage
	{
		/// <summary>Наименование хранилища.</summary>
		string StorageName { get; }

		/// <summary>Получить отчёт хранилища.</summary>
		/// <returns>Список форматированных строк.</returns>
		IReadOnlyList<ReportText> GetStorageText();

		/// <summary>Получить отчёт хранилища.</summary>
		/// <param name="format">Формат строки.</param>
		/// <returns>Список форматированных строк.</returns>
		/// <remarks>В вормате строки должны быть только 2 параметра - описание и значение.</remarks>
		IReadOnlyList<ReportText> GetStorageText(string format);
	}
}
