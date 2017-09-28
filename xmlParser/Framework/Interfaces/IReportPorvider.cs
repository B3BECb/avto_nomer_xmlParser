using System;

using System.Collections.Generic;

namespace xmlParser.Framework.Interfaces
{
	/// <summary>Описывает провайдера отчёта.</summary>
	public interface IReportPorvider
	{
		void Write(IReadOnlyList<IDataStorage> storages);
	}
}
