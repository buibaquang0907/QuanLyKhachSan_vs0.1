namespace QuanLyKhachSan_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHANQUYEN")]
    public partial class PHANQUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHANQUYEN()
        {
            TAIKHOANs = new HashSet<TAIKHOAN>();
        }

        [Key]
        [StringLength(50)]
        public string maQuyen { get; set; }

        [StringLength(50)]
        public string tenQuyen { get; set; }

        [StringLength(50)]
        public string quyenHan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAIKHOAN> TAIKHOANs { get; set; }
    }
}
