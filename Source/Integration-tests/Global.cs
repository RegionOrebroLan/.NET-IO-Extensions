using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.IO.IntegrationTests
{
	[TestClass]
	[SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable")]
	[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
	public class Global
	{
		#region Fields

		// ReSharper disable PossibleNullReferenceException
		public static readonly string ProjectDirectoryPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
		// ReSharper restore PossibleNullReferenceException

		#endregion

		#region Methods

		[AssemblyInitialize]
		[CLSCompliant(false)]
		[SuppressMessage("Usage", "CA1801:Review unused parameters")]
		public static void Initialize(TestContext testContext) { }

		#endregion
	}
}