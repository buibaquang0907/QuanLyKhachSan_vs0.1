using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyKhachSan_Final.Models
{
    public partial class KhachSanDBContext : DbContext
    {
        public KhachSanDBContext()
            : base("name=KhachSanDBContext1")
        {
        }

        public virtual DbSet<HOADON> HOADONs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<LOAIPHONG> LOAIPHONGs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }
        public virtual DbSet<PHIEUTHUEPHONG> PHIEUTHUEPHONGs { get; set; }
        public virtual DbSet<PHONG> PHONGs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
