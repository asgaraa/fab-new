using Fab.Areas.FabAdmin.Helpers;
using Fab.Areas.FabAdmin.ViewModels.Gorunus;
using Fab.Data;
using Fab.Models.CategoriesFolder;
using Fab.Models.Tetbiq;
using Fab.ViewModels.ProductVM;
using FabAdmin.ViewModels.Subcategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Immutable;
using System.Drawing.Printing;
using System.Linq;

namespace Fab.Controllers
{
    public class ProductController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }



        [HttpGet]
        public async Task<IActionResult> Index(int categoryid, int subctgid, string applicationid, string appearanceid, string charid, int page = 1, int pageSize = 10)
        {

            List<GetallProduct> allqueryproduct;
            var lang = Request.Cookies["selectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {
                lang = "az";
            }
            else
            {
                lang = lang.ToLower();
            }

            if (categoryid == null && subctgid == null && applicationid == null && appearanceid == null && charid == null)
            {
                var products = await _context.Products
               .Include(p => p.Translates)
               .Include(p => p.Images)
                 .Include(p => p.Category)
                   .Include(p => p.Subcategory)
                     .Include(p => p.Characteristic)
                       .Include(p => p.ApplicationField)
                         .Include(p => p.AppearanceField)
               .Where(p => p.Translates.Any(t => t.LangCode == lang))
               .Select(x => new GetallProduct
               {
                   Id = x.Id,
                   Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                   Size = x.Translates.FirstOrDefault(x => x.LangCode == lang).Size,
                   Image = x.Images.FirstOrDefault(x => x.IsMain == true).Image,
                   CategoryId = (int)x.CategoryId,
                   SubCategoryId = (int)x.SubcategoryId,
                   ApplicationId = x.ApplicationFieldId,
                   AppearanceId = x.AppearanceFieldId,
                   CharacteristicsId = x.CharacteristicId,
                   CharacteristicsName = x.Characteristic.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                   ApplicationName = x.ApplicationField.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                   AppearanceName = x.AppearanceField.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
               })
               .AsNoTracking()
               .ToListAsync();


                var allchars = _context.Characteristics.Include(m => m.Translates).Select(x => new CharVM()
                {
                    TranslateIds = x.Translates.Where(x => x.LangCode == lang).Select(m => m.Id).ToList(),
                    Id = x.Id,
                    CategoryId = x.Category.Id,
                    Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
                }).ToList();
                var allapplication = _context.ApplicationFields.Select(x => new ApplicationVm()
                {
                    Id = x.Id,
                    CategoryId = x.Category.Id,
                    Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
                }).ToList();
                var allapperance = _context.AppearanceFields.Select(x => new ApperanceVm()
                {

                    Id = x.Id,
                    CategoryId = x.Category.Id,
                    Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
                }).ToList();

                var ctg = _context.Categories.Select(x => new GetCategory()
                {
                    Id = x.Id,
                    Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                    Subcategories = x.Subcategories.Select(y => new Getsubcategory()
                    {
                        Name = y.Translates.FirstOrDefault(u => u.LangCode == lang).Name,
                        Id = y.Id,
                        CategoryId = x.Id
                    }).ToList(),
                }).ToList();


                var allsubctg = new GetallCategory
                {


                    Categories = ctg,
                    Apperance = allapperance,
                    Applicat = allapplication,
                    Chars = allchars

                };
                var paginateallTickets = await PaginatedList<GetallProduct>.CreateListAsync(products, page, pageSize);
                GetAllVM productall = new GetAllVM()
                {
                    Categories = allsubctg,
                    Products = paginateallTickets,
                    LangCode = lang,
                    //Applicat = allapplication,
                    //Apperance = allapperance,
                    //Chars = allchars
                };

                return View(productall);
            }


            allqueryproduct = await _context.Products
              .Include(p => p.Translates)
              .Include(p => p.Images)
                .Include(p => p.Category)
                  .Include(p => p.Subcategory)
                    .Include(p => p.Characteristic)
                      .Include(p => p.ApplicationField)
                        .Include(p => p.AppearanceField)
              .Where(p => p.Translates.Any(t => t.LangCode == lang))
              .Select(x => new GetallProduct
              {
                  Id = x.Id,
                  Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                  Size = x.Translates.FirstOrDefault(x => x.LangCode == lang).Size,
                  Image = x.Images.FirstOrDefault(x => x.IsMain == true).Image,
                  CategoryId = (int)x.CategoryId,
                  SubCategoryId = (int)x.SubcategoryId,
                  ApplicationId = x.ApplicationFieldId,
                  AppearanceId = x.AppearanceFieldId,
                  CharacteristicsId = x.CharacteristicId,
                  CharacteristicsName = x.Characteristic.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                  ApplicationName = x.ApplicationField.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                  AppearanceName = x.AppearanceField.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
              })
              .AsNoTracking()
              .ToListAsync();




            var allquerychars = _context.Characteristics.Select(x => new CharVM()
            {
                TranslateIds = x.Translates.Where(x => x.LangCode == lang).Select(m => m.Id).ToList(),
                Id = x.Id,
                CategoryId = x.Category.Id,
                Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
            }).ToList();
            var allqueryapplication = _context.ApplicationFields.Select(x => new ApplicationVm()
            {
                TranslateIds = x.Translates.Where(x => x.LangCode == lang).Select(m => m.Id).ToList(),
                Id = x.Id,
                CategoryId = x.Category.Id,
                Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
            }).ToList();
            var allqueryapperance = _context.AppearanceFields.Select(x => new ApperanceVm()
            {
                TranslateIds = x.Translates.Where(x => x.LangCode == lang).Select(m => m.Id).ToList(),
                Id = x.Id,
                CategoryId = x.Category.Id,
                Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
            }).ToList();

            var queryctg = _context.Categories.Select(x => new GetCategory()
            {
                Id = x.Id,
                Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
                Subcategories = x.Subcategories.Select(y => new Getsubcategory()
                {
                    Name = y.Translates.FirstOrDefault(u => u.LangCode == lang).Name,
                    Id = y.Id,
                    CategoryId = x.Id
                }).ToList(),
            }).ToList();


            if (categoryid != 0)
            {
                allqueryproduct = allqueryproduct.Where(x => x.CategoryId == categoryid).ToList();
                allquerychars = allquerychars.Where(x => x.CategoryId == categoryid).ToList();
                allqueryapperance = allqueryapperance.Where(x => x.CategoryId == categoryid).ToList();
                allqueryapplication = allqueryapplication.Where(x => x.CategoryId == categoryid).ToList();
            }

            if (subctgid != 0)
            {
                allqueryproduct = allqueryproduct.Where(x => x.SubCategoryId == subctgid).ToList();
            }



            if ((appearanceid != null && appearanceid.Length > 0) ||
    (charid != null && charid.Length > 0) ||
    (applicationid != null && applicationid.Length > 0))
            {
                int[] appearanceIds = null;
                int[] charIds = null;
                int[] applicationIds = null;

                if (appearanceid != null && appearanceid.Length > 0)
                {
                    appearanceIds = appearanceid.Split(',')
                        .Select(str => int.TryParse(str, out int id) ? id : 0)
                        .Where(id => id != 0)
                        .ToArray();
                }

                if (charid != null && charid.Length > 0)
                {
                    charIds = charid.Split(',')
                        .Select(str => int.TryParse(str, out int id) ? id : 0)
                        .Where(id => id != 0)
                        .ToArray();
                }

                if (applicationid != null && applicationid.Length > 0)
                {
                    applicationIds = applicationid.Split(',')
                        .Select(str => int.TryParse(str, out int id) ? id : 0)
                        .Where(id => id != 0)
                        .ToArray();
                }

                allqueryproduct = allqueryproduct
                    .Where(a =>
                        (appearanceIds != null && a.AppearanceId.HasValue && appearanceIds.Contains(a.AppearanceId.Value)) ||
                        (charIds != null && a.CharacteristicsId.HasValue && charIds.Contains(a.CharacteristicsId.Value)) ||
                        (applicationIds != null && a.ApplicationId.HasValue && applicationIds.Contains(a.ApplicationId.Value))
                    )
                    .ToList();  // Explicitly convert to IQueryable if necessary
            }




            var allquerysubctg = new GetallCategory
            {


                Categories = queryctg,
                Apperance = allqueryapperance,
                Applicat = allqueryapplication,
                Chars = allquerychars

            };

            var paginateTickets = await PaginatedList<GetallProduct>.CreateListAsync(allqueryproduct, page, pageSize);
            GetAllVM product = new GetAllVM()
            {
                Categories = allquerysubctg,
                Products = paginateTickets,
                LangCode = lang,
                //Applicat = allapplication,
                //Apperance = allapperance,
                //Chars = allchars
            };
            allqueryproduct = null;
            return View(product);


        }

        //public async Task<IActionResult> Index(int categoryid, int subctgid, int applicationid, int appearanceid, int charid, int page=1, int pageSize=10)
        //{

        //    ViewBag.Applicate = applicationid;
        //    ViewBag.Appear = appearanceid;
        //    ViewBag.Charid = charid;
        //    ViewBag.Categoryid = categoryid;
        //    ViewBag.SubCategoryid = subctgid;

        //    var lang = Request.Cookies["selectedLanguage"];
        //    if (string.IsNullOrEmpty(lang))
        //    {
        //        lang = "az";
        //    }
        //    else
        //    {
        //        lang = lang.ToLower();
        //    }

        //    var allproduct = _context.Products.Select(x => new GetallProduct
        //    {
        //        Id = x.Id,
        //        Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
        //        Size = x.Translates.FirstOrDefault(x => x.LangCode == lang).Size,
        //        Image = x.Images.FirstOrDefault(x => x.IsMain == true).Image,
        //        CategoryId = x.CategoryId,
        //        SubCategoryId = x.SubcategoryId,
        //        ApplicationId = x.ApplicationFieldId,
        //        AppearanceId = x.AppearanceFieldId,
        //        CharacteristicsId = x.CharacteristicId,
        //        CharacteristicsName = x.Characteristic.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
        //        ApplicationName = x.ApplicationField.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
        //        AppearanceName = x.AppearanceField.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
        //    }).AsNoTracking()
        //        .AsQueryable();               



        //    var allchars = _context.Characteristics.Select(x => new CharVM()
        //    {
        //        Id = x.Id,
        //        CategoryId = x.Category.Id,
        //        Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
        //    }).ToList();
        //    var allapplication = _context.Characteristics.Select(x => new ApplicationVm()
        //    {
        //        Id = x.Id,
        //        CategoryId = x.Category.Id,
        //        Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
        //    }).ToList();
        //    var allapperance = _context.Characteristics.Select(x => new ApperanceVm()
        //    {

        //        Id = x.Id,
        //        CategoryId = x.Category.Id,
        //        Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name
        //    }).ToList();

        //    if (categoryid != 0)
        //    {
        //        allproduct = allproduct.Where(x => x.CategoryId == categoryid);
        //        allchars = allchars.Where(x => x.CategoryId == categoryid).ToList();
        //        allapperance = allapperance.Where(x => x.CategoryId == categoryid).ToList();
        //        allapplication = allapplication.Where(x => x.CategoryId == categoryid).ToList();
        //    }

        //    if (subctgid != 0)
        //    {
        //        allproduct = allproduct.Where(x => x.SubCategoryId == subctgid);
        //    }
        //    if (appearanceid != 0)
        //    {
        //        allproduct = allproduct.Where(x => x.AppearanceId == appearanceid);
        //    }
        //    if (charid != 0)
        //    {
        //        allproduct = allproduct.Where(x => x.CharacteristicsId == charid);
        //    }
        //    if (applicationid != 0)
        //    {
        //        allproduct = allproduct.Where(x => x.ApplicationId == applicationid);
        //    }
        //    var ctg = _context.Categories.Select(x => new GetCategory()
        //    {
        //        Id = x.Id,
        //        Name = x.Translates.FirstOrDefault(x => x.LangCode == lang).Name,
        //        Subcategories = x.Subcategories.Select(y => new Getsubcategory()
        //        {
        //            Name = y.Translates.FirstOrDefault(u => u.LangCode == lang).Name,
        //            Id = y.Id,
        //            CategoryId = x.Id
        //        }).ToList(),
        //    }).ToList();
        //    var allsubctg = new GetallCategory
        //    {


        //        Categories = ctg,
        //        Apperance = allapperance,
        //        Applicat = allapplication,
        //        Chars = allchars

        //    };

        //    var paginateTickets = await PaginatedList<GetallProduct>.CreateAsync(allproduct, page, pageSize);
        //    GetAllVM product = new GetAllVM()
        //    {
        //        Categories = allsubctg,
        //        Products = paginateTickets,
        //        LangCode= lang,
        //        //Applicat = allapplication,
        //        //Apperance = allapperance,
        //        //Chars = allchars
        //    };

        //    return View(product);
        //}
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return BadRequest();

            var lang = Request.Cookies["selectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {
                lang = "az";
            }
            else
            {
                lang = lang.ToLower();
            }

            var product = await _context.Products.Include(m => m.Category).ThenInclude(m => m.Translates)
                .Include(m => m.Translates.Where(m => m.LangCode == lang))
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            DetailVM detailedProducts = new()
            {
                Product = product,
                Others = await _context.Products.Where(m => m.CategoryId == product.CategoryId).Where(m => m.Id != id).Include(m => m.Translates.Where(m => m.LangCode == lang)).Include(m => m.Images).ToListAsync(),
                LangCode = lang
            };






            return View(detailedProducts);

        }
        public async Task<IActionResult> SearchProduct(string searchTerm, int page = 1, int pageSize = 10)
        {
            searchTerm = searchTerm.ToLower();
            var lang = Request.Cookies["selectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {
                lang = "az";
            }
            else
            {
                lang = lang.ToLower();
            }
            // Linq kullanarak arama işlemini gerçekleştirelim
            var results = _context.Products.Include(m => m.Images).Include(m => m.Translates)
                .Where(p =>
                    p.Brand.ToLower().Contains(searchTerm) ||
                    p.ProductCode.Contains(searchTerm) ||
                    p.Translates.Where(m => m.LangCode == lang).Any(t => t.Name.ToLower().Contains(searchTerm) || t.Desc.ToLower().Contains(searchTerm)))
                .Distinct() // Duplikatları kaldır
                .ToList();

            ProductSearchVM result = new()
            {
                Products = results,
                LangCode = lang
            };
            // Sonuçları bir view'e gönderelim
            return View(result);
        }
    }
}
