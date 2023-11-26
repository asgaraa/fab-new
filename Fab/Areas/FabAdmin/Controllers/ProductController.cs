using Fab.Data;
using Fab.Models.AboutFolder;
using Fab.Models.CategoriesFolder;
using Fab.Models.ProductFolder;
using Fab.Models.SubcategoryFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.CategoriesFolder;
using FabAdmin.ViewModels.ProductFolder;
using FabAdmin.ViewModels.Subcategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Product";
            ViewBag.CurrentAction = "Index";
            var products = await _context.Products.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();

            return View(products);
        }


        public async Task<IActionResult> Create()
        {

            ViewBag.Categories = await _context.Categories.Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.FirstOrDefault(het => het.LangCode == "en").Name
            }).ToListAsync();
            ViewBag.SubCategories = await _context.Subcategories.Select(he => new SubcategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.FirstOrDefault(het => het.LangCode == "en").Name
            }).ToListAsync();

            ViewBag.Applicat = await _context.ApplicationFields.Select(he => new ApplicationProductVm
            {
                Id = he.Id,
                Name = he.Translates.FirstOrDefault(het => het.LangCode == "en").Name
            }).ToListAsync();
            ViewBag.Appear = await _context.AppearanceFields.Select(he => new ApperanceProductVm
            {
                Id = he.Id,
                Name = he.Translates.FirstOrDefault(het => het.LangCode == "en").Name
            }).ToListAsync();
            ViewBag.Char = await _context.Characteristics.Select(he => new CharProductVM
            {
                Id = he.Id,
                Name = he.Translates.FirstOrDefault(het => het.LangCode == "en").Name
            }).ToListAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductVM model)
        {
            List<ProductTranslate> productTranslates = new();
            foreach (var translate in model.Translates)
            {
                ProductTranslate productTranslate = new()
                {
                    Name = translate.Name,
                    Desc = translate.Desc,
                    ProducerCountry = translate.ProducerCountry,
                    LangCode = translate.LangCode,
                    Size = translate.Size,
                    Length= translate.Length,
                    Color = translate.Color
                };
                productTranslates.Add(productTranslate);
            }

            List<ProductImages> productImages = new();
            foreach (var photo in model.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", fileName);
                await FileHelper.SaveFileAsync(path, photo);

                ProductImages newImage = new()
                {
                    //Image = "http://134.209.118.89/" + "ModelImages/ProductImages/" + fileName,
                    Image =  fileName,

                };
                productImages.Add(newImage);
            }
            productImages.FirstOrDefault().IsMain = true;

            string logoFileName = Guid.NewGuid().ToString() + "_" + model.Properties.FileName;
            string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", logoFileName);
            await FileHelper.SaveFileAsync(logoPath, model.Properties);


            var product = new Product
            {
                CategoryId = model.CategoryId,
                SubcategoryId = model.SubCategoryId,
                Brand = model.Brand,
                ProductCode = model.ProductCode,
                Images = productImages,
                Translates = productTranslates,
                //Properties = "http://134.209.118.89/" + "ModelImages/ProductImages/"+ logoFileName,
                Properties =  logoFileName,

                AppearanceFieldId = model.AppearId,
                ApplicationFieldId=model.ApplicatId,
                CharacteristicId=model.CharId
            };


            await _context.ProductTranslates.AddRangeAsync(productTranslates);
            await _context.ProductImages.AddRangeAsync(productImages);
            await _context.Products.AddAsync(product);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {

            var product = await _context.Products.Include(x => x.Translates).Include(m => m.Images).FirstOrDefaultAsync(x => x.Id == Id);

            ViewBag.Category = await _context.Categories.Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisCategory = await _context.Categories.Where(m => m.Id == product.CategoryId).Select(he => new CategoryDTO
            {
                Id = product.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.Subcategory = await _context.Subcategories.Where(m => m.CategoryId == product.CategoryId).Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisSubcategory = await _context.Subcategories.Where(m => m.Id == product.SubcategoryId).Select(he => new CategoryDTO
            {
                Id = product.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.Characteristic = await _context.Characteristics.Where(m => m.Id == product.CharacteristicId).Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisCharacteristic = await _context.Characteristics.Where(m => m.Id == product.CharacteristicId).Select(he => new CategoryDTO
            {
                Id = product.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.Application = await _context.ApplicationFields.Where(m => m.Id == product.ApplicationFieldId).Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisApplication = await _context.ApplicationFields.Where(m => m.Id == product.ApplicationFieldId).Select(he => new CategoryDTO
            {
                Id = product.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.Appearence = await _context.AppearanceFields.Where(m => m.Id == product.AppearanceFieldId).Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisAppearence = await _context.AppearanceFields.Where(m => m.Id == product.AppearanceFieldId).Select(he => new CategoryDTO
            {
                Id = product.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();



            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM product, int Id)
        {
            var dbProduct = await _context.Products.Include(s => s.Translates).Include(m => m.Images).FirstOrDefaultAsync(s => s.Id == Id);

            if (dbProduct == null) return View();

            dbProduct.Translates.Clear();
            foreach (var translate in product.Translates)
            {
                ProductTranslate productTranslate = new()
                {
                    LangCode = translate.LangCode,
                    Name = translate.Name,
                    Desc = translate.Desc,
                    Size = translate.Size,
                    ProducerCountry = translate.ProducerCountry,
                    Length = translate.Length,
                    Color = translate.Color,
                };
                dbProduct.Translates.Add(productTranslate);
            }

            dbProduct.CategoryId = product.CategoryId;
            dbProduct.SubcategoryId = product.SubCategoryId;
            if (product.CharId != 0)
            {
                dbProduct.CharacteristicId = product.CharId;
            }
            if (product.ApplicatId != 0)
            {
                dbProduct.ApplicationFieldId = product.ApplicatId;
            }
            if (product.AppearId != 0)
            {
                dbProduct.AppearanceFieldId = product.AppearId;
            }



            if (product.Images != null )
            {
                List<ProductImages> productImages = new();
                foreach (var image in dbProduct.Images)
                {
                    //var pathbanner = image.Image.Replace("http://134.209.118.89/", "");
                    var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", image.Image);
                    FileHelper.DeleteFile(filebanner);
                }

                foreach (var photo in product.Images)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", fileName);
                    await FileHelper.SaveFileAsync(path, photo);

                    ProductImages newImage = new()
                    {
                        //Image = "http://134.209.118.89/" + "ModelImages/ProductImages/"+ fileName,
                        Image =  fileName,

                    };
                    productImages.Add(newImage);
                }
                productImages.FirstOrDefault().IsMain = true;
            }

            if (product.Properties != null)
            {
                //var pathbanner = dbProduct.Properties.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", dbProduct.Properties);
                FileHelper.DeleteFile(filebanner);

                string logoFileName = Guid.NewGuid().ToString() + "_" + product.Properties.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, product.Properties);
                //dbProduct.Properties = "http://134.209.118.89/" + "ModelImages/ProductImages/" + logoFileName;
                dbProduct.Properties = logoFileName;


            }





            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();

            var product = await _context.Products.Include(m => m.Images).Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            if (product.Properties != null && product.Properties != "")
            {
                //var pathbanner = product.Properties.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", product.Properties);
                FileHelper.DeleteFile(filebanner);
            }

            if (product.Images != null)
            {
                foreach (var image in product.Images)
                {
                    //var pathbanner = image.Image.Replace("http://134.209.118.89/", "");
                    var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ProductImages/", image.Image);
                    FileHelper.DeleteFile(filebanner);
                }
            }
            


            _context.ProductTranslates.RemoveRange(product.Translates);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






        //[HttpPost]
        //public async Task<ActionResult> GetSubCategories(int categoryId)
        //{
        //    var subCategories = await _context.Subcategories
        //        .Where(x => x.CategoryId == categoryId)
        //        .Select(he => new SubcategoryDTO
        //        {
        //            Id = he.Id,
        //            Name = he.Translates.FirstOrDefault(het => het.LangCode == "en").Name
        //        })
        //        .ToListAsync();


        //    var exProp = await _context.ExProps
        //        .Where(x => x.CategoryId == categoryId)
        //        .Select(he => new ExPropDTO
        //        {
        //            Id = he.Id,
        //            Names = he.Translates.FirstOrDefault(het => het.LangCode == "en").Name,
        //            ExPropValues = he.Values.Select(x => new Fab.Areas.FabAdmin.ViewModels.ExPropFolder.ExpropValueDTO
        //            {
        //                Id = x.Id,
        //                Name = x.ExPropValueTranslates.FirstOrDefault().Value
        //            }).ToList(),
        //        })
        //        .ToListAsync();


        //    return Json(new { ExtraProperties = exProp, Subcategories = subCategories });
        //}



    }
}
