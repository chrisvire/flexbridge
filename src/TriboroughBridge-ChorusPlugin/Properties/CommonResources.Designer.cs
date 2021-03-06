﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TriboroughBridge_ChorusPlugin.Properties {
	using System;


	/// <summary>
	///   A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	// This class was auto-generated by the StronglyTypedResourceBuilder
	// class via a tool like ResGen or Visual Studio.
	// To add or remove a member, edit your .ResX file then rerun ResGen
	// with the /str option, or rebuild your VS project.
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	public class CommonResources {

		private static global::System.Resources.ResourceManager resourceMan;

		private static global::System.Globalization.CultureInfo resourceCulture;

		[global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal CommonResources() {
		}

		/// <summary>
		///   Returns the cached ResourceManager instance used by this class.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		public static global::System.Resources.ResourceManager ResourceManager {
			get {
				if (object.ReferenceEquals(resourceMan, null)) {
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TriboroughBridge_ChorusPlugin.Properties.CommonResources", typeof(CommonResources).Assembly);
					resourceMan = temp;
				}
				return resourceMan;
			}
		}

		/// <summary>
		///   Overrides the current thread's CurrentUICulture property for all
		///   resource lookups using this strongly typed resource class.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		public static global::System.Globalization.CultureInfo Culture {
			get {
				return resourceCulture;
			}
			set {
				resourceCulture = value;
			}
		}

		public static System.Drawing.Icon chorus {
			get {
				object obj = ResourceManager.GetObject("chorus", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}

		public static System.Drawing.Icon chorus32x32 {
			get {
				object obj = ResourceManager.GetObject("chorus32x32", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}

		/// <summary>
		///   Looks up a localized string similar to There is already a copy of FLExBridge running.
		///You probably have a Conflict Report open. It will need to be closed before you can access any of the other FLExBridge functions such as:
		///-- Send/Receive Project
		///-- Receive Project from a colleague
		///-- View Conflict Report (can&apos;t have two open).
		/// </summary>
		public static string kAlreadyRunning {
			get {
				return ResourceManager.GetString("kAlreadyRunning", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Sorry. FLEx Bridge was not able to check for an update right now..
		/// </summary>
		public static string kCannotCheckForUpdateNow {
			get {
				return ResourceManager.GetString("kCannotCheckForUpdateNow", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Check for Update Failure.
		/// </summary>
		public static string kCheckForUpdateFailure {
			get {
				return ResourceManager.GetString("kCheckForUpdateFailure", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to This repository has no data in it yet. Before you can get data from this repository, someone needs to send project data to this repository..
		/// </summary>
		public static string kEmptyRepoMsg {
			get {
				return ResourceManager.GetString("kEmptyRepoMsg", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to FLEx Bridge.
		/// </summary>
		public static string kFLExBridge {
			get {
				return ResourceManager.GetString("kFLExBridge", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to FLEx Bridge Version.
		/// </summary>
		public static string KFlexBridgeVersion {
			get {
				return ResourceManager.GetString("KFlexBridgeVersion", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to FLEx isn&apos;t listening..
		/// </summary>
		public static string kFlexNotListening {
			get {
				return ResourceManager.GetString("kFlexNotListening", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Could not obtain the new project, because one with the same name already exists..
		/// </summary>
		public static string kFlexProjectExists {
			get {
				return ResourceManager.GetString("kFlexProjectExists", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to You will need to update your version of Flex to obtain the requested project, since it is newer..
		/// </summary>
		public static string kFlexUpdateRequired {
			get {
				return ResourceManager.GetString("kFlexUpdateRequired", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to To Send/Receive that project, open it and use Send/Receive &gt; Project (or Lexicon)..
		/// </summary>
		public static string kHowToSendReceiveExtantRepository {
			get {
				return ResourceManager.GetString("kHowToSendReceiveExtantRepository", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Invalid command line options. Please launch from FLEx..
		/// </summary>
		public static string kInvalidCommandLineOptions {
			get {
				return ResourceManager.GetString("kInvalidCommandLineOptions", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Moving obtained project to final location..
		/// </summary>
		public static string kMovingRepo {
			get {
				return ResourceManager.GetString("kMovingRepo", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Your version is &apos;{0}&apos;. A newer version &apos;{1}&apos; exists. Do you want to download it?.
		/// </summary>
		public static string kNewerVersionAvailablePattern {
			get {
				return ResourceManager.GetString("kNewerVersionAvailablePattern", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Obtain project.
		/// </summary>
		public static string kObtainProject {
			get {
				return ResourceManager.GetString("kObtainProject", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Empty Repository.
		/// </summary>
		public static string kRepoProblem {
			get {
				return ResourceManager.GetString("kRepoProblem", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Your version is current..
		/// </summary>
		public static string kYourVersionIsCurrent {
			get {
				return ResourceManager.GetString("kYourVersionIsCurrent", resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Receive project.
		/// </summary>
		public static string ObtainProjectView_DialogTitle {
			get {
				return ResourceManager.GetString("ObtainProjectView_DialogTitle", resourceCulture);
			}
		}
	}
}
