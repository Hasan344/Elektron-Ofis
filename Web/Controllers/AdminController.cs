using Data.Abstract;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles ="admin")]
    
    public class AdminController:Controller
    {
        
        private IXercTeyinatiDal _xercTeyinatiDal;
        private IXercDal _xercDal;
        public AdminController(IXercTeyinatiDal xercTeyinatiDal,IXercDal xercDal){
            _xercTeyinatiDal=xercTeyinatiDal;
            _xercDal=xercDal;
        }

        //Xerc Teyinati

        [HttpGet]
        public IActionResult XercTeyinatiYarat(){
            return View();
        }
        [HttpPost]
       public IActionResult XercTeyinatiYarat(XercTeyinati model){
           _xercTeyinatiDal.Add(model);
           return RedirectToAction("XercTeyinatiList");
       }
       [HttpGet]
       public IActionResult XercTeyinatiList(){
           List<XercTeyinati> list=_xercTeyinatiDal.GetAll();
           return View(list);

       }
       [HttpGet]
       public IActionResult xercTeyinatiDelete(int id){
         _xercTeyinatiDal.Delete(_xercTeyinatiDal.Get(u=>u.Id==id));
         return RedirectToAction("XercTeyinatiList");
       }
       [HttpGet]
       public IActionResult xercTeyinatiUpdate(int id){
           XercTeyinati entity=_xercTeyinatiDal.Get(u=>u.Id==id);
           return View(entity);
       }
        [HttpPost]
       public IActionResult xercTeyinatiUpdate(XercTeyinati model){
           _xercTeyinatiDal.Update(model);
           return RedirectToAction("XercTeyinatiList");
       }

       // Xerc

       [HttpGet]
       public IActionResult XercYarat(){
          
        return View(new Xerc_XercTeyinatiModel{XercTeyinatlari=_xercTeyinatiDal.GetAll()});
       }
       [HttpPost]
       public IActionResult XercYarat(Xerc_XercTeyinatiModel model){
           _xercDal.Add(model.Xerc);
           return RedirectToAction("XercList");

       }
       [HttpGet]
       public IActionResult XercList(){
           return View(_xercDal.GetXerclerWithXercTeyinati());
       }


    }
}
