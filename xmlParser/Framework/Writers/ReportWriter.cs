using System;
using System.Collections.Generic;

using xmlParser.Framework.Interfaces;

namespace xmlParser.Framework.Writers
{
	/// <summary>Писатель отчёта.</summary>
	class ReportWriter
	{
		/// <summary>Хранилища писателя.</summary>
		public List<IDataStorage> Storages { get; set; }

		private readonly IReportPorvider _provider;

		/// <summary>Создать и инициализировать писателя отчёта.</summary>
		/// <param name="provider">Провайдер отчёта <see cref="IReportPorvider"/>.</param>
		/// <param name="counter">Данные для отчёта <see cref="CountryStatisticCounter"/>.</param>
		public ReportWriter(IReportPorvider provider)
		{
			_provider = provider ?? throw new Exception(nameof(provider) + " is null");

			Storages = new List<IDataStorage>();
		}

		/// <summary>Сформировать отчёт.</summary>
		public void Write()
		{
			_provider.Write(Storages);
		}
	}
}
