namespace VTSV.Models
{
    public class ImageTag
    {
        public Image Image { get; set; }
        public Tag Tag { get; set; }
        public int Image_ID { get; set; }
        public int Tag_ID { get; set; }
    }
}