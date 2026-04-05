using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    [MetadataType(typeof(TAIKHOANMetadata))]
    public partial class TAIKHOAN
    {
        [NotMapped]
        [Required(ErrorMessage = "Error Empty")]
        [Compare("MatKhau")]
        public string RePass { get; set; }
    }
  
    //[MetadataType(typeof(PRODUCTMetadata))]
    //public partial class PRODUCT
    //{

    //    [Required(ErrorMessage = "Hay chon file anh")]
    //    //[RegularExpression(@"[a-zA-Z0-9\s_\\.\-:] + (.png|.jpg|.gif)$",ErrorMessage = "Chi nhan dinh dang .PNG, .JPG , .GIF")]
    //    [NotMapped]
    //    public HttpPostedFileBase UploadImage { get; set; } //Anh 1

    //    [Required(ErrorMessage = "Hay chon file anh")]
    //    //[RegularExpression(@"[a-zA-Z0-9\s_\\.\-:] + (.png|.jpg|.gif)$",ErrorMessage = "Chi nhan dinh dang .PNG, .JPG , .GIF")]
    //    [NotMapped]
    //    public HttpPostedFileBase UploadImage1 { get; set; } //Anh 1

    //    [Required(ErrorMessage = "Hay chon file anh")]
    //    //[RegularExpression(@"[a-zA-Z0-9\s_\\.\-:] + (.png|.jpg|.gif)$",ErrorMessage = "Chi nhan dinh dang .PNG, .JPG , .GIF")]
    //    [NotMapped]
    //    public HttpPostedFileBase UploadImage2 { get; set; } //Anh 1
    //}
}