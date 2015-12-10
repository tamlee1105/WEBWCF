using System;
using System.Diagnostics;
using System.Linq;
using Business.Database;
using Business.Entity;
using Business.IRepository;

namespace Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ShopDataContext _data = ShopData.DataContext;

        #region Implementation of IProductRepository

        /// <summary>
        /// Get all items product in database
        /// </summary>
        /// <returns>List all items</returns>
        public IQueryable<Product> GetAll()
        {
            //return _data.SanPhams.ConvertToProduct();
            return _data.SanPhams.Select(p => p.ConvertToProduct());
        }

        public IQueryable<Product> GetAllOnTime()
        {
            return _data.SanPhams.ConvertToProduct();
        }

        public Product Find(int proId)
        {
            //return _data.SanPhams.First(p => p.ID == proId).ConvertToProduct();
            //return GetAll().ToList().FirstOrDefault(p => p.Id == proId);

            var priceRepository = new WeightedAverageUnitPriceRepository();
            var result = priceRepository.Find(proId, DateTime.Now);
            return result.Product;
        }

        /// <summary>
        /// Find all items by product name and category Id.
        /// Default category hidden, at times will find by product name
        /// </summary>
        /// <param name="productName">Name of product</param>
        /// <param name="cateId">Category Id</param>
        /// <returns>List items product</returns>
        public IQueryable<Product> Find(string productName, int cateId = 0)
        {
            if (productName == null) productName = string.Empty;
            var data = new ShopDataContext();
            var list = cateId == 0 ? data.SanPhams : data.SanPhams.Where(p => p.IDLoaiSanPham == cateId);
            return list.Where(p => p.TenSanPham.Contains(productName)).Select(p=>p.ConvertToProduct());
        }

        public IQueryable<Product> FindOnTime(string productName, int cateId = 0)
        {
            if (productName == null) productName = string.Empty;

            var list = cateId == 0 ? _data.SanPhams : _data.SanPhams.Where(p => p.IDLoaiSanPham == cateId);
            return list.Where(p => p.TenSanPham.Contains(productName)).ConvertToProduct();
        }

        ///// <summary>
        ///// Find all items by product name
        ///// </summary>
        ///// <param name="productName">Name of product</param>
        ///// <returns>List items product</returns>
        //public IQueryable<Product> Find(string productName)
        //{
        //    return _data.SanPhams.Where(p => p.TenSanPham.Contains(productName)).ConvertToProduct();
        //}



        /// <summary>
        /// Get all items products by category Id
        /// </summary>
        /// <param name="cateId">Id of category</param>
        /// <returns>List items products</returns>
        public IQueryable<Product> GetByCate(int cateId)
        {
            var list= _data.SanPhams.Where(p => p.IDLoaiSanPham == cateId);
            return list.Select(p => p.ConvertToProduct());
        }

        public IQueryable<Product> GetByCateOnTime(int cateId)
        {
            return _data.SanPhams.Where(p => p.IDLoaiSanPham == cateId).ConvertToProduct();
        }

        /// <summary>
        /// Add product in a pending  insert states to Data context
        /// </summary>
        /// <param name="product">Product insert to database</param>
        /// <returns>TRUE if success </returns>
        public bool Add(Product product)
        {
            try
            {
                var sp = product.ConvertToSanPham();
                _data.SanPhams.InsertOnSubmit(sp);
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Add product: " + exception.Message);
                return false;
            }

        }

        /// <summary>
        /// Update product in a pending state
        /// </summary>
        /// <param name="product">Product to update</param>
        /// <returns>TRUE if success</returns>
        public bool Edit(Product product)
        {
            try
            {
                var updateItem = _data.SanPhams.First(p => p.ID == product.Id);

                updateItem.ID = product.Id;
                updateItem.MaSanPham = product.Code;
                updateItem.TenSanPham = product.Name;
                updateItem.IDLoaiSanPham = product.Category.Id;
                updateItem.MoTa = product.Description;
                //updateItem.GiaBan = product.Price;
                updateItem.BaoHanh = product.Warranty;
                updateItem.Anh = product.Image;
                //updateItem.SoTon = product.Inventory;
                updateItem.Recycle = product.Recycle;

                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Edit product: " + exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Puts a product from this table into a pending delete state 
        /// </summary>
        /// <param name="productId">product id of delete product</param>
        /// <returns>TRUE if success</returns>
        public bool Delete(int productId)
        {
            try
            {
                var delItem = _data.SanPhams.First(p => p.ID == productId);
                _data.SanPhams.DeleteOnSubmit(delItem);
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Delete product: " + exception.Message);
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
            _data = ShopData.DataContext;
        }

        #endregion


    }
}