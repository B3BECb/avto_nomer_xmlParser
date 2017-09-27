using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlParser.Framework.DataProcessors;
using xmlParser.Framework.Interfaces;

namespace xmlParser.Framework.Writers
{
	class ReportWriter
	{
		private readonly IReportPorvider _provider;

		public List<CountryStatisticCounter> Counters { get; set; }

		/// <summary>Создать и инициализировать писателя отчёта.</summary>
		/// <param name="provider">Провайдер отчёта <see cref="IReportPorvider"/>.</param>
		/// <param name="counter">Данные для отчёта <see cref="CountryStatisticCounter"/>.</param>
		public ReportWriter(IReportPorvider provider)
		{
			_provider = provider ?? throw new Exception(nameof(provider) + " is null");

			Counters = new List<CountryStatisticCounter>();
		}

		/// <summary>Сформировать отчёт.</summary>
		public void Write()
		{
			_provider.Write(Counters);
		}
	}
}
