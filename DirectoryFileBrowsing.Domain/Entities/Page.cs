using System.Collections.Generic;

namespace DirectoryFileBrowsing.Domain.Entities
{
    public class Page
    {
        public string Parent { get; set; }
        public string CurrentPath { get; set; }
        public List<Point> Directorys { get; set; }

        public Page(string parent, string currentPath, List<Point> directorys)
        {
            Parent = parent;
            CurrentPath = currentPath;
            Directorys = directorys;
        }
    }
}
