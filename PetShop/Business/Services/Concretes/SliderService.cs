using Business.Exceptions;
using Business.Extension;
using Business.Services.Abstract;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class SliderService : ISliderServices
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IWebHostEnvironment _env;
        public SliderService(ISliderRepository sliderRepository, IWebHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
            _env = env;
        }

        public async Task AddSlider(Slider slider)
        {
            if (slider.ImageFile == null)
                throw new FileNullReference("File bos ola bilmez");

            slider.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\sliders", slider.ImageFile);


            await _sliderRepository.AddAsync(slider);
            await _sliderRepository.CommitAsync();

        }

        public void DeleteSlider(int id)
        {
            var existSlider = _sliderRepository.Get(x => x.Id == id);
            if (existSlider == null)
                throw new SliderNotFound("Slider tapilmadi");

            Helper.DeleteFile(_env.WebRootPath, @"uploads\sliders", existSlider.ImageUrl);

            _sliderRepository.Delete(existSlider);
            _sliderRepository.Commit();
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.Get(func);
        }

        public List<Slider> GetSliderList(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.GetAll(func);
        }

        public void UpdateSlider(int id, Slider newSlider)
        {
            var oldSlider = _sliderRepository.Get(x => x.Id == id);

            if (oldSlider == null)
                throw new SliderNotFound("Slider Tapilmadi");

            if (newSlider.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\sliders", oldSlider.ImageUrl);

                oldSlider.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\sliders", newSlider.ImageFile);

            }
            oldSlider.FullName = newSlider.FullName;
            oldSlider.Designation = newSlider.Designation;

            _sliderRepository.Commit();
        }
    }
}
