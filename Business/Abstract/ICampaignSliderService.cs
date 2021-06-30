using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICampaignSliderService
    {
        IDataResult<List<CampaignSlider>> GetAll();
        IResult Add(CampaignSlider campaignSlider);
        IResult Delete(CampaignSlider campaignSlider);
        IResult Update(CampaignSlider campaignSlider);
   
    }
}
