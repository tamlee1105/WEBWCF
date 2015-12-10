using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Business.Entity;
using Business.Database;
namespace Business.Repository
{
    public static class Utils
    {
        public static IQueryable<Product> ConvertToProduct(this IQueryable<SanPham> sanPhams)
        {
            var result = from p in sanPhams
                         let averPrice = p.GiaBinhQuans
                                .Where(g => g.ThoiGian <= DateTime.Now)
                                .OrderByDescending(g => g.ID).FirstOrDefault()
                         where averPrice != null
                         select new Product
                                    {
                                        Id = p.ID,
                                        Code = p.MaSanPham,
                                        Name = p.TenSanPham,
                                        Category =
                                            new Category
                                                {
                                                    Id = p.LoaiSanPham.ID,
                                                    Code = p.LoaiSanPham.MaLoai,
                                                    Name = p.LoaiSanPham.TenLoai
                                                },
                                        Description = p.MoTa,
                                        Price = averPrice.GiaBinQuan ?? decimal.Zero,
                                        Warranty = p.BaoHanh,
                                        Image = p.Anh,
                                        Inventory = averPrice.SoLuong ?? 0,
                                        UpdateTime = averPrice.ThoiGian ?? DateTime.Now.AddYears(-1),
                                        Recycle = p.Recycle ?? false

                                    };
            return result;
        }

        public static Product ConvertToProduct(this SanPham p)
        {
            var result = new Product
                         {
                             Id = p.ID,
                             Code = p.MaSanPham,
                             Name = p.TenSanPham,
                             Category =
                                 new Category
                                 {
                                     Id = p.LoaiSanPham.ID,
                                     Code = p.LoaiSanPham.MaLoai,
                                     Name = p.LoaiSanPham.TenLoai
                                 },
                             Description = p.MoTa,
                             //Price = p.GiaBan ?? decimal.Zero,
                             Warranty = p.BaoHanh,
                             Image = p.Anh,
                             //Inventory = p.SoTon ?? 0,
                             Recycle = p.Recycle ?? false

                         };
            return result;
        }

        public static SanPham ConvertToSanPham(this Product product)
        {
            return new SanPham
                       {
                           ID = product.Id,
                           MaSanPham = product.Code,
                           TenSanPham = product.Name,
                           IDLoaiSanPham = product.Category.Id,
                           MoTa = product.Description,
                           //GiaBan = product.Price,
                           BaoHanh = product.Warranty,
                           Anh = product.Image,
                           //SoTon = product.Inventory,
                           Recycle = product.Recycle
                       };
        }

        public static ChiTietPhieuNhapHang ConvertToChiTietPhieuNhapHang(this ReceiptNoteDetail noteDetail)
        {
            return new ChiTietPhieuNhapHang
                       {
                           IDSanPham = noteDetail.ProductId,
                           IDPhieuNhap = noteDetail.ReceiptNoteId,
                           SoLuong = noteDetail.Unit,
                           GiaVon = noteDetail.CostPrice,
                           Kho = noteDetail.Repository
                       };
        }

        public static PhieuNhapHang ConvertToPhieuNhapHang(this ReceiptNote note)
        {
            var listDetails = new EntitySet<ChiTietPhieuNhapHang>();
            foreach (var detail in note.ReceiptNoteDetails)
            {
                listDetails.Add(detail.ConvertToChiTietPhieuNhapHang());
            }
            return new PhieuNhapHang
                       {
                           ThoiGianNhap = note.Date,
                           TrangThai = note.Status,
                           ChiTietPhieuNhapHangs = listDetails,
                           IDPhieuNhap = note.DeliveryNoteId
                       };
        }


        public static Kho ConvertToKho(this MyRepo repo)
        {
            return new Kho
                       {
                           IDNhapXuat = repo.ReceiptDeliveryId,
                           LaPhieuNhap = repo.IsReceipt,
                           IDSanPham = repo.ProductId,
                           SoLuongTonKho = repo.StockUnit,
                           Kho1 = repo.Repository,
                           ThoiGian = repo.Date,
                           ConHang = repo.Inventory,
                           SoLuongConLanNhap = repo.ReceiptUnit
                       };
        }

        public static MyRepo ConvertToRepository(this Kho kho)
        {
            if (kho == null) return null;
            return new MyRepo
                       {
                           Id = kho.ID,
                           ReceiptDeliveryId = kho.IDNhapXuat ?? -1,
                           IsReceipt = kho.LaPhieuNhap ?? false,
                           ProductId = kho.IDSanPham ?? -1,
                           //Product = kho.SanPham.ConvertToProduct(),
                           StockUnit = kho.SoLuongTonKho ?? 0,
                           Repository = kho.Kho1 ?? 1,
                           Date = kho.ThoiGian ?? DateTime.Now.AddYears(-1),
                           Inventory = kho.ConHang ?? 0,
                           ReceiptUnit = kho.SoLuongConLanNhap ?? 0
                       };
        }

        public static ReceiptNote ConvertToReceiptNote(this PhieuNhapHang phieu)
        {
            var notedetails = phieu.ChiTietPhieuNhapHangs
                .Select(chitiet => new ReceiptNoteDetail
                                       {
                                           Id = chitiet.ID,
                                           ProductId = chitiet.IDSanPham ?? 0,
                                           Product = chitiet.SanPham.ConvertToProduct(),
                                           Unit = chitiet.SoLuong ?? 0,
                                           CostPrice = chitiet.GiaVon ?? decimal.Zero,
                                           Repository = chitiet.Kho ?? 0
                                       });
            return new ReceiptNote
                       {
                           Id = phieu.ID,
                           Date = phieu.ThoiGianNhap ?? DateTime.Now.AddYears(-1),
                           Status = phieu.TrangThai ?? false,
                           ReceiptNoteDetails = notedetails.ToList()
                       };
        }

