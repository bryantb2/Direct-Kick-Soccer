using System;
using Xunit;
using dropShippingApp.Models;
using dropShippingApp.Repositories;

namespace dropShippingAppTests
{
	public class CustomProductTests
	{
		FakeCustomProductRepo repo;
		bool pass;


		[Fact]
		public void AddColorTest()
		{
			// Arrange
			var repo = new FakeCustomProductRepo();
			ProductColor colorTest = new ProductColor();
			bool pass = false;

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
			var repo = new FakeCustomProductRepo();
			ProductColor colorTest = new ProductColor();
			bool pass = false;

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
			var repo = new FakeCustomProductRepo();
			ProductSize sizeTest = new ProductSize();
			bool pass = false;

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
			var repo = new FakeCustomProductRepo();
			ProductSize sizeTest = new ProductSize();
			bool pass = false;

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
			var repo = new FakeCustomProductRepo();
			PricingHistory priceTest = new PricingHistory();
			bool pass = false;

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
			var repo = new FakeCustomProductRepo();
			PricingHistory priceTest = new PricingHistory();
			bool pass = false;

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

		[Fact]
		public void AddCustomImageTest()
		{
			// Arrange
			var repo = new FakeCustomProductRepo();
			CustomProduct imageTest = new CustomProduct();
			bool pass = false;

			// Act
			imageTest.CustomImageSVG = "asdfasdfas";
			imageTest.CustomImagePNG = "jkkkjjijk";
			repo.AddCustomImage(imageTest);

			// Assert
		}

		[Fact]
		public void RemoveCustomImageTest()
		{
			// Arrange
			var repo = new FakeCustomProductRepo();
			bool pass = false;

			// Act

			// Assert
		}

		[Fact]
		public void AddCustomProductTest()
		{
			// Arrange
			var repo = new FakeCustomProductRepo();
			bool pass = false;

			// Act

			// Assert
		}

		[Fact]
		public void RemoveCustomProductTest()
		{
			// Arrange
			var repo = new FakeCustomProductRepo();
			bool pass = false;

			// Act

			// Assert
		}
	}
}
