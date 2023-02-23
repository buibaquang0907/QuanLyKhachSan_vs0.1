using QuanLyKhachSan_Final.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QuanLyKhachSan_Final
{
    public partial class frmHoaDon : Form
    {
        KhachSanDBContext context = new KhachSanDBContext();
        public string tenDangNhap;
        public string matKhau;
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void BrindGrid(List<HOADON> listHoaDon)
        {
            dgvHoaDon.Rows.Clear();
            foreach (var item in listHoaDon)
            {
                int index = dgvHoaDon.Rows.Add();
                dgvHoaDon.Rows[index].Cells[0].Value = item.maHoaDon;
                dgvHoaDon.Rows[index].Cells[1].Value = item.maPhieuThue;
                dgvHoaDon.Rows[index].Cells[2].Value = item.maNhanVien;
                dgvHoaDon.Rows[index].Cells[3].Value = item.ngayThanhToan;
                dgvHoaDon.Rows[index].Cells[4].Value = item.thanhTien;
            }
        }
        public bool kiemTraMaHD(string ma)
        {
            for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
            {
                if (dgvHoaDon.Rows[i].Cells[0].Value.ToString().CompareTo(ma) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool kiemTraMaPhieuThue(string ma)
        {
            for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
            {
                if (dgvHoaDon.Rows[i].Cells[1].Value.ToString().CompareTo(ma) == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            List<HOADON> listHoaDon = context.HOADONs.ToList();
            BrindGrid(listHoaDon);
            // Đỗ mã Phieu thu
            List<PHIEUTHUEPHONG> listPhieuThue = context.PHIEUTHUEPHONGs.ToList();
            cmbMaPT.DataSource = listPhieuThue;
            cmbMaPT.DisplayMember = "maPhieuThue";
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHD.Text != "")
                {
                    if (kiemTraMaHD(txtMaHD.Text))
                    {
                        if (kiemTraMaPhieuThue(cmbMaPT.Text))
                        {
                            TAIKHOAN nhanVienTruyCap = context.TAIKHOANs.FirstOrDefault(p => p.tenDangNhap == tenDangNhap);
                            string maNhanVienTruyCap = (string)nhanVienTruyCap.maNhanVien;

                            PHIEUTHUEPHONG phieuThanhToan = context.PHIEUTHUEPHONGs.FirstOrDefault(p => p.maPhieuThue == cmbMaPT.Text);
                            //Tinh tong ngay thue
                            DateTime ngayDen = Convert.ToDateTime(phieuThanhToan.ngayDen);
                            TimeSpan tongNgayThue = dtpNgayThanhToan.Value - ngayDen;

                            int tongSoNgay = tongNgayThue.Days;

                            float thanhTien = tongSoNgay * (int)phieuThanhToan.PHONG.LOAIPHONG.giaThue;

                            HOADON hoaDon = new HOADON() { maHoaDon = txtMaHD.Text, maPhieuThue = cmbMaPT.Text, maNhanVien = maNhanVienTruyCap, ngayThanhToan = dtpNgayThanhToan.Value, thanhTien = thanhTien };
                            context.HOADONs.Add(hoaDon);
                            context.SaveChanges();
                            List<HOADON> listHoaDon = context.HOADONs.ToList();
                            BrindGrid(listHoaDon);
                            MessageBox.Show("Khách Hàng: " + phieuThanhToan.KHACHHANG.tenKhachHang + "\nTong tien: " + thanhTien + "vnd", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        }
                        else
                        {
                            MessageBox.Show("Phiếu thuê đã có hóa đơn!", "CHÚ Ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã hóa đơn đã có!", "CHÚ Ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "CHÚ Ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtMaHD.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                HOADON dbDelete = context.HOADONs.FirstOrDefault(p => p.maHoaDon == txtMaHD.Text);
                if (dbDelete != null)
                {
                    if (txtMaHD.Text != "")
                    {
                        DialogResult re = MessageBox.Show("Bạn Có Muốn Xóa?", "XÁC NHẬN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (re == DialogResult.Yes)
                        {
                            context.HOADONs.Remove(dbDelete);
                            context.SaveChanges();
                            List<HOADON> listHoaDon = context.HOADONs.ToList();
                            BrindGrid(listHoaDon);
                            MessageBox.Show("Xóa hóa đơn thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập mã khách hàng cần xóa!", "CHÚ Ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng có mã: " + txtMaHD.Text);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSoCCCD_TextChanged(object sender, EventArgs e)
        {
            List<string> listMaPT = new List<string>();
            cmbMaPT.DataSource = null;
            List<PHIEUTHUEPHONG> listKhachHang = context.PHIEUTHUEPHONGs.ToList();
            
            foreach (PHIEUTHUEPHONG item in listKhachHang)
            {
                if (item.KHACHHANG.soCCCD.Contains(txtSoCCCD.Text))
                {
                    listMaPT.Add(item.maPhieuThue);
                }
            }
            cmbMaPT.DataSource = listMaPT;
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                txtMaHD.Text = (string)dgvHoaDon.SelectedRows[0].Cells[0].Value;
                cmbMaPT.Text = (string)dgvHoaDon.SelectedRows[0].Cells[1].Value;
                dtpNgayThanhToan.Value = DateTime.Parse(dgvHoaDon.SelectedRows[0].Cells[3].Value.ToString());
                List<PHIEUTHUEPHONG> listPT = context.PHIEUTHUEPHONGs.ToList();
                PHIEUTHUEPHONG khachThue = listPT.FirstOrDefault(p => p.maPhieuThue == (string)dgvHoaDon.SelectedRows[0].Cells[1].Value);
                txtSoCCCD.Text = khachThue.KHACHHANG.soCCCD;
            }
        }
    }
}