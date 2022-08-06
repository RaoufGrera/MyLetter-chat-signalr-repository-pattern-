using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLetter.Dto;
using MyLetter.EF;
using MyLetter.EF.Models;
using MyLetter.Helpers;
using MyLetter.SignalR;
using System;
using System.Collections.Generic;

namespace MyLetter.Controllers.v1
{
    public class ChatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private MessageHub _messageHub;
        public ChatController(IUnitOfWork unitOfWork,IMapper mapper, MessageHub messageHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageHub = messageHub;
        }

        [Authorize]
        [HttpGet] //chat/{userId}/{messageId
        [Route("chat/{messageId}")]

        public MessageDto getMessageByMessageId(string messageId)
        {
            int currentId = User.GetUserId();
            var result = _unitOfWork.Messages.Find(i => i.Id == messageId && (i.RecipientId == currentId || i.SenderId == currentId));
            MessageDto message = _mapper.Map<MessageDto>(result);
            return message;
        }


        [Authorize]
        [HttpGet]
        [Route("chat/{userId}")]
        public IEnumerable<MessageDto> getAllMessagesById(int userId)
        {
            var currentId = User.GetUserId();

            _unitOfWork.Notifications.UpdateReadNotificationsMessageByUserId(currentId, userId);
            _unitOfWork.Complete();

            var messages = _unitOfWork.Messages.FindAll(m => (m.RecipientId == currentId && m.RecipientDeleted == false && m.SenderId == userId) 
            || (m.RecipientId == userId  && m.SenderId == currentId && m.SenderDeleted == false));

            IEnumerable<MessageDto> messageDto = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);

            return messageDto;

        }


        [Authorize]
        [HttpPost]
        [Route("chat/{userId}")]
        public IActionResult AddMessageToUserId([FromBody] MessageDto messageDto, int userId)
        {
            long currentId = User.GetUserId();

            var currentUser = _unitOfWork.Users.Find(o => o.Id == currentId);
            var toUser = _unitOfWork.Users.Find(o => o.Id == userId);
         

            GroupMessages groupMessage = _unitOfWork.GroupMessages.Find(o => (o.SenderId == currentId && o.RecipientId == toUser.Id) ||
            (o.RecipientId == currentId && o.SenderId == toUser.Id));
           
            groupMessage.LastUpdated = DateTime.UtcNow.AddHours(3);
            if (groupMessage == null)
            {
                groupMessage.SenderId = currentId;
                groupMessage.RecipientId = toUser.Id;
                _unitOfWork.GroupMessages.Add(groupMessage);
            }
            else
            {
                _unitOfWork.GroupMessages.Update(groupMessage);
            }

            Message message = _mapper.Map<Message>(messageDto);

            _ = _unitOfWork.Messages.Add(message);

            Notification notification = _unitOfWork.Notifications.Add(currentUser , toUser);
            _ = _messageHub.SendNotification(notification);

            _unitOfWork.Complete();


      
            return Ok();

        }


    }
}
