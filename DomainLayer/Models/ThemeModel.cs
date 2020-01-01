using DomainLayer.Entities;

namespace DomainLayer.Models
{
    public class ThemeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StyleSheetCDN { get; set; }
    }
}
