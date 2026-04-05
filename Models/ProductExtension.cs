using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace QLCoffee.Models // Đảm bảo namespace này khớp với namespace trong project của bạn
{
    // Bổ sung các thuộc tính ảo để upload ảnh mà không lo bị EF ghi đè
    public partial class PRODUCT
    {
        public HttpPostedFileBase UploadImage { get; set; }
        public HttpPostedFileBase UploadImage1 { get; set; }
        public HttpPostedFileBase UploadImage2 { get; set; }
        
        public partial class TAIKHOAN
        {
            [NotMapped] // Thuộc tính này báo cho SQL biết đây là biến ảo, không cần lưu vào Database
            public string RePass { get; set; }
        }
    }
}