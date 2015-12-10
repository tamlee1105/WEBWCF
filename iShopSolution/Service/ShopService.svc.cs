using System;
using System.Diagnostics;
using System.Linq;
using Business.Entity;
using Business.Repository;

namespace Service
{

    public class ShopService : IShopService
    {
        #region Implementation of IShopService

        public Category[] GetAllCategories()
        {
            try
            {
                var _categoryRepository = new CategoryRepository();
                return _categoryRepository.GetAll().ToArray();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Product[] GetByCate(int cateId)
        {
            try
            {
                var _repository = new ProductRepository();

                var items = cateId == 0
                            ? _repository.GetAllOnTime().ToArray()
                            : _repository.GetByCateOnTime(cateId).ToArray();


                return items;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Product GetProduct(int proId)
        {
            try
            {
                var _repository = new ProductRepository();
                var item = _repository.Find(proId);

                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Product[] Find(string productName, int cateId)
        {
            try
            {
                var repository = new ProductRepository();
                return repository.FindOnTime(productName, cateId).ToArray();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public WeightedAverageUnitPrice FindPrice(int proId, DateTime date)
        {
            try
            {
                var repository = new WeightedAverageUnitPriceRepository();
                return repository.Find(proId, date);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int ProgressOrder(DeliveryNote note)
        {
            try
            {
                var repository = new DeliveryNoteRepository();
                return repository.ProgressShoppingCart(note);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Service progress order: " + exception.Message);
                return -1;
            }
        }

        public DeliveryNote GetDelivery(int noteId)
        {
            try
            {
                var repository = new DeliveryNoteRepository();
                return repository.Find(noteId);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Get delivery: " + exception.Message);
                return null;
            }
        }

        public byte GetStatusDelivery(int noteId)
        {
            try
            {
                var repository = new DeliveryNoteRepository();
                return repository.GetStatusDelivery(noteId);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Get Status Delivery " + exception.Message);
                return 1;
            }
        }

        #endregion
    }
}
