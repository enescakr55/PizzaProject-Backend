using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CampaignSliderManager : ICampaignSliderService
    {
        ICampaignSliderDal campaignSliderDal;

        public CampaignSliderManager(ICampaignSliderDal campaignSliderDal)
        {
            this.campaignSliderDal = campaignSliderDal;
        }

        public IResult Add(CampaignSlider campaignSlider)
        {
            var validator = new SliderValidation();
            var validationResult = validator.Validate(campaignSlider);
            if (validationResult.IsValid)
            {
                campaignSliderDal.Add(campaignSlider);
                return new SuccessResult("Başarıyla eklendi");
            }
            return new ErrorDataResult<List<ValidationFailure>>(validationResult.Errors);

        }

        public IResult Delete(CampaignSlider campaignSlider)
        {
            campaignSliderDal.Delete(campaignSlider);
            return new SuccessResult("Slider kaldırıldı");
        }

        public IDataResult<List<CampaignSlider>> GetAll()
        {
            return new SuccessDataResult<List<CampaignSlider>>(campaignSliderDal.GetAll());
        }

        public IResult Update(CampaignSlider campaignSlider)
        {
            campaignSliderDal.Update(campaignSlider);
            return new SuccessResult("Başarıyla Güncellendi");
        }
    }
}
