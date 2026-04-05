using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class TAIKHOANMetadata
    {
        [DisplayName("TenDN")]
        [Required(ErrorMessage = "Error Empty")]

        public string TenDN { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Error Empty")]
        public string Email { get; set; }

        [DisplayName("MatKhau")]
        [Required(ErrorMessage = "Error Empty")]
        public string MatKhau { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Error Empty")]
        [Compare("MatKhau")]
        public string RePass { get; set; }

        private string _PhanQuyen;
        [DisplayName("PhanQuyen")]
        [Required(ErrorMessage = "Error Empty")]
        [StringLength(1, ErrorMessage = "PhanQuyen save only 1 character")]
        public string PhanQuyen
        {
            get { return _PhanQuyen; }
            set { _PhanQuyen = value?.Trim(); }
        }
        public string MaKH { get; set; }
        public string MaNV { get; set; }


    }
    public class PRODUCTMetadata
    {
        public string IDPro { get; set; }
        public string NamePro { get; set; }
        public string Desciption { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string MaLoaiSP { get; set; }


        [Required(ErrorMessage = "Hay chon file anh")]
        //[RegularExpression(@"[a-zA-Z0-9\s_\\.\-:] + (.png|.jpg|.gif)$",ErrorMessage = "Chi nhan dinh dang .PNG, .JPG , .GIF")]
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; } //Anh 1

        [Required(ErrorMessage = "Hay chon file anh")]
        //[RegularExpression(@"[a-zA-Z0-9\s_\\.\-:] + (.png|.jpg|.gif)$",ErrorMessage = "Chi nhan dinh dang .PNG, .JPG , .GIF")]
        [NotMapped]
        public HttpPostedFileBase UploadImage1 { get; set; } //Anh 1

        [Required(ErrorMessage = "Hay chon file anh")]
        //[RegularExpression(@"[a-zA-Z0-9\s_\\.\-:] + (.png|.jpg|.gif)$",ErrorMessage = "Chi nhan dinh dang .PNG, .JPG , .GIF")]
        [NotMapped]
        public HttpPostedFileBase UploadImage2 { get; set; } //Anh 1

    }
}