using System;
using System.Collections.Generic;
using xmlParser.Framework.DataProcessors;

namespace xmlParser.Framework.Interfaces
{
	/// <summary>Описывает провайдера отчёта.</summary>
	public interface IReportPorvider
	{
		void Write(IReadOnlyList<CountryStatisticCounter> counters);
	}
}