        public static WeightedAverageUnitPrice ConvertToAverUnitPrice(this GiaBinhQuan gia)
        {
            var averprice = new WeightedAverageUnitPrice
                                {
                                    Id = gia.ID,
                                    AverageUnitPrice = gia.GiaBinQuan ?? decimal.Zero,
                                    Units = gia.SoLuong ?? 0,
                                    Date = gia.ThoiGian ?? DateTime.Now.AddYears(-1),
                                    ProductId = gia.IDSanPham ?? 0,
                                    Product = gia.SanPham.ConvertToProduct()
                                };
            averprice.Product.Inventory = averprice.Units;
            averprice.Product.Price = averprice.AverageUnitPrice;
            averprice.Product.UpdateTime = averprice.Date;

            return averprice;
        }


        public static WeightedAverageUnitPrice ConvertToLiteAverUnitPrice(this GiaBinhQuan gia)
        {
            var averprice = new WeightedAverageUnitPrice
                                {
                                    Id = gia.ID,
                                    AverageUnitPrice = gia.GiaBinQuan ?? decimal.Zero,
                                    Units = gia.SoLuong ?? 0,
                                    Date = gia.ThoiGian ?? DateTime.Now.AddYears(-1),
                                    ProductId = gia.IDSanPham ?? 0
                                };
            return averprice;
        }

        public static GiaBinhQuan ConvertToGiaBinhQuan(this WeightedAverageUnitPrice price)
        {
            return new GiaBinhQuan
                       {
                           ID = price.Id,
                           GiaBinQuan = price.AverageUnitPrice,
                           SoLuong = price.Units,
                           ThoiGian = price.Date,
                           IDSanPham = price.ProductId
                       };
        }

        public static ChiTietPhieuDatHang ConvertToChiTietPhieuDatHang(this DeliveryNoteDetail detail)
        {
            return new ChiTietPhieuDatHang
                       {
                           IDDatHang = detail.DeliveryNoteId,
                           IDSanPham = detail.ProductId,
                           SoLuong = detail.Unit,
                           TrangThai = detail.Status
                       };
        }

        public static PhieuDatHang ConvertToPhieuDatHang(this DeliveryNote note)
        {

            return new PhieuDatHang
                       {
                           ID = note.Id,
                           IDKhachHang = note.CustomerId,
                           ThoiGianDatHang = note.Date,
                           Duyet = note.Status,
                           KhachHang = note.Customer.ConvertToKhacHang()
                       };
        }

        public static KhachHang ConvertToKhacHang(this Customer cus)
        {
            return new KhachHang
                       {
                           HoTen = cus.Name,
                           SoDienThoai = cus.Phone,
                           DiaChi = cus.Address,
                           CMND = cus.CodeId,
                           Email = cus.Email
                       };
        }



        public static DeliveryNote ConvertToDeliveryNote(this PhieuDatHang phieu)
        {

            return new DeliveryNote
                       {
                           Id = phieu.ID,
                           CustomerId = phieu.IDKhachHang ?? 0,
                           Date = phieu.ThoiGianDatHang ?? DateTime.Now.AddYears(-1),
                           Status = phieu.Duyet ?? false,
                           NoteDetails = phieu.ChiTietPhieuDatHangs.Select(d => d.ConvertToDeliveryNoteDetails()).ToList()
                       };
        }

        public static DeliveryNoteDetail ConvertToDeliveryNoteDetails(this ChiTietPhieuDatHang chitiet)
        {
            var repo = new MyRepository();
            var item = new DeliveryNoteDetail
                       {
                           Id = chitiet.ID,
                           DeliveryNoteId = chitiet.IDDatHang ?? 0,
                           ProductId = chitiet.IDSanPham,
                           Product = chitiet.SanPham.ConvertToProduct(),
                           Unit = chitiet.SoLuong ?? 0,
                           Status = chitiet.TrangThai ?? false,
                           DeliveryNote = new DeliveryNote
                                              {
                                                  Id = chitiet.PhieuDatHang.ID,
                                                  Date = chitiet.PhieuDatHang.ThoiGianDatHang ?? DateTime.Now.AddYears(-1),
                                                  CustomerId = chitiet.PhieuDatHang.IDKhachHang ?? 0,
                                                  Status = chitiet.PhieuDatHang.Duyet ?? false
                                              }
                       };
            var repos = repo.GetRepoByDeliveryNoteDetails(item.Id) ?? new List<MyRepo>();
            item.Repos = repos.ToList();
            return item;
        }

        public static ReceiptNoteDetail ConvertToReceiptNoteDetail(this ChiTietPhieuNhapHang chitiet)
        {
            return new ReceiptNoteDetail
                       {
                           Unit = chitiet.SoLuong ?? 0,
                           ReceiptNote = new ReceiptNote
                                             {
                                                 Date = chitiet.PhieuNhapHang.ThoiGianNhap ?? DateTime.Now.AddYears(-1),
                                             }
                       };
        }
    }
}