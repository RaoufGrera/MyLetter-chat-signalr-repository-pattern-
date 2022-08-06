namespace MyLetter.Dto
{
    internal class FacebookUserData
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string Gender { get; set; }
        public string Locale { get; set; }
        public FacebookPictureData Picture { get; set; }
    }

    internal class FacebookPictureData
    {
        public FacebookPicture Data { get; set; }
    }

    internal class FacebookPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public bool is_silhouette { get; set; }
        public string Url { get; set; }
    }

    internal class FacebookUserAccessTokenData
    {
        public long app_id { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }
        public long expires_at { get; set; }
        public bool is_valid { get; set; }
        public long user_id { get; set; }
    }

    internal class FacebookUserAccessTokenValidation
    {
        public FacebookUserAccessTokenData Data { get; set; }
    }

    internal class FacebookAppAccessToken
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }


    public class FacebookAuthSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
}
