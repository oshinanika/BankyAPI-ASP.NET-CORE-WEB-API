 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BankyWeb.Models;
using BankyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BankyWeb.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankRepository _bankRepo;

        public BankController(IBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }
        public IActionResult Index()
        {
            return View(new Bank() { });
        }


        //Upsert=Update/Insert choosing, if Insert, id==null; if Update, id!=null
        public async Task<IActionResult> Upsert(int? id) 
        {
            Bank obj = new Bank();

            if(id == null)
            { 
                //This is for Create/Insert
                return View(obj);
            }
             
            //This is for update 
            obj = await _bankRepo.GetAsync(SD.BankAPIPath, id.GetValueOrDefault());
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);  //------Sending the id info to view
        }


        //Upsert=Update/Insert actual work
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Bank obj)
        {
            if (ModelState.IsValid)
            {
                //--------Image File handling------
                var files = HttpContext.Request.Form.Files;
                if(files.Count > 0)
                {
                    byte[] image1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream()) { 
                            
                            fs1.CopyTo(ms1);
                            image1 = ms1.ToArray(); 
                        }
                    }
                    obj.IdentificationImage = image1;
                }
                else
                {
                    var objFromDb = await _bankRepo.GetAsync(SD.BankAPIPath, obj.Id);
                    obj.IdentificationImage = objFromDb.IdentificationImage;
                }

                if(obj.Id == 0)
                {
                    await _bankRepo.CreateAsync(SD.BankAPIPath, obj);
                }
                else
                {
                    await _bankRepo.UpdateAsync(SD.BankAPIPath + obj.Id, obj);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }


        public async Task<IActionResult> GetAllAccounts()
        {
            return Json(new { data = await _bankRepo.GetAllAsync(SD.BankAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _bankRepo.DeleteAsync(SD.BankAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = " Delete Successful" });
            }
            return Json(new { success = false, message = " Delete UnSuccessful" });
        }
    }
}