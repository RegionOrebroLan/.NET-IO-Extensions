using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.IO.IntegrationTests
{
	[TestClass]
	public class FileSystemEntryMatcherTest
	{
		#region Fields

		private static readonly IFileSystemEntryMatcher _fileSystemEntryMatcher = new FileSystemEntryMatcher();

		#endregion

		#region Properties

		protected internal virtual IFileSystemEntryMatcher FileSystemEntryMatcher => _fileSystemEntryMatcher;

		#endregion

		#region Methods

		[TestMethod]
		public void GetPathMatches_IfTheIncludePatternContainsDirectoryPathsForDirectoriesThatDoesNotExist_ShouldReturnAResultIncludingTheDirectoryIncludePath()
		{
			const string directoryPath = @"D:\4CB01766-B0AA-42FF-BC71-F4318B6DF030\87210E66-8AC6-4B8A-875C-86151099EE36\";

			Assert.IsFalse(Directory.Exists(directoryPath));

			var pathMatches = this.FileSystemEntryMatcher.GetPathMatches(null, null, directoryPath).ToArray();

			Assert.AreEqual(1, pathMatches.Length);
			Assert.AreEqual(directoryPath, pathMatches[0]);
		}

		[TestMethod]
		public void GetPathMatches_ShouldHandleWildcardMatches()
		{
			var testResourcesDirectoryPath = Path.Combine(Global.ProjectDirectoryPath, "Test-resources");
			const string includePattern = @"**\*.*";

			this.GetPathMatchesShouldHandleWildcardMatches(this.FileSystemEntryMatcher.GetPathMatches(testResourcesDirectoryPath, null, includePattern), string.Empty);
			this.GetPathMatchesShouldHandleWildcardMatches(this.FileSystemEntryMatcher.GetPathMatches(null, null, Path.Combine(testResourcesDirectoryPath, includePattern)), testResourcesDirectoryPath + "\\");
		}

		protected internal virtual void GetPathMatchesShouldHandleWildcardMatches(IEnumerable<string> pathMatches, string pathMatchPrefix)
		{
			pathMatches = pathMatches.ToArray();

			Assert.AreEqual(10, pathMatches.Count());

			Assert.AreEqual(pathMatchPrefix + @"Directory\First.log", pathMatches.ElementAt(0));
			Assert.AreEqual(pathMatchPrefix + @"Directory\First.txt", pathMatches.ElementAt(1));
			Assert.AreEqual(pathMatchPrefix + @"Directory\Second.log", pathMatches.ElementAt(2));
			Assert.AreEqual(pathMatchPrefix + @"Directory\Second.txt", pathMatches.ElementAt(3));
			Assert.AreEqual(pathMatchPrefix + @"Directory\Third.txt", pathMatches.ElementAt(4));

			Assert.AreEqual(pathMatchPrefix + "First.log", pathMatches.ElementAt(5));
			Assert.AreEqual(pathMatchPrefix + "First.txt", pathMatches.ElementAt(6));
			Assert.AreEqual(pathMatchPrefix + "Second.log", pathMatches.ElementAt(7));
			Assert.AreEqual(pathMatchPrefix + "Second.txt", pathMatches.ElementAt(8));
			Assert.AreEqual(pathMatchPrefix + "Third.txt", pathMatches.ElementAt(9));
		}

		#endregion
	}
}