using System;
using Xunit;
using dropShippingApp.Models;
using dropShippingApp.Repositories;
using dropShippingApp.Controllers;

namespace dropShippingAppTests
{
	public class CustomProductTests
	{
		FakeCustomProductRepo repo;
		bool pass = false;

		[Fact]
		public void GetProductBySKU()
		{
			// Not working yet, will adjust later
			//// Arrange
			//CustomProduct product = new CustomProduct();
			//repo = new FakeCustomProductRepo();
			//var controller = new CustomProductController(repo);

			//// Act
			//product.SKU = 1234;
			//repo.AddCustomProduct(product);
			//controller.GetProductBySKU(1234);

			//// Assert
			//if (repo.CustomProducts[0].SKU == product.SKU)
			//{
			//	pass = true;
			//}
			//Assert.True(pass);
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
	}
}
