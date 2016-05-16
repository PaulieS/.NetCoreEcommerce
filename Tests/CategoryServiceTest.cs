//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CategoryService;
//using Microsoft.Data.Entity;
//using Xunit;

//namespace Tests
//{
//    public class CategoryServiceTest
//    {
//        private CategoryService.CategoryService GetInMemmoryCategoryService()
//        {
//            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
//            optionsBuilder.UseInMemoryDatabase();
//            var categoryServiceContext = new ProductsCategoryServiceContext(optionsBuilder.Options);
//            return new CategoryService.CategoryService(categoryServiceContext);
//        }

//        [Fact]
//        public void InitialiseCategoryService()
//        {
//            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
//            optionsBuilder.UseInMemoryDatabase();
//            var categoryServiceContext = new ProductsCategoryServiceContext(optionsBuilder.Options);
//            CategoryService.CategoryService categoryService = new CategoryService.CategoryService(categoryServiceContext);
//            Task.Run(async ()=> await categoryService.Initialise()).GetAwaiter().GetResult();
//        }
//        public void AddCategoryTest()
//        {
//            var categoryService = GetInMemmoryCategoryService();
//            var category = categoryService.Categories.Add("New Category 1");

//        }
//    }
//}
