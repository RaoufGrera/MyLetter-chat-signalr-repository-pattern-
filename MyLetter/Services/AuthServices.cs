

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyLetter.EF.Models;
using MyLetter.Dto;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyLetter.EF;

namespace MyLetter.Services
{
    public interface IAuthService
    {
        Task<RegisterDto> LoginByFacebook(string AccessToken);
        Task<RegisterDto> LoginByGmail(string AccessToken);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
 
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly FacebookAuthSettings _fbAuthSettings;
        private static readonly HttpClient Client = new HttpClient();
        private readonly AppSettings _appSettings;
        public AuthService(IOptions<AppSettings> appSettings, IOptions<JwtIssuerOptions> jwtOptions, IOptions<FacebookAuthSettings> fbAuthSettingsAccessor)
        {
            _appSettings = appSettings.Value;
            _jwtOptions = jwtOptions.Value;
            _fbAuthSettings = fbAuthSettingsAccessor.Value;
        }



        public async Task<RegisterDto> LoginByFacebook(string AccessToken)
        {

            // 1.generate an app access token
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={AccessToken}&access_token={appAccessToken?.access_token}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.is_valid)
                return null;
          
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v12.0/me?fields=id,first_name&access_token={AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            var faceId = "f" + userInfo.Id.ToString();

            var user = await _unitOfWork.Users.GetUserByName(faceId);    

            if (user == null)
            {
                var appUser = new AppUser
                {
                    Name = userInfo.first_name,
                    Password = faceId,
                    FacebookId = userInfo.Id,
                    Email = faceId,
                    UserName = faceId,
                };

                var result = await _unitOfWork.Users.CreateUserAsync(appUser); 
                if (!result.Succeeded) return null;
            }

            var objUser = await _unitOfWork.Users.GetUserByName(faceId);

            return await CreateToken(objUser);
        }

        public async Task<RegisterDto> LoginByGmail(string AccessToken)
        {

            GoogleUserOutputData serStatus = null;
            try
            {
                HttpClient client = new HttpClient();
                client.CancelPendingRequests();
                HttpResponseMessage output = await Client.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={AccessToken}");

                if (output.IsSuccessStatusCode)
                {
                    string outputData = await output.Content.ReadAsStringAsync();
                    serStatus = JsonConvert.DeserializeObject<GoogleUserOutputData>(outputData);

                    if (serStatus == null)
                         return null;
                  
                }
            }
            catch (Exception ex)
            {
                //
            }

            var user = await _unitOfWork.Users.FindByEmailAsync(serStatus.email);

            if (user == null)
            {

                var appUser = new AppUser
                {
                    Name = serStatus.name,
                    GmailId = serStatus.id,
                    Email = serStatus.email,
                    UserName = serStatus.id,
                };

                var result = await _unitOfWork.Users.CreateUserAsync(appUser);
                if (!result.Succeeded) return null;


            }
            var objUser = await _unitOfWork.Users.FindByEmailAsync(serStatus.email);

            return await CreateToken(objUser); 

        }
        public async Task<RegisterDto> CreateToken(AppUser user)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));

       

            var roles = await _unitOfWork.Users.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var creds = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.ToUniversalTime().AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var writeToken = tokenHandler.WriteToken(token);

            int likes = _unitOfWork.Notifications.GetCount(p => p.AppUserId == user.Id && !p.IsRead && !p.Name.Contains("message"));
            int messages = _unitOfWork.Notifications.GetCount(p => p.AppUserId == user.Id && !p.IsRead && p.Name.Contains("message"));
            return new RegisterDto
            {
                Id = user.Id,
                UserId = user.Id,
                Likes = likes,
                Messages = messages,
                Name = user.Name,
                UserName = user.UserName,
                Token = writeToken,
                Country = user.CountryId,
                Gender = user.GenderId,
                Image = user.Image

            };

        }
        //public UserDto GenerateJwt(AppUser objUser)
        //{
        //    if (objUser == null)
        //        return null;


        //    var claims = new[]{
        //            new Claim(ClaimTypes.Name,objUser.UserName),
        //            new Claim(ClaimTypes.Email,objUser.Email),
        //            new Claim(ClaimTypes.Sid,objUser.Id.ToString()),
        //            new Claim("Gender",objUser.GenderId.ToString()),
        //            new Claim("UserId",objUser.Id.ToString()),
        //        };

        //    SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
        //    var token = new JwtSecurityToken(
        //      issuer: _jwtOptions.Issuer,
        //      audience: _jwtOptions.Audience,
        //      claims: claims,
        //      expires: DateTime.Now.ToUniversalTime().AddDays(200),
        //      signingCredentials: new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256));

        //    objUser.Token = new JwtSecurityTokenHandler().WriteToken(token);


        //    _context.Entry(objUser).Property("Token").IsModified = true;
        //    _context.AppUser.Update(objUser);
        //    return new UserDto
        //    {
        //        Message = "تم تسجيل الدخول بنجاح",
        //        UserId = objUser.Id,
        //        Name = objUser.Name,
        //        UserName = objUser.UserName,
        //        Token = objUser.Token,
        //        Gender = objUser.GenderId,
        //        Image = objUser.Image

        //    };
        //}

    }
}
