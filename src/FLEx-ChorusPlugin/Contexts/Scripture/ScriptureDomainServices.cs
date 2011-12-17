﻿using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using FLEx_ChorusPlugin.Infrastructure;

namespace FLEx_ChorusPlugin.Contexts.Scripture
{
	/// <summary>
	/// This domain services class interacts with the Scripture bounded contexts.
	/// </summary>
	internal static class ScriptureDomainServices
	{
		internal static void WriteDomainData(XmlReaderSettings readerSettings, string rootDir,
			MetadataCache mdc,
			IDictionary<string, SortedDictionary<string, XElement>> classData,
			Dictionary<string, string> guidToClassMapping,
			Dictionary<string, Dictionary<string, HashSet<string>>> interestingPropertiesCache,
			HashSet<string> skipWriteEmptyClassFiles)
		{
			var scriptureBaseDir = Path.Combine(rootDir, "Scripture");
			// TODO: Switch to new location at some point.
			var multiFileDirRoot = Path.Combine(rootDir, "DataFiles");
			ScriptureBoundedContextService.ExtractBoundedContexts(readerSettings, multiFileDirRoot, mdc, classData, guidToClassMapping, skipWriteEmptyClassFiles);
		}

		internal static void RemoveBoundedContextData(string pathRoot)
		{

		}
	}
}