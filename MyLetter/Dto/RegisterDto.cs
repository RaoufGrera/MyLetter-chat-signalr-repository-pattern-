namespace MyLetter.Dto
{
    public class RegisterDto
    {
        public long UserId;
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsReceived { get; set; }

        public int Likes { get; set; }
        public int Messages { get; set; }
        public string Country { get; set; }

        public string Gender { get; set; }

        public string UserName { get; set; }

        public string Image { get; set; }

        public string Message { get; set; }

        public DateTime MessageSent { get; set; }

        public string Token { get; set; }
    }
}
