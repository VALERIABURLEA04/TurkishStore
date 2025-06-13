using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.BlogEntities;
using eUseControl.Domain.Entities.ProductEntities;
using eUseControl.Domain.Entities.UserEntities;
using eUseControl.Domain.Enums;
using eUseControl.Helpers.AccessFlow;
using System;
using System.Collections.Generic;
using System.Linq;

public class DbInitializer
{
    public static void SeedAdmin()
    {
        using (var context = new EUseControlDbContext())
        {
            if (!context.Users.Any(u => u.Email == "admin@example.com"))
            {
                var adminUser = new User
                {
                    Name = "administrator",
                    Email = "admin@example.com",
                    Password = AccessHelper.HashPassword("admin123!"),
                    Level = eUseControl.Domain.Enums.UserRole.Admin,
                    LastLogin = DateTime.Now,
                    UserIp = "127.0.0.1"
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }
        }
    }

    public static void SeedProducts()
    {
        using (var context = new EUseControlDbContext())
        {
            if (!context.Products.Any())
            {
                List<Product> products = new List<Product> {
                   new Product
                {
                    Id = 1,
                    Name = "Esprit Ruffle Shirt",
                    Description = "Esprit Ruffle Shirt",
                    FullDescription = "A lightweight, casual shirt with ruffle detail perfect for summer.",
                    Price = 16.64m,
                    Sku = "ESPRIT-RUFFLE-01",
                    Category = ProductCategoryEnum.Women,
                    Stock = 100,
                    Weight = 0.25m,
                    Dimensions = "40 x 30 x 2 cm",
                    Materials = "100% Cotton",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 1, ImageUrl = "/Content/images/product-01.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 1, SizeValue = ProductSizeEnum.S.ToString() },
                        new ProductSize { ProductId = 1, SizeValue = ProductSizeEnum.M.ToString() },
                        new ProductSize { ProductId = 1, SizeValue = ProductSizeEnum.L.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 1, ColorValue = ProductColorEnum.Red.ToString() },
                        new ProductColor { ProductId = 1, ColorValue = ProductColorEnum.Blue.ToString() }
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Herschel Supply Backpack",
                    Description = "Classic Herschel backpack",
                    FullDescription = "Durable and stylish backpack from Herschel with multiple compartments.",
                    Price = 35.31m,
                    Sku = "HERSCHEL-BP-02",
                    Category = ProductCategoryEnum.Women,
                    Stock = 100,
                    Weight = 0.75m,
                    Dimensions = "45 x 30 x 15 cm",
                    Materials = "Polyester",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 2, ImageUrl = "/Content/images/product-02.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 2, SizeValue = ProductSizeEnum.M.ToString() },
                        new ProductSize { ProductId = 2, SizeValue = ProductSizeEnum.L.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 2, ColorValue = ProductColorEnum.Black.ToString() },
                        new ProductColor { ProductId = 2, ColorValue = ProductColorEnum.Grey.ToString() }
                    }
                },
                new Product
                {
                    Id = 3,
                    Name = "Only Check Trouser",
                    Description = "Only Check Trouser",
                    FullDescription = "Comfortable check-patterned trousers ideal for casual wear.",
                    Price = 25.50m,
                    Sku = "ONLY-CHECK-03",
                    Category = ProductCategoryEnum.Men,
                    Stock = 100,
                    Weight = 0.50m,
                    Dimensions = "100 x 50 x 2 cm",
                    Materials = "Cotton Blend",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 3, ImageUrl = "/Content/images/product-03.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 3, SizeValue = ProductSizeEnum.M.ToString() },
                        new ProductSize { ProductId = 3, SizeValue = ProductSizeEnum.L.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 3, ColorValue = ProductColorEnum.Black.ToString() },
                        new ProductColor { ProductId = 3, ColorValue = ProductColorEnum.Brown.ToString() }
                    }
                },
                new Product
                {
                    Id = 4,
                    Name = "Classic Trench Coat",
                    Description = "Classic Trench Coat",
                    FullDescription = "Timeless trench coat design offering elegance and protection against the elements.",
                    Price = 75.00m,
                    Sku = "TRENCH-COAT-04",
                    Category = ProductCategoryEnum.Women,
                    Stock = 100,
                    Weight = 1.20m,
                    Dimensions = "120 x 60 x 10 cm",
                    Materials = "100% Cotton",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 4, ImageUrl = "/Content/images/product-04.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 4, SizeValue = ProductSizeEnum.M.ToString() },
                        new ProductSize { ProductId = 4, SizeValue = ProductSizeEnum.L.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 4, ColorValue = ProductColorEnum.Grey.ToString() },
                        new ProductColor { ProductId = 4, ColorValue = ProductColorEnum.Black.ToString() }
                    }
                },
                new Product
                {
                    Id = 5,
                    Name = "Front Pocket Jumper",
                    Description = "Front Pocket Jumper",
                    FullDescription = "Cozy jumper featuring a convenient front pocket and soft knit fabric.",
                    Price = 34.75m,
                    Sku = "JUMPER-05",
                    Category = ProductCategoryEnum.Women,
                    Stock = 100,
                    Weight = 0.60m,
                    Dimensions = "80 x 60 x 5 cm",
                    Materials = "Wool Blend",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 5, ImageUrl = "/Content/images/product-05.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 5, SizeValue = ProductSizeEnum.S.ToString() },
                        new ProductSize { ProductId = 5, SizeValue = ProductSizeEnum.M.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 5, ColorValue = ProductColorEnum.Blue.ToString() },
                        new ProductColor { ProductId = 5, ColorValue = ProductColorEnum.Grey.ToString() }
                    }
                },
                new Product
                {
                    Id = 6,
                    Name = "Vintage Inspired Classic",
                    Description = "Vintage Inspired Classic",
                    FullDescription = "Classic timepiece with vintage-inspired design and stainless steel mesh band.",
                    Price = 93.20m,
                    Sku = "WATCH-MESH-06",
                    Category = ProductCategoryEnum.Watches,
                    Stock = 100,
                    Weight = 0.10m,
                    Dimensions = "4 x 4 x 1 cm",
                    Materials = "Stainless Steel",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 6, ImageUrl = "/Content/images/product-06.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>(),
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 6, ColorValue = ProductColorEnum.Pink.ToString() }
                    }
                },
                new Product
                {
                    Id = 7,
                    Name = "Shirt in Stretch Cotton",
                    Description = "Shirt in Stretch Cotton",
                    FullDescription = "Soft stretch-cotton shirt offering a comfortable fit and classic style.",
                    Price = 52.66m,
                    Sku = "STRETCH-SHIRT-07",
                    Category = ProductCategoryEnum.Women,
                    Stock = 100,
                    Weight = 0.30m,
                    Dimensions = "40 x 30 x 2 cm",
                    Materials = "95% Cotton, 5% Elastane",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 7, ImageUrl = "/Content/images/product-07.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 7, SizeValue = ProductSizeEnum.S.ToString() },
                        new ProductSize { ProductId = 7, SizeValue = ProductSizeEnum.L.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 7, ColorValue = ProductColorEnum.White.ToString() },
                        new ProductColor { ProductId = 7, ColorValue = ProductColorEnum.Pink.ToString() }
                    }
                },
                new Product
                {
                    Id = 8,
                    Name = "Pieces Metallic Printed",
                    Description = "Pieces Metallic Printed",
                    FullDescription = "Dress featuring metallic print detailing for a glamorous look.",
                    Price = 18.96m,
                    Sku = "METALLIC-DRESS-08",
                    Category = ProductCategoryEnum.Women,
                    Stock = 100,
                    Weight = 0.25m,
                    Dimensions = "90 x 60 x 2 cm",
                    Materials = "Polyester",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 8, ImageUrl = "/Content/images/product-08.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 8, SizeValue = ProductSizeEnum.S.ToString() },
                        new ProductSize { ProductId = 8, SizeValue = ProductSizeEnum.M.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 8, ColorValue = ProductColorEnum.Yellow.ToString() },
                        new ProductColor { ProductId = 8, ColorValue = ProductColorEnum.Grey.ToString() }
                    }
                },
                new Product
                {
                    Id = 9,
                    Name = "Converse All Star Hi Plimsolls",
                    Description = "Classic high-top canvas sneakers",
                    FullDescription = "Classic high-top canvas sneakers by Converse, comfortable and stylish.",
                    Price = 75.00m,
                    Sku = "CONVERSE-HI-09",
                    Category = ProductCategoryEnum.Shoes,
                    Stock = 100,
                    Weight = 0.80m,
                    Dimensions = "30 x 10 x 10 cm",
                    Materials = "Canvas, Rubber",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 9, ImageUrl = "/Content/images/product-09.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 9, SizeValue = ProductSizeEnum.M.ToString() },
                        new ProductSize { ProductId = 9, SizeValue = ProductSizeEnum.L.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 9, ColorValue = ProductColorEnum.White.ToString() },
                        new ProductColor { ProductId = 9, ColorValue = ProductColorEnum.Black.ToString() }
                    }
                },
                new Product
                {
                    Id = 10,
                    Name = "Femme T-Shirt In Stripe",
                    Description = "Striped t-shirt",
                    FullDescription = "Striped t-shirt with a relaxed fit, perfect for casual wear.",
                    Price = 25.85m,
                    Sku = "STRIPE-TS-10",
                    Category = ProductCategoryEnum.Women,
                    Stock = 100,
                    Weight = 0.22m,
                    Dimensions = "45 x 35 x 2 cm",
                    Materials = "100% Cotton",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 10, ImageUrl = "/Content/images/product-10.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 10, SizeValue = ProductSizeEnum.S.ToString() },
                        new ProductSize { ProductId = 10, SizeValue = ProductSizeEnum.M.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 10, ColorValue = ProductColorEnum.Blue.ToString() },
                        new ProductColor { ProductId = 10, ColorValue = ProductColorEnum.White.ToString() }
                    }
                },
                new Product
                {
                    Id = 17,
                    Name = "Retro Denim Jacket",
                    Description = "Classic denim jacket",
                    FullDescription = "Retro-inspired denim jacket with button-up front and chest pockets.",
                    Price = 49.99m,
                    Sku = "DENIM-JKT-17",
                    Category = ProductCategoryEnum.Men,
                    Stock = 50,
                    Weight = 0.70m,
                    Dimensions = "60 x 50 x 5 cm",
                    Materials = "100% Cotton Denim",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 17, ImageUrl = "/Content/images/product-04.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 17, SizeValue = ProductSizeEnum.M.ToString() },
                        new ProductSize { ProductId = 17, SizeValue = ProductSizeEnum.L.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 17, ColorValue = ProductColorEnum.Blue.ToString() }
                    }
                },
                new Product
                {
                    Id = 18,
                    Name = "Leather Belt",
                    Description = "Genuine leather belt",
                    FullDescription = "High-quality genuine leather belt with adjustable buckle.",
                    Price = 19.99m,
                    Sku = "BELT-18",
                    Category = ProductCategoryEnum.Men,
                    Stock = 200,
                    Weight = 0.15m,
                    Dimensions = "120 x 5 x 0.5 cm",
                    Materials = "100% Leather",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 18, ImageUrl = "/Content/images/product-09.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 18, SizeValue = ProductSizeEnum.S.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 18, ColorValue = ProductColorEnum.Brown.ToString() }
                    }
                },
                new Product
                {
                    Id = 19,
                    Name = "Sport Running Shoes",
                    Description = "Comfort running shoes",
                    FullDescription = "Lightweight sport running shoes designed for comfort and performance.",
                    Price = 59.99m,
                    Sku = "RUN-SHOES-19",
                    Category = ProductCategoryEnum.Shoes,
                    Stock = 120,
                    Weight = 0.85m,
                    Dimensions = "30 x 12 x 10 cm",
                    Materials = "Mesh, Rubber",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 19, ImageUrl = "/Content/images/product-03.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = 19, SizeValue = ProductSizeEnum.S.ToString() },
                        new ProductSize { ProductId = 19, SizeValue = ProductSizeEnum.M.ToString() }
                    },
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 19, ColorValue = ProductColorEnum.Black.ToString() },
                        new ProductColor { ProductId = 19, ColorValue = ProductColorEnum.White.ToString() }
                    }
                },
                new Product
                {
                    Id = 20,
                    Name = "Classic Aviator Sunglasses",
                    Description = "Aviator style sunglasses",
                    FullDescription = "Classic aviator sunglasses with UV protection and metal frame.",
                    Price = 29.49m,
                    Sku = "SUNGLASS-20",
                    Category = ProductCategoryEnum.Women,
                    Stock = 80,
                    Weight = 0.05m,
                    Dimensions = "15 x 5 x 5 cm",
                    Materials = "Metal, Glass",
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ProductId = 20, ImageUrl = "/Content/images/product-06.jpg", SortOrder = 1 }
                    },
                    ProductSizes = new List<ProductSize>(),
                    ProductColors = new List<ProductColor>
                    {
                        new ProductColor { ProductId = 20, ColorValue = ProductColorEnum.Black.ToString() }
                    }
                }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }

    public static void SeedBlogs()
    {
        using (var context = new EUseControlDbContext())
        {
            if (!context.BlogPosts.Any())
            {
                List<BlogPost> blogPosts = new List<BlogPost>
                {
                    new BlogPost {
                        Id = 1,
                        Title = "8 Inspiring Ways to Wear Dresses in the Winter",
                        Content = "Class aptent taciti sociosqu ad litora...Donec dictum vitae sapien eu varius.",
                        Author = "Admin",
                        PublishDate = new DateTime(2018, 1, 22),
                        ImageUrl = "/Content/images/blog-04.jpg",
                        Categories = "StreetStyle,Fashion,Couple",
                        CommentsCount = 8
                    },
                    new BlogPost {
                        Id = 2,
                        Title = "The Great Big List of Men’s Gifts for the Holidays",
                        Content = "Class aptent taciti sociosqu ad litora...Donec dictum vitae sapien eu varius.",
                        Author = "Admin",
                        PublishDate = new DateTime(2018, 1, 18),
                        ImageUrl = "/Content/images/blog-05.jpg",
                        Categories = "StreetStyle,Fashion,Couple",
                        CommentsCount = 8
                    },
                    new BlogPost {
                        Id = 3,
                        Title = "5 Winter-to-Spring Fashion Trends to Try Now",
                        Content = "Class aptent taciti sociosqu ad litora...Donec dictum vitae sapien eu varius.",
                        Author = "Admin",
                        PublishDate = new DateTime(2018, 1, 16),
                        ImageUrl = "/Content/images/blog-06.jpg",
                        Categories = "StreetStyle,Fashion,Couple",
                        CommentsCount = 8
                    }
                };

                context.BlogPosts.AddRange(blogPosts);
                context.SaveChanges();
            }
        }
    }
}