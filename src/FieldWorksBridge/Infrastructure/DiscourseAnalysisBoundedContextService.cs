﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Chorus.FileTypeHanders.FieldWorks;

namespace FieldWorksBridge.Infrastructure
{
	/// <summary>
	/// Read/Write the Discourse Analysis Bounded Context.
	///
	/// This will be the DsDiscourseData instance and all it owns.
	/// </summary>
	internal static class DiscourseAnalysisBoundedContextService
	{
		private const string DiscourseRootFolder = "Discourse";

		internal static void ExtractBoundedContexts(XmlReaderSettings readerSettings, string multiFileDirRoot,
			MetadataCache mdc,
			IDictionary<string, SortedDictionary<string, byte[]>> classData, Dictionary<string, string> guidToClassMapping,
			HashSet<string> skipWriteEmptyClassFiles)
		{
			var discourseBaseDir = Path.Combine(multiFileDirRoot, DiscourseRootFolder);
			if (Directory.Exists(discourseBaseDir))
				Directory.Delete(discourseBaseDir, true);

			SortedDictionary<string, byte[]> sortedInstanceData;
			if (!classData.TryGetValue("DsDiscourseData", out sortedInstanceData))
				return;

			Directory.CreateDirectory(discourseBaseDir);

			// TODO: Are there any other lists, other than thse two?
			// ConstChartTempl and ChartMarkers are two lists owned by DsDiscourseData.
			// How about lang proj's:  <owning num="55" id="TextMarkupTags" card="atomic" sig="CmPossibilityList">

			var multiClassOutput = new Dictionary<string, SortedDictionary<string, byte[]>>();
			if (sortedInstanceData.Count > 0)
			{
				var guid = sortedInstanceData.Keys.First();
				var dataBytes = sortedInstanceData.Values.First();

				// 1. Write out the DsDiscourseData instance in discourseBaseDir, but not the charts it owns.
				FileWriterService.WriteObject(mdc, classData, guidToClassMapping, discourseBaseDir, readerSettings, multiClassOutput, guid, new HashSet<string> { "Charts" });

				// 2. Each chart it owns needs to be written in its own subfolder of discourseBaseDir, a la texts.
				ObjectFinderServices.WritePropertyInFolders(mdc,
					classData, guidToClassMapping, multiClassOutput,
					readerSettings, discourseBaseDir,
					XElement.Parse(MultipleFileServices.Utf8.GetString(dataBytes)),
					"Charts", "Chart_");
			}

			// No need to process these in the 'soup' now.
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "DsDiscourseData");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "DsConstChart");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "ConstChartRow");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "ConstChartWordGroup");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "ConstChartMovedTextMarker");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "ConstChartClauseMarker");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "ConstChartTag");
		}

		internal static void RestoreOriginalFile(XmlWriter writer, XmlReaderSettings readerSettings, string multiFileDirRoot)
		{
			var discourseBaseDir = Path.Combine(multiFileDirRoot, DiscourseRootFolder);
			if (!Directory.Exists(discourseBaseDir))
				return;

			// Main folder
			FileWriterService.WriteClassDataToOriginal(writer, discourseBaseDir, readerSettings);

			// Sub-Folders.
			foreach (var directory in Directory.GetDirectories(discourseBaseDir))
				FileWriterService.WriteClassDataToOriginal(writer, directory, readerSettings);
		}
	}
}