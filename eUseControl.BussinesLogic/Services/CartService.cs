using businessLogic.Dtos.CartDtos;
using eUseControl.DataAccesss.Repositories;
using eUseControl.Domain.Entities.ListingEntities;
using eUseControl.Domain.Repositories;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eUSeControl.BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly IProductRepository _productRepository;
        private bool _disposed;

        private static CartService _instance;
        private static readonly object _lock = new object();

        private CartService()
        {
            _repository = CartRepository.GetInstance();
            _productRepository = ProductRepository.GetInstance();
        }

        public static CartService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new CartService();
                }
            }
            return _instance;
        }

        public async Task<string> AddCartItemAsync(UpsertCartItemDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var existing = await _repository.GetItemByUserIdAndProductIdAsync(model.UserId, model.ProductId);

            var product = await _productRepository.GetProductByIdAsync(model.ProductId);
            int? sizeId = product?.ProductSizes.FirstOrDefault(ps => ps.SizeValue == model.Size)?.Id;
            int? colorId = product?.ProductColors.FirstOrDefault(pc => pc.ColorValue == model.Color)?.Id;

            if (existing != null)
            {
                existing.Quantity += model.Quantity;
                existing.SizeId = sizeId;
                existing.ColorId = colorId;
                await _repository.UpdateAsync(existing);
                return "Cart item updated.";
            }

            var cartItem = new Cart
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                AddedDate = DateTime.UtcNow,
                SizeId = sizeId,
                ColorId = colorId
            };
            var id = await _repository.CreateAsync(cartItem);
            return id > 0 ? "Cart item added." : "Failed to add cart item.";
        }

        public async Task<string> RemoveCartItemAsync(int cartItemId)
        {
            if (cartItemId <= 0)
                return "Invalid cart item id.";

            var success = await _repository.DeleteAsync(cartItemId);
            return success ? "Cart item removed." : "Cart item not found.";
        }

        public async Task<string> RemoveAllCartItemsAsync(int userId)
        {
            if (userId <= 0)
                return "Invalid user id.";

            var success = await _repository.DeleteAllByUserIdAsync(userId);
            return success ? "All cart items removed." : "No cart items to remove.";
        }

        public async Task<int> UpdateCartItemQuantityAsync(int userId, int cartId, int quantity)
        {
            if (userId <= 0 || cartId <= 0 || quantity <= 0)
                return 0;

            var items = await _repository.GetCartItemsByUserIdAsync(userId);
            var item = items.SingleOrDefault(c => c.Id == cartId);
            if (item == null)
                return 0;

            item.Quantity = quantity;
            await _repository.UpdateAsync(item);
            return item.Quantity;
        }

        public int GetProductsFromCartByUserId(int userId)
        {
            if (userId <= 0)
                return 0;

            var items = _repository.GetCartItemsByUserId(userId);
            return items.Count();
        }

        public async Task<List<CartItemDto>> GetCartItemsAsync(int userId)
        {
            var cartItems = await _repository.GetCartItemsByUserIdAsync(userId);

            var result = cartItems
                .Select(x => new CartItemDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Quantity,
                    ImageUrl = x.Product.ProductImages.FirstOrDefault()?.ImageUrl,
                })
                .ToList();

            return result;
        }
    }
}