using System;

using System.Collections.Generic;

namespace xmlParser.Framework.Interfaces
{
	/// <summary>Описывает провайдера отчёта.</summary>
	public interface IReportPorvider
	{
		/// <summary>Создать отчёт.</summary>
		/// <param name="storages">Список хранилищ для которых необходимо создать отчёт.</param>
		void Write(IReadOnlyList<IDataStorage> storages);
	}
}
