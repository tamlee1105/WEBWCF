using System.Collections.Generic;
using System.Linq;
using Business.Entity;

namespace Business.IRepository
{
    public interface IProductRepository
    {

        /// <summary>
        /// Get all items product in database
        /// </summary>
        /// <returns>List all items</returns>
        IQueryable<Product> GetAll();

        IQueryable<Product> GetAllOnTime();
        
        Product Find(int proId);

        /// <summary>
        /// Find all items by product name and category Id.
        /// Default category hidden, at times will find by product name
        /// </summary>
        /// <param name="productName">Name of product</param>
        /// <param name="cateId">Category Id</param>
        /// <returns>List items product</returns>
        IQueryable<Product> Find(string productName, int cateId = 0);

        IQueryable<Product> FindOnTime(string productName, int cateId = 0);

        /// <summary>
        /// Get all items products by category Id
        /// </summary>
        /// <param name="cateId">Id of category</param>
        /// <returns>List items products</returns>
        IQueryable<Product> GetByCate(int cateId);
        IQueryable<Product> GetByCateOnTime(int cateId);
        /// <summary>
        /// Add product in a pending  insert states to Data context
        /// </summary>
        /// <param name="product">Product insert to database</param>
        /// <returns>TRUE if success </returns>
        bool Add(Product product);

        /// <summary>
        /// Update product in a pending state
        /// </summary>
        /// <param name="product">Product to update</param>
        /// <returns>TRUE if success</returns>
        bool Edit(Product product);

        /// <summary>
        /// Puts a product from this table into a pending delete state 
        /// </summary>
        /// <param name="productId">product id of delete product</param>
        /// <returns>TRUE if success</returns>
        bool Delete(int productId);

        void Commit();

        void Refresh();
    }
}