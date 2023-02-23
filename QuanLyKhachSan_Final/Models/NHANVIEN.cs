namespace QuanLyKhachSan_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            HOADONs = new HashSet<HOADON>();
            PHIEUTHUEPHONGs = new HashSet<PHIEUTHUEPHONG>();
            TAIKHOANs = new HashSet<TAIKHOAN>();
        }

        [Key]
        [StringLength(50)]
        public string maNhanVien { get; set; }

        [StringLength(50)]
        public string tenNhanVien { get; set; }

        [StringLength(50)]
        public string gioiTinh { get; set; }

        [StringLength(50)]
        public string soCCCD { get; set; }

        [StringLength(50)]
        public string diaChi { get; set; }

        [StringLength(50)]
        public string soDienThoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUTHUEPHONG> PHIEUTHUEPHONGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAIKHOAN> TAIKHOANs { get; set; }
    }
}
