using System;
using System.Collections.Generic;

using xmlParser.Framework.Entities;

namespace xmlParser.Framework.Interfaces
{
	public interface IDataStorage
	{
		string StorageName { get; }

		IReadOnlyList<ReportText> GetStorageText();

		IReadOnlyList<ReportText> GetStorageText(string format);
	}
}
