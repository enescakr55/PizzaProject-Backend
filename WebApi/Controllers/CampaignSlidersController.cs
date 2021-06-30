using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/campaignsliders")]
    [ApiController]
    public class CampaignSlidersController : ControllerBase
    {
        ICampaignSliderService campaignSliderService;
        IAuthService authService;

        public CampaignSlidersController(ICampaignSliderService campaignSliderService, IAuthService authService)
        {
            this.campaignSliderService = campaignSliderService;
            this.authService = authService;
        }
        [HttpPost("addslider")]
        public IActionResult addSlider(CampaignSlider campaignSlider)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                var result = campaignSliderService.Add(campaignSlider);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
                
            }
            return BadRequest(authService.IsStaff());
        }
        [HttpPost("deleteslider")]
        public IActionResult deleteSlider(CampaignSlider campaignSlider)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(campaignSliderService.Delete(campaignSlider));
            }
            return BadRequest(authService.IsStaff());
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(campaignSliderService.GetAll());
        }
        [HttpPost("updateslider")]
        public IActionResult Update(CampaignSlider campaignSlider)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(campaignSliderService.Update(campaignSlider));
            }
            return BadRequest(authService.IsStaff());
        }
    }
}
