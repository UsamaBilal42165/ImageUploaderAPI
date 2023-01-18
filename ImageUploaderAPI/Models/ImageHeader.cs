using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageUploaderAPI.Models
{
    public class ImageHeader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PK { get; set; }
        public string ImageCaption { get; set; } = String.Empty;
        public string ImageUrl { get; set; } = String.Empty;
        public byte[]? ImageData { get; set; } = null;
    }
}
