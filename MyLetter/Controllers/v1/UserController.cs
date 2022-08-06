using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLetter.Dto;
using MyLetter.EF;
using MyLetter.EF.Models;
using MyLetter.Helpers;

namespace MyLetter.Controllers.v1
{
    public class UserController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult profile()
        {
            int currentId = User.GetUserId();
            var user = _unitOfWork.Users.GetById(currentId);
            ProfileDto profile = _mapper.Map<ProfileDto>(user);
           
            return Ok(profile);
        }

        [Authorize]
        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult Index([FromBody] int userId)
        {
            var user = _unitOfWork.Users.GetByIdAsync(userId);
            if (user.Result == null)
                return null;

            ProfileDto profile = _mapper.Map<ProfileDto>(user.Result);
            return Ok(profile);
        }




        [Authorize]
        [HttpPost]
        [Route("user/delete")]
        public IActionResult DeleteUser()
        {
            int currentId = User.GetUserId();
            AppUser user = _unitOfWork.Users.GetById(currentId);
            _unitOfWork.Users.Delete(user);
            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("user/edit")]
        public IActionResult EditUserProfile(EditDto editDto)
        {
            int currentId = User.GetUserId();
            AppUser appUser = _unitOfWork.Users.GetById(currentId);
            AppUser user = _unitOfWork.Users.EditProfileData(appUser, editDto.Name, editDto.Value);
            _ = _unitOfWork.Users.Update(user);
            ProfileDto profileDto = _mapper.Map<ProfileDto>(user);

            _unitOfWork.Complete();

            return Ok(profileDto);
        }


        [HttpGet("user/filter")]
        public FilterDto GetFilter()
        {


            var educations = _unitOfWork.Educations.GetAll();
            var sectors = _unitOfWork.Sectors.GetAll();
            FilterDto filter =  _mapper.Map<FilterDto>(( educations, sectors));
            return filter;

        }


    }
}
