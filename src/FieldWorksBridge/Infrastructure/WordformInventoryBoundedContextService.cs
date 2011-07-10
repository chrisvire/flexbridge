﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Chorus.FileTypeHanders.FieldWorks;

namespace FieldWorksBridge.Infrastructure
{
	internal static class WordformInventoryBoundedContextService
	{
		private const string WordformInventoryRootFolder = "WordformInventory";

		internal static void ExtractBoundedContexts(XmlReaderSettings readerSettings, string multiFileDirRoot,
												  MetadataCache mdc,
												  IDictionary<string, SortedDictionary<string, byte[]>> classData, Dictionary<string, string> guidToClassMapping,
												  HashSet<string> skipWriteEmptyClassFiles)
		{
			var wfiBaseDir = Path.Combine(multiFileDirRoot, WordformInventoryRootFolder);
			if (Directory.Exists(wfiBaseDir))
				Directory.Delete(wfiBaseDir, true);

			Directory.CreateDirectory(wfiBaseDir);

			// WfiWordSet (Morphology Bounded Context, not WFI).
			// PunctuationForm (in its own Bounded Context)

			// WfiWordform, owns:
			//	WfiAnalysis owns:
			//		WfiGloss (owns nothing)
			//		FsFeatStruc owns:
			//			FsFeatStrucDisj owns:
			//				FsFeatStruc (recursive, see FsFeatStruc)
			//			FsFeatureSpecification (owns nothing)
			//		MoDeriv owns:
			//			FsFeatStruc (see FsFeatStruc)
			//			MoStratumApp owns:
			//				MoCompoundRuleApp (owns nothing)
			//				MoDerivAffApp owns:
			//					MoDerivStepMsa owns:
			//						FsFeatStruc (see FsFeatStruc)
			//				MoInflTemplateApp owns:
			//					MoInflAffixSlotApp (owns nothing)
			//				MoPhonolRuleApp (owns nothing)
			//		WfiMorphBundle (owns nothing)
			SortedDictionary<string, byte[]> sortedInstanceData;
			if (!classData.TryGetValue("WfiWordform", out sortedInstanceData))
				return;

			var srcDataCopy = new SortedDictionary<string, byte[]>(sortedInstanceData);
			var multiClassOutput = new Dictionary<string, SortedDictionary<string, byte[]>>();
			foreach (var kvpWordform in srcDataCopy)
			{
				var dataBytes = ObjectFinderServices.RegisterDataInBoundedContext(classData, guidToClassMapping, multiClassOutput, kvpWordform.Key);
				ObjectFinderServices.CollectAllOwnedObjects(mdc,
															classData, guidToClassMapping, multiClassOutput,
															XElement.Parse(MultipleFileServices.Utf8.GetString(dataBytes)),
															new HashSet<string>());
			}
			foreach (var kvp in multiClassOutput)
			{
				var classname = kvp.Key;
				switch (classname)
				{
					default:
						// Only write one file.
						FileWriterService.WriteSecondaryFile(Path.Combine(wfiBaseDir, classname + ".ClassData"), readerSettings, kvp.Value);
						break;
					case "WfiWordform":
					case "WfiAnalysis":
					case "WfiMorphBundle":
						// Write 10 files for each high volume class.
						FileWriterService.WriteSecondaryFiles(wfiBaseDir, classname, readerSettings, kvp.Value);
						break;
				}
			}

			// No need to process these in the 'soup' now.
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "WfiWordform");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "WfiAnalysis");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "WfiGloss");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "WfiMorphBundle");
			ObjectFinderServices.ProcessLists(classData, skipWriteEmptyClassFiles, "MoDeriv");
		}

		internal static void RestoreOriginalFile(XmlWriter writer, XmlReaderSettings readerSettings, string multiFileDirRoot)
		{
			var wfiBaseDir = Path.Combine(multiFileDirRoot, WordformInventoryRootFolder);
			if (!Directory.Exists(wfiBaseDir))
				return;

			FileWriterService.WriteClassDataToOriginal(writer, wfiBaseDir, readerSettings);
		}
	}
}