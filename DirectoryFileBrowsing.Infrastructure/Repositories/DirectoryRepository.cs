using DirectoryFileBrowsing.Domain.Entities;
using DirectoryFileBrowsing.Domain.Enums;
using DirectoryFileBrowsing.Domain.IRepositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryFileBrowsing.Infrastructure.Repositories
{
	public class DirectoryRepository : BaseRepository, IDirectoryRepository
	{
		public List<Point> GetDrives()
		{
			var directories = DriveInfo.GetDrives().Select(s => s.RootDirectory.FullName).ToList();

			return directories.Select(directory => new Point(directory, PointType.Disk)).ToList();
		}

		public List<Point> GetObList(string path)
		{
			var directories = new List<Point>();


			var directory = new DirectoryInfo(path);
			try
			{
				directories.AddRange((from dir in directory.EnumerateDirectories()
									  let f = File.GetAttributes(dir.FullName)
									  where (f & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint
									  where (f & FileAttributes.Hidden) != FileAttributes.Hidden
									  select new Point(dir.FullName, PointType.Directory))
									 .Union(from fileInfo in directory.EnumerateFiles()
											let f = File.GetAttributes(fileInfo.FullName)
											where (f & FileAttributes.Hidden) != FileAttributes.Hidden
											select new Point(fileInfo.FullName.Replace(@"\", "\\"), PointType.File)));

			}
			catch
			{
				// ignored
			}
			//try
			//{
			//	directories.AddRange(from fileInfo in directory.EnumerateFiles()
			//						 let f = File.GetAttributes(fileInfo.FullName)
			//						 where (f & FileAttributes.Hidden) != FileAttributes.Hidden
			//						 select new Point(fileInfo.FullName.Replace(@"\", "\\"), PointType.File));
			//}
			//catch
			//{

			//	// ignored
			//}
			return directories;
		}

		public PageInfo GetFolderInfo(string path)
		{
			int less10Mb = 0;
			int more10MbAndLess50Mb = 0;
			int more100Mb = 0;

			var dirs = new Stack<string>(20);

			if (!Directory.Exists(path))
			{
				return new PageInfo(-1, -1, -1);
			}
			dirs.Push(path);

			while (dirs.Count > 0)
			{
				var currentDir = dirs.Pop();
				string[] subDirs = new string[] { };
				try
				{
					subDirs = Directory.GetDirectories(currentDir);
				}
				catch
				{
					continue;
				}
				string[] files = new string[] { };
				try
				{
					files = Directory.GetFiles(currentDir);
				}

				catch
				{
					continue;
				}
				try
				{
					foreach (var f in files
					.Select(s => new FileInfo(s))
					.Where(f => (f.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden))
					{
						if (f.Length <= (1 << 20) * 10)
						{
							less10Mb++;
						}
						if (f.Length >= (1 << 20) * 10 & f.Length <= (1 << 20) * 50)
						{
							more10MbAndLess50Mb++;
						}
						if (f.Length >= (1 << 20) * 100)
						{
							more100Mb++;
						}
					}

					foreach (var str in from str in subDirs
										let f = File.GetAttributes(str)
										where (f & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint
										where (f & FileAttributes.Hidden) != FileAttributes.Hidden
										select str)
					{
						dirs.Push(str);
					}
				}
				catch
				{
					// ignored
				}
			}
			return new PageInfo(less10Mb, more10MbAndLess50Mb, more100Mb);
		}

	}
}
