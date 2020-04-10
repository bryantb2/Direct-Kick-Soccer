using System;
using Xunit;
using dropShippingApp.Models;
//using dropShippingApp.Repositories;
using dropShippingApp.Controllers;
using System.Collections.Generic;
using dropShippingApp.Data.Repositories;

namespace dropShippingAppTests
{
	/*public class CustomProductTests
	{
		FakeCustomProductRepo repo;
		bool pass = false;

		[Fact]
		public void GetProductBySKUTest()
		{
			// Arrange
			CustomProduct product;
			RosterProduct rosterProduct;
			ProductColor black;
			ProductColor red;
			ProductSize small; 
			repo = new FakeCustomProductRepo();
			var controller = new CustomProductController(repo);

			// Act
			black = new ProductColor()
			{
				ProductColorID = 2,
				ColorName = "red",
				AddOnPrice = 0.00m,
				IsColorActive = true,
			};
			red = new ProductColor()
			{
				ProductColorID = 1,
				ColorName = "black",
				AddOnPrice = 0.00m,
				IsColorActive = true,
			};
			repo.AddColor(black);
			repo.AddColor(red);
			small = new ProductSize()
			{
				ProductSizeID = 2,
				SizeName = "small",
				AddOnPrice = 0.00m,
				IsSizeActive = true,
			};
			repo.AddSize(small);
			rosterProduct = new RosterProduct()
			{
				RosterProductID = 98765,
				ModelNumber = 2345,
				ProductName = "Atheltic Cotton Tee",
				ProductDescription = "Cotton Tee for all your work needs",
				BaseColors = repo.OfferedColors,
				BaseSizes = repo.OfferedSizes,
				BasePrice = 12.99m,
				AddOnPrice = 0.00m,
			};
			product = new CustomProduct()
			{
				CustomProductID = 12345,
				SKU = 1234,
				ModelNumber = 2345,
				BaseProduct = rosterProduct,
				ProductTitle = "Test Cotton Tee",
				ProductDescription = "This is a Test for a CustomProduct",
				AddOnPrice = 0.00m,
				CustomImagePNG = "jijkki",
				CustomImageSVG = "fhfyfhfyfh",
				IsProductActive = true,
			};
			repo.AddCustomProduct(product);
			controller.GetProductBySKU(1234);

			// Assert
			if (repo.CustomProducts[0].SKU == product.SKU)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		[Fact]
		public void GetProductByModelNumberTest()
		{
			// Arrange
			CustomProduct product;
			RosterProduct rosterProduct;
			ProductColor black;
			ProductColor red;
			ProductSize small;
			repo = new FakeCustomProductRepo();
			var controller = new CustomProductController(repo);

			// Act
			black = new ProductColor()
			{
				ProductColorID = 2,
				ColorName = "red",
				AddOnPrice = 0.00m,
				IsColorActive = true,
			};
			red = new ProductColor()
			{
				ProductColorID = 1,
				ColorName = "black",
				AddOnPrice = 0.00m,
				IsColorActive = true,
			};
			repo.AddColor(black);
			repo.AddColor(red);
			small = new ProductSize()
			{
				ProductSizeID = 2,
				SizeName = "small",
				AddOnPrice = 0.00m,
				IsSizeActive = true,
			};
			repo.AddSize(small);
			rosterProduct = new RosterProduct()
			{
				RosterProductID = 98765,
				ModelNumber = 2345,
				ProductName = "Atheltic Cotton Tee",
				ProductDescription = "Cotton Tee for all your work needs",
				BaseColors = repo.OfferedColors,
				BaseSizes = repo.OfferedSizes,
				BasePrice = 12.99m,
				AddOnPrice = 0.00m,
			};
			product = new CustomProduct()
			{
				CustomProductID = 12345,
				SKU = 1234,
				ModelNumber = 2345,
				BaseProduct = rosterProduct,
				ProductTitle = "Test Cotton Tee",
				ProductDescription = "This is a Test for a CustomProduct",
				AddOnPrice = 0.00m,
				CustomImagePNG = "jijkki",
				CustomImageSVG = "fhfyfhfyfh",
				IsProductActive = true,
			};
			repo.AddCustomProduct(product);
			controller.GetProductBySKU(1234);

			// Assert
			if (repo.CustomProducts[0].ModelNumber == product.ModelNumber)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		[Fact]
		public void AddColorTest()
		{
			// Arrange
			repo = new FakeCustomProductRepo();
			ProductColor colorTest = new ProductColor();

			// Act
			colorTest.ColorName = "blue";
			repo.AddColor(colorTest);


			// Assert
			if (repo.OfferedColors.Contains(colorTest) == true)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		[Fact]
		public void RemoveColorTest()
		{
			// Arrange
			repo = new FakeCustomProductRepo();
			ProductColor colorTest = new ProductColor();

			// Act
			colorTest.ColorName = "blue";
			repo.RemoveColor(colorTest);

			// Assert
			if (repo.OfferedColors.Contains(colorTest) == false)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		[Fact]
		public void AddSizeTest()
		{
			// Arrange
			repo = new FakeCustomProductRepo();
			ProductSize sizeTest = new ProductSize();

			// Act
			sizeTest.SizeName = "Large";
			repo.AddSize(sizeTest);

			// Assert
			if (repo.OfferedSizes.Contains(sizeTest) == true)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		[Fact]
		public void RemoveSizeTest()
		{
			// Arrange
			repo = new FakeCustomProductRepo();
			ProductSize sizeTest = new ProductSize();

			// Act
			repo.RemoveSize(sizeTest);

			// Assert
			if (repo.OfferedSizes.Contains(sizeTest) == false)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		[Fact]
		public void AddPriceChangeTest()
		{
			// Arrange
			repo = new FakeCustomProductRepo();
			PricingHistory priceTest = new PricingHistory();

			// Act
			priceTest.NewPrice = 13.99m;
			repo.AddPriceChange(priceTest);

			// Assert
			if (repo.PricingHistory.Contains(priceTest) == true)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		[Fact]
		public void RemovePriceChangeTest()
		{
			// Arrange
			repo = new FakeCustomProductRepo();
			PricingHistory priceTest = new PricingHistory();

			// Act
			priceTest.NewPrice = 13.99m;
			repo.RemovePriceChange(priceTest);

			// Assert
			if (repo.PricingHistory.Contains(priceTest) == false)
			{
				pass = true;
			}
			Assert.True(pass);
		}

		//[Fact]
		//public void AddCustomImageTest()
		//{
		//	// Not fully implemented yet...

		//	// Arrange
		//	repo = new FakeCustomProductRepo();
		//	CustomProduct imageTest = new CustomProduct();

		//	// Act
		//	imageTest.CustomImageSVG = "asdfasdfas";
		//	imageTest.CustomImagePNG = "jkkkjjijk";
		//	repo.AddCustomImage(imageTest);

		//	// Assert
		//	if ()
		//}

		//[Fact]
		//public void RemoveCustomImageTest()
		//{
		//	// Arrange
		//	repo = new FakeCustomProductRepo();

		//	// Act

		//	// Assert
		//}

		//[Fact]
		//public void AddCustomProductTest()
		//{
		//	// Arrange
		//	repo = new FakeCustomProductRepo();

		//	// Act

		//	// Assert
		//}

		//[Fact]
		//public void RemoveCustomProductTest()
		//{
		//	// Arrange
		//	repo = new FakeCustomProductRepo();

		//	// Act

		//	// Assert
		//}
	}*/
}
