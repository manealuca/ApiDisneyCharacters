using System;
using System.Text.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using disneyapi;
using Data;

namespace APIDisneyCharacter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        [Route("/category")]
        [HttpPost]
        public IActionResult PostCategory(CategoryEntity oCategory)
        {
            try
            {
                using (var db = new DisneyContext())
                {
                    db.Category.Add(oCategory);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok(oCategory);
        }

        [Route("/categorys")]
        [HttpGet]
        public IActionResult GetCategoryList()
        {
            List<CategoryEntity> categoryList = new List<CategoryEntity>();
            try
            {
                using (var db = new DisneyContext())
                {
                   categoryList = db.Category.ToList();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok(categoryList);
        }
        [Route("/category")]
        [HttpPut]
        public IActionResult UpdateCategory(CategoryEntity oCategory)
        {
            try
            {
                using (var db = new DisneyContext())
                {
                    db.Category.Update(oCategory);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok($"Categoria: {oCategory.CategoryName} actualizada correctamente");
        }

        [Route("/category")]
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            CategoryEntity oCategory = new CategoryEntity();
            try
            {
                using (var db = new DisneyContext())
                {
                    oCategory = db.Category.Find(id);
                    db.Remove(oCategory);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok($"Categoria: {oCategory.CategoryName} Elimianda correctamente");
        }
    }
}
