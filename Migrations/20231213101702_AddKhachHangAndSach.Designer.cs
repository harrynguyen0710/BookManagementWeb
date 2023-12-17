﻿// <auto-generated />
using BookManagementWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookManagementWeb.Migrations
{
    [DbContext(typeof(BookstoreDbContext))]
    [Migration("20231213101702_AddKhachHangAndSach")]
    partial class AddKhachHangAndSach
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookManagementWeb.Models.Entities.KhachHang", b =>
                {
                    b.Property<int>("MaKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhachHang"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("HoVaTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("SoTienNo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MaKhachHang");

                    b.ToTable("KHACHHANG");
                });

            modelBuilder.Entity("BookManagementWeb.Models.Entities.Sach", b =>
                {
                    b.Property<int>("MaSach")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSach"), 1L, 1);

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuongSach")
                        .HasColumnType("int");

                    b.Property<string>("TacGia")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenSach")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TheLoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaSach");

                    b.ToTable("SACH");
                });
#pragma warning restore 612, 618
        }
    }
}
