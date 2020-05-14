using dropShippingApp.Controllers;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Data.Repositories.FakeRepos;
using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class CategoryRepoTest
    {
        private ICategoryRepo categoryRepo;
        private ProductController controller;


        public CategoryRepoTest()
        {
            categoryRepo = new FakeCategoryRepo();
            
        }
        public void Dispose()
        {
            categoryRepo = null;
        }
        [Fact]
        public async Task TestAddCategory()
        {
            //arrange
            ProductCategory category = new ProductCategory
            {
                ProductCategoryID = 1,
                Name = "Test"
            };


            //act
            await categoryRepo.AddCategory(category);
            var result = categoryRepo.GetCategories;

            //assert
            Assert.Equal(category, result.Find(c => c == category));
        }
        [Fact]
        public async Task TestRemoveTeam()
        {
            //arrange
            ProductCategory category = new ProductCategory
            {
                ProductCategoryID = 1,
                Name = "Test"
            };
            await categoryRepo.AddCategory(category);

            //act
            await categoryRepo.RemoveCategory(category.ProductCategoryID);

            //assert
            Assert.DoesNotContain(category, categoryRepo.GetCategories);

        }
        [Fact]
        public async Task TestUpdateCategory()
        {
            ProductCategory category = new ProductCategory
            {
                ProductCategoryID = 1,
                Name = "Test"
            };
            ProductCategory updatedCategory = new ProductCategory
            {
                ProductCategoryID = 1,
                Name = " UpdatedTest"
            };
            await categoryRepo.AddCategory(category);

            //act
            await categoryRepo.UpdateCategory(updatedCategory);
            //assert
            Assert.DoesNotContain(category, categoryRepo.GetCategories);
            Assert.Contains(updatedCategory, categoryRepo.GetCategories);

        }
        [Fact]
        public async Task FindCategoryById()
        {
            //arrange
            ProductCategory category = new ProductCategory
            {
                ProductCategoryID = 1,
                Name = "Test"
            };
            await categoryRepo.AddCategory(category);

            //act
            ProductCategory result = await categoryRepo.GetCategoryById(1);


            //assert

            Assert.Equal(category, result);
        }

    }
}
