using DirectoryFileBrowsing.Domain.Enums;

namespace DirectoryFileBrowsing.Domain.Entities
{
    public class Point
    {
        public string Path { get; set; }
        public PointType Type { get; set; }

        public Point(string paths, PointType types)
        {
            Path = paths;
            Type = types;
        }
    }
}
