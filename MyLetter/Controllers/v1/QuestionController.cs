using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLetter.EF;
using MyLetter.EF.Models;
using MyLetter.Helpers;
using MyLetter.SignalR;

namespace MyLetter.Controllers.v1
{
    public class QuestionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly MessageHub _messageHub;

        public QuestionController(IUnitOfWork unitOfWork, IMapper mapper,MessageHub messageHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageHub = messageHub;
        }

        [Authorize]
        [HttpGet] //chat/ask {askId
        [Route("question/{questionId}")]

        public Questions getQuestionByQuestionId(string questionId)
        {
            long currentId = User.GetUserId();
            var question = _unitOfWork.Questions.Find(i => i.RecipientId == currentId && i.RecipientDeleted == false && i.Id == questionId);
            return question;
        }

        [Authorize]
        [HttpGet]
        [Route("question/user/{userId}")]

        public IActionResult getAllQuestionsByUserId(int userId)
        {
            var questions = _unitOfWork.Questions.FindAll(i => i.RecipientId == userId);
            return Ok(questions);
        }


        [Authorize]
        [HttpPost]
        [Route("question/answer/{questionId}")]
        public IActionResult AddAnswerQuestionToQuestionId([FromBody] string answer, string questionId)
        {
            int currentId = User.GetUserId();
            var user = _unitOfWork.Users.Find(i => i.Id == currentId);

            var quesion = _unitOfWork.Questions.Find(i => i.Id == questionId && i.RecipientId == currentId);
            quesion.Answer = answer;
            var questions = _unitOfWork.Questions.Update(quesion);


            var notification = _unitOfWork.Notifications.Add(questions, user);
            _unitOfWork.Complete();

            // send notification to user
            _ = _messageHub.SendNotification(notification);
            
            return Ok(user);
        }
    }
}