using System;
using System.Collections.Generic;
using System.Linq;
using Chorus.FileTypeHanders.xml;
using Chorus.merge;
using Chorus.merge.xml.generic;
using FLEx_ChorusPlugin.Infrastructure;
using FLEx_ChorusPlugin.Infrastructure.Handling;
using LibChorus.TestUtilities;
using NUnit.Framework;

namespace FLEx_ChorusPluginTests.Infrastructure.Handling.ReportsByDataType
{
	/// <summary>
	/// Test all expected reports (change and conflict) for the GenDate data type.
	///
	/// As of DM 7000052, GenDate is used in:
	///
	/// CmPerson		- DateOfBirth
	/// CmPerson		- DateOfDeath
	/// RnGenericRec	- DateOfEvent
	/// Reminder		- Date
	///
	/// Of these, we'll go with immutable for merge purposes, even if the FLEx UI might allow a change.
	/// </summary>
	[TestFixture]
	public class GenDateDataTypeReportTests : BaseFieldWorksTypeHandlerTests
	{
		private MetadataCache _mdc;
		private XmlMerger _merger;

		[TestFixtureSetUp]
		public override void FixtureSetup()
		{
			base.FixtureSetup();

			_mdc = MetadataCache.TestOnlyNewCache;
			_mdc.UpgradeToVersion(MetadataCache.MaximumModelVersion);
			var mergeOrder = new MergeOrder(null, null, null, new NullMergeSituation())
			{
				EventListener = new ListenerForUnitTests()
			};
			_merger = FieldWorksMergeStrategyServices.CreateXmlMergerForFieldWorksData(mergeOrder, _mdc);
		}

		[Test]
		public void EnsureAllGenDatePropertiesAreSetUpCorrectly()
		{
			foreach (var classInfo in _mdc.AllConcreteClasses)
			{
				var clsInfo = classInfo;
				foreach (var elementStrategy in classInfo.AllProperties
					.Where(pi => pi.DataType == DataType.GenDate)
					.Select(propertyInfo => _merger.MergeStrategies.ElementStrategies[string.Format("{0}{1}_{2}", propertyInfo.IsCustomProperty ? "Custom_" : "", clsInfo.ClassName, propertyInfo.PropertyName)]))
				{
					Assert.IsFalse(elementStrategy.IsAtomic);
					Assert.IsFalse(elementStrategy.OrderIsRelevant);
					Assert.AreEqual(0, elementStrategy.AttributesToIgnoreForMerging.Count);
					Assert.AreEqual(NumberOfChildrenAllowed.Zero, elementStrategy.NumberOfChildren);
					Assert.IsInstanceOf<FindFirstElementWithSameName>(elementStrategy.MergePartnerFinder);
					Assert.IsFalse(elementStrategy.IsImmutable);
				}
			}
		}

		[Test]
		public void EnsureDateOfEventAddedByLoserGetsMerged()
		{
			const string commonAncestor =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
	</RnGenericRec>
</Anthropology>";

			const string ours =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
	</RnGenericRec>
</Anthropology>";

			const string theirs =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
		<DateOfEvent
			val='201206221' />
	</RnGenericRec>
</Anthropology>";

			FieldWorksTestServices.DoMerge(FileHandler,
				"ntbk",
				commonAncestor, ours, theirs,
				new[] { "Anthropology/RnGenericRec/DateOfEvent" }, null,
				0, new List<Type>(),
				1, new List<Type> { typeof(XmlAdditionChangeReport) });
		}

		[Test]
		public void EnsureDateOfEventAddedByWinnerGetsMerged()
		{
			const string commonAncestor =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
	</RnGenericRec>
</Anthropology>";

			const string ours =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
		<DateOfEvent
			val='201206221' />
	</RnGenericRec>
</Anthropology>";

			const string theirs =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
	</RnGenericRec>
</Anthropology>";

			FieldWorksTestServices.DoMerge(FileHandler,
				"ntbk",
				commonAncestor, ours, theirs,
				new[] { "Anthropology/RnGenericRec/DateOfEvent" }, null,
				0, new List<Type>(),
				1, new List<Type> { typeof(XmlAdditionChangeReport) });
		}

		[Test]
		public void EnsureDateOfEventRemovedByLoserGetsMerged()
		{
			const string commonAncestor =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
		<DateOfEvent
			val='201206221' />
	</RnGenericRec>
</Anthropology>";

			const string ours =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
		<DateOfEvent
			val='201206221' />
	</RnGenericRec>
</Anthropology>";

			const string theirs =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
	</RnGenericRec>
</Anthropology>";

			FieldWorksTestServices.DoMerge(FileHandler,
				"ntbk",
				commonAncestor, ours, theirs,
				null, new[] { "Anthropology/RnGenericRec/DateOfEvent" },
				0, new List<Type>(),
				1, new List<Type> { typeof(XmlDeletionChangeReport) });
		}

		[Test]
		public void EnsureDateOfEventRemovedByWinnerGetsMerged()
		{
			const string commonAncestor =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
		<DateOfEvent
			val='201206221' />
	</RnGenericRec>
</Anthropology>";

			const string ours =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
	</RnGenericRec>
</Anthropology>";

			const string theirs =
				@"<?xml version='1.0' encoding='utf-8'?>
<Anthropology>
	<header>
		<RnResearchNbk guid='lexdb' />
	</header>
	<RnGenericRec guid='c1ed94c5-e382-11de-8a39-0800200c9a66'>
		<DateOfEvent
			val='201206221' />
	</RnGenericRec>
</Anthropology>";

			FieldWorksTestServices.DoMerge(FileHandler,
				"ntbk",
				commonAncestor, ours, theirs,
				null, new[] { "Anthropology/RnGenericRec/DateOfEvent" },
				0, new List<Type>(),
				1, new List<Type> { typeof(XmlDeletionChangeReport) });
		}
	}
}