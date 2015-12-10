using System;
using System.Diagnostics;
using System.Linq;
using Business.Entity;
using Business.IRepository;
using Business.Database;
namespace Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private ShopDataContext _data = ShopData.DataContext;

        #region Implementation of ICategoryRepository

        public IQueryable<Category> GetAll()
        {
            return _data.LoaiSanPhams.
                Select(c => new Category
                                {
                                    Id = c.ID,
                                    Code = c.MaLoai,
                                    Name = c.TenLoai
                                });

        }

        public IQueryable<Category> Find(string cateName)
        {
            return _data.LoaiSanPhams
                .Where(c => c.TenLoai.Contains(cateName))
                .Select(c => new Category
                                 {
                                     Id = c.ID,
                                     Code = c.MaLoai,
                                     Name = c.TenLoai
                                 });
        }

        public bool Add(Category category)
        {
            try
            {
                var insertItem = new LoaiSanPham
                                     {
                                         MaLoai = category.Code,
                                         TenLoai = category.Name
                                     };
                _data.LoaiSanPhams.InsertOnSubmit(insertItem);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Add category: " + e.Message);
                return false;
            }

        }

        public bool Edit(Category category)
        {
            try
            {
                var updateItem = _data.LoaiSanPhams.First(c => c.ID == category.Id);
                updateItem.MaLoai = category.Code;
                updateItem.TenLoai = category.Name;
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Edit category: " + e.Message);
                return false;
            }
        }

        public bool Delete(int cateId)
        {
            try
            {
                var deleteItem = _data.LoaiSanPhams.First(c => c.ID == cateId);
                _data.LoaiSanPhams.DeleteOnSubmit(deleteItem);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Delete category: " + e.Message);
                return false;
            }
        }

        public void Commit()
        {
            _data.SubmitChanges();
        }

        public void Refresh()
        {
            _data.Dispose();
            _data=new ShopDataContext();
            //_data.Refresh(RefreshMode.OverwriteCurrentValues);
        }
        #endregion
    }
}