using Business.Exceptions;
using Business.Services.Abstract;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace PetShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderServices _sliderServices;

        public SliderController(ISliderServices sliderServices)
        {
            _sliderServices = sliderServices;
        }

        public IActionResult Index()
        {
            var sliders = _sliderServices.GetSliderList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult>  Create(Slider slider)
        {
            if(!ModelState.IsValid) 
                return View();

            try
            {
                await _sliderServices.AddSlider(slider);
            }
            catch (ImageContentType ex)
            {
                ModelState.AddModelError("ImageFile",ex.Message);   
            }
            catch (ImageSize ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (FileNullReference ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete( int id)
        {
          var exsitSlider = _sliderServices.GetSlider(x=>x.Id == id);
            if (exsitSlider == null) return NotFound();
            return View(exsitSlider);
        }
        [HttpPost]

        public IActionResult DeletePost(int id)
        {
            try
            {
                _sliderServices.DeleteSlider(id);
            }
            catch (SliderNotFound ex)
            {
                return NotFound();
               
            }
            catch (Business.Exceptions.FileNotFound ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var existSlider = _sliderServices.GetSlider(x=>x.Id == id);
            if (existSlider == null) return NotFound();
            return View(existSlider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                 _sliderServices.UpdateSlider(slider.Id,slider);
            }
            catch (ImageContentType ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (ImageSize ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (FileNullReference ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch(SliderNotFound ex)
            {
                ModelState.AddModelError("ImageFile",ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
