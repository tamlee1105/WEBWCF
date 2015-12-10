using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Business.Entity;
using Business.Repository;

namespace App
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShopService" in both code and config file together.
    public class ShopService : IShopService
    {

        #region Implementation of IShopService

        public Category[] GetAllCategories()
        {
            var _categoryRepository = new CategoryRepository();
            return _categoryRepository.GetAll().ToArray();
        }

        #endregion
    }
}
