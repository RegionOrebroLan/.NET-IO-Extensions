using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Build.Execution;

namespace RegionOrebroLan.IO
{
	public class FileSystemEntryMatcher : IFileSystemEntryMatcher
	{
		#region Fields

		private static Type _fileMatcherType;
		private static Func<string, string, List<string>, string[]> _getFilesFunction;

		#endregion

		#region Properties

		protected internal virtual Type FileMatcherType
		{
			get
			{
				// ReSharper disable All
				if(_fileMatcherType == null)
				{
					var buildManagerType = typeof(BuildManager);
					var fileMatcherAssemblyQualifiedName = buildManagerType.AssemblyQualifiedName.Replace(buildManagerType.FullName, "Microsoft.Build.Shared.FileMatcher");

					_fileMatcherType = Type.GetType(fileMatcherAssemblyQualifiedName, true);
				}
				// ReSharper restore All

				return _fileMatcherType;
			}
		}

		protected internal virtual Func<string, string, List<string>, string[]> GetFilesFunction
		{
			get
			{
				// ReSharper disable InvertIf
				if(_getFilesFunction == null)
				{
					// Got it here: https://github.com/Microsoft/msbuild/blob/master/src/Shared/FileMatcher.cs#L60
					var fileMatcher = this.FileMatcherType.GetField("Default").GetValue(null);
					var method = this.FileMatcherType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).First(item => string.Equals(item.Name, "GetFiles", StringComparison.Ordinal));

					_getFilesFunction = (Func<string, string, List<string>, string[]>) Delegate.CreateDelegate(typeof(Func<string, string, List<string>, string[]>), fileMatcher, method);
				}
				// ReSharper restore InvertIf

				return _getFilesFunction;
			}
		}

		#endregion

		#region Methods

		public virtual IEnumerable<string> GetPathMatches(string directoryPath, IEnumerable<string> excludePatterns, string includePattern)
		{
			return this.GetFilesFunction.Invoke(directoryPath, includePattern, excludePatterns?.ToList());
		}

		#endregion
	}
}