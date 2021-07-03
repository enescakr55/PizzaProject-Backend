using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class SliderValidation:AbstractValidator<CampaignSlider>
    {
        public SliderValidation()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage("Açıklama boş olamaz");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Resim Adresi boş olamaz");
            RuleFor(p => p.PizzaId).NotEmpty().WithMessage("Lütfen bir pizza seçin");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Slider başlığı boş olamaz");
        }
    }
}
