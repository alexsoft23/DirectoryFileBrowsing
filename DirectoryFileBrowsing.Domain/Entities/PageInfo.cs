

namespace DirectoryFileBrowsing.Domain.Entities
{
    public class PageInfo
    {
        public int Less10Mb { get; set; }
        public int More10MbAndLess50Mb { get; set; }
        public int More100Mb { get; set; }

        public PageInfo(int less10Mb, int more10MbAndLess50Mb, int more100Mb)
        {
            Less10Mb = less10Mb;
            More10MbAndLess50Mb = more10MbAndLess50Mb;
            More100Mb = more100Mb;
        }
    }
}
