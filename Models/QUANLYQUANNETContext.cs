using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class QUANLYQUANNETContext : DbContext
    {
        public QUANLYQUANNETContext()
        {
        }

        public QUANLYQUANNETContext(DbContextOptions<QUANLYQUANNETContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaLam> CaLam { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<HoaDonChiTiet> HoaDonChiTiet { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<MayTinh> MayTinh { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<ThucDon> ThucDon { get; set; }
        public virtual DbSet<VaiTro> VaiTro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=HUANPCGAMING;Database=QUANLYQUANNET;Trusted_Connection=True;uid=sa;password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaLam>(entity =>
            {
                entity.HasKey(e => e.MaCa)
                    .HasName("PK__CaLam__27258E7B21D11FC7");

                entity.Property(e => e.MaCa).HasMaxLength(10);

                entity.Property(e => e.CaLam1)
                    .IsRequired()
                    .HasColumnName("CaLam")
                    .HasMaxLength(20);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.ViPham)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.CaLam)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__CaLam__MaNhanVie__3C69FB99");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HoaDon__835ED13BCAD4996E");

                entity.Property(e => e.MaHoaDon).HasMaxLength(10);

                entity.Property(e => e.MaKhachHang)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.MaMay)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.MaNhanVien)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.TriGia).HasColumnType("money");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__MaKhachH__48CFD27E");

                entity.HasOne(d => d.MaMayNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaMay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__MaMay__49C3F6B7");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__MaNhanVi__47DBAE45");
            });

            modelBuilder.Entity<HoaDonChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.MaHoaDon, e.MaMonAn })
                    .HasName("PK__HoaDonCh__C84FA0597982B342");

                entity.Property(e => e.MaHoaDon).HasMaxLength(10);

                entity.Property(e => e.MaMonAn).HasMaxLength(10);

                entity.HasOne(d => d.MaHoaDonNavigation)
                    .WithMany(p => p.HoaDonChiTiet)
                    .HasForeignKey(d => d.MaHoaDon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDonChi__MaHoa__4CA06362");

                entity.HasOne(d => d.MaMonAnNavigation)
                    .WithMany(p => p.HoaDonChiTiet)
                    .HasForeignKey(d => d.MaMonAn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDonChi__MaMon__4D94879B");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KhachHan__88D2F0E5A5DC9BB6");

                entity.Property(e => e.MaKhachHang).HasMaxLength(10);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaiKhoan)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.KhachHang)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__KhachHang__MaNha__3F466844");
            });

            modelBuilder.Entity<MayTinh>(entity =>
            {
                entity.HasKey(e => e.MaMay)
                    .HasName("PK__MayTinh__3A5BBB416C4BB8CA");

                entity.Property(e => e.MaMay).HasMaxLength(10);

                entity.Property(e => e.Cpu)
                    .IsRequired()
                    .HasColumnName("CPU")
                    .HasMaxLength(50);

                entity.Property(e => e.GiaTien).HasColumnType("money");

                entity.Property(e => e.Gpu)
                    .IsRequired()
                    .HasColumnName("GPU")
                    .HasMaxLength(50);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.Ram)
                    .IsRequired()
                    .HasColumnName("RAM")
                    .HasMaxLength(10);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.MayTinh)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__MayTinh__MaNhanV__44FF419A");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__NhanVien__77B2CA47779FA806");

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Gmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaVaiTro).HasMaxLength(10);

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.Property(e => e.TenVaiTro)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.MaVaiTroNavigation)
                    .WithMany(p => p.NhanVien)
                    .HasForeignKey(d => d.MaVaiTro)
                    .HasConstraintName("FK__NhanVien__MaVaiT__398D8EEE");
            });

            modelBuilder.Entity<ThucDon>(entity =>
            {
                entity.HasKey(e => e.MaMonAn)
                    .HasName("PK__ThucDon__B117162553D6F245");

                entity.Property(e => e.MaMonAn).HasMaxLength(10);

                entity.Property(e => e.Gia).HasColumnType("money");

                entity.Property(e => e.HinhAnh)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.TenMonAn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.ThucDon)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__ThucDon__MaNhanV__4222D4EF");
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.MaVaiTro)
                    .HasName("PK__VaiTro__C24C41CFECF1A872");

                entity.Property(e => e.MaVaiTro).HasMaxLength(10);

                entity.Property(e => e.TenVaiTro)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
