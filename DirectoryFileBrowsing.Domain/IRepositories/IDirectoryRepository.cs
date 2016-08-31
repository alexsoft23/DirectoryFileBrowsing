using System.Collections.Generic;
using DirectoryFileBrowsing.Domain.Entities;

namespace DirectoryFileBrowsing.Domain.IRepositories
{
    public interface IDirectoryRepository : IBaseRepository<Point>, IBaseRepository<PageInfo>
    {
        PageInfo GetFolderInfo(string path);
        List<Point> GetObList(string path);
        List<Point> GetDrives();
    }
}
