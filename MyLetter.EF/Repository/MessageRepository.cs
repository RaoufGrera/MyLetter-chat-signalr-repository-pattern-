using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetter.EF.Repository
{
    public class MessageRepository : BaseRepository<Message>,  IMessageRepository 
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        

    }
}
