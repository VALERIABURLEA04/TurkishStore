using businessLogic.Interfaces;
using businessLogic.Interfaces.Repositories;
using eUseControl.Domain.Entities.ListingEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace businessLogic.BLStruct
{
    public class CartBL
    {
        public class CartService : ICart
        {
            private readonly ICartRepository _cartRepository;
            private readonly IProductRepository _productRepository;

            public CartService(
                ICartRepository cartRepository,
                IProductRepository productRepository)
            {
                _cartRepository = cartRepository;
                _productRepository = productRepository;
            }

            public async Task<decimal> CalculateTotalAsync(int userId)
            {
                var items = await _cartRepository.GetItemsByUserIdAsync(userId);
                decimal total = 0;

                foreach (var item in items)
                {
                    total += item.UnitPrice * item.Quantity;
                }

                return total;
            }

            public async Task<int> AddItemAsync(int userId, int productId, int quantity)
            {
                if (quantity <= 0)
                    throw new ArgumentException("Quantity must be bigger than 0.");

                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new ArgumentException("Product doesn't exist.");

                var existingItem = await _cartRepository.GetItemByUserIdAndProductIdAsync(userId, productId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                    existingItem.TotalPrice = existingItem.UnitPrice * existingItem.Quantity;
                    await _cartRepository.UpdateAsync(existingItem);
                    return existingItem.Id;
                }

                var newItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = quantity,
                    TotalPrice = product.Price * quantity,
                    AddedDate = DateTime.Now
                };

                return await _cartRepository.CreateAsync(newItem);
            }

            public async Task<bool> RemoveItemAsync(int userId, int productId)
            {
                var item = await _cartRepository.GetItemByUserIdAndProductIdAsync(userId, productId);
                if (item == null)
                    return false;

                return await _cartRepository.DeleteAsync(item.Id);
            }

            public async Task<bool> UpdateQuantityAsync(int userId, int productId, int quantity)
            {
                if (quantity <= 0)
                    throw new ArgumentException("Cantitatea trebuie să fie mai mare decât 0.");

                var item = await _cartRepository.GetItemByUserIdAndProductIdAsync(userId, productId);
                if (item == null)
                    return false;

                item.Quantity = quantity;
                item.TotalPrice = item.UnitPrice * quantity;
                await _cartRepository.UpdateAsync(item);
                return true;
            }

            public async Task<List<CartItem>> GetBasketItemsAsync(int userId)
            {
                return await _cartRepository.GetItemsByUserIdAsync(userId);
            }

            public async Task<bool> ClearBasketAsync(int userId)
            {
                return await _cartRepository.DeleteAllByUserIdAsync(userId);
            }
        }
    }
}