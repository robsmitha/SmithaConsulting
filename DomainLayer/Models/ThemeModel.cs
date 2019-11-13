using DataLayer;

namespace DomainLayer.Models
{
    public class ThemeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StyleSheetCDN { get; set; }
        public ThemeModel(Theme theme)
        {
           if(theme != null)
            {
                ID = theme.ID;
                Name = theme.Name;
                StyleSheetCDN = theme.StyleSheetCDN;
            }
        }
    }
}
