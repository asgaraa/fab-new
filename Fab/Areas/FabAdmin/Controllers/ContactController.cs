using Fab.Data;
using Fab.Models.AdsFolder;
using Fab.Models.ContactFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.AdsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ContactController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index1()
        {
            var contactInfo = await _context.ContactInformations.ToListAsync();

            return View(contactInfo);
        }




        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ContactInformations informations)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ContactInformations newInformations = new()
                {
                    LocationLink = informations.LocationLink,
                    Tel1 = informations.Tel1,
                    Tel2 = informations.Tel2,
                    Tel3 = informations.Tel3,
                    //Fax1 = informations.Fax1,
                    SocialLink1 = informations.SocialLink1,
                    SocialLink2 = informations.SocialLink2,
                    SocialLink3 = informations.SocialLink3,
                    SocialLink4 = informations.SocialLink4,
                    Email = informations.Email,
                    Location = informations.Location,

                };

                await _context.ContactInformations.AddAsync(newInformations);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index1");

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }


        public async Task<IActionResult> Edit(int Id)
        {

            var contact = await _context.ContactInformations.FirstOrDefaultAsync(x => x.Id == Id);
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, ContactInformations information)
        {
            var dbInformation = await _context.ContactInformations.FirstOrDefaultAsync(m => m.Id == Id);
            if (dbInformation == null)
            {
                return View();
            }
            

            dbInformation.LocationLink=information.LocationLink;
            dbInformation.Tel1=information.Tel1;
            dbInformation.Tel2=information.Tel2;
            dbInformation.Tel3=information.Tel3;
            //dbInformation.Fax1 = information.Fax1;
            dbInformation.SocialLink1 = information.SocialLink1;
            dbInformation.SocialLink2 = information.SocialLink2;
            dbInformation.SocialLink3 = information.SocialLink3;
            dbInformation.SocialLink4 = information.SocialLink4;
            dbInformation.Email = information.Email;
            dbInformation.Location = information.Location;



            await _context.SaveChangesAsync();
            return RedirectToAction("Index1", "Contact");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            var dbInformation = await _context.ContactInformations
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbInformation == null) return NotFound();

            _context.ContactInformations.Remove(dbInformation);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index1");
        }

    }
}
