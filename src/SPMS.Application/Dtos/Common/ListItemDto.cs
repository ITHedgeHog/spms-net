namespace SPMS.Application.Dtos.Common
{
    public class ListItemDto
    {
        public ListItemDto()
        {
            
        }
        public ListItemDto(string text, string value)
        {
            Value = value;
            Text = text;
            Selected = false;
        }
        public ListItemDto(string text, string value, bool selected)
        {
            Value = value;
            Text = text;
            Selected = selected;
        }

        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}