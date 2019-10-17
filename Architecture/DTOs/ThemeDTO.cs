using DataModeling;

namespace Architecture.DTOs
{
    public class ThemeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StyleSheetCDN { get; set; }
        public ThemeDTO(Theme theme)
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
