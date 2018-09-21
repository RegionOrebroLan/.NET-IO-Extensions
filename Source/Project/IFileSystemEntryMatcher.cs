using System.Collections.Generic;

namespace RegionOrebroLan.IO
{
	public interface IFileSystemEntryMatcher
	{
		#region Methods

		IEnumerable<string> GetPathMatches(string directoryPath, IEnumerable<string> excludePatterns, string includePattern);

		#endregion
	}
}