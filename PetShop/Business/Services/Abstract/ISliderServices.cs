using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ISliderServices
    {
        Task AddSlider(Slider slider);
        void DeleteSlider(int id);

        void UpdateSlider(int id, Slider newSlider);

        Slider GetSlider(Func<Slider,bool>? func=null);

        List<Slider> GetSliderList(Func<Slider, bool>? func = null);
    }
}
