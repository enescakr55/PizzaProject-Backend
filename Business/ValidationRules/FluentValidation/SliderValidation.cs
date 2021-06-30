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
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.ImageUrl).NotEmpty();
            RuleFor(p => p.PizzaId).NotEmpty();
            RuleFor(p => p.Title).NotEmpty();
        }
    }
}
