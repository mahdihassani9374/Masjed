using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Utility;

namespace Varesin.Mvc.InstaPost
{
    public class InstaPostService
    {
        private readonly string _userName;
        private readonly string _password;
        private IInstaApi _instaApi;
        public InstaPostService(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }
        private async Task<IResult<InstaLoginResult>> LoginAsync()
        {
            var userSession = new UserSessionData
            {
                UserName = _userName,
                Password = _password
            };

            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.All))
                .Build();

            return await _instaApi.LoginAsync();
        }
        public async Task<ServiceResult> UploadAlboumAsync(List<InstaImageUpload> images,
            List<InstaVideoUpload> videos,
            string caption)
        {
            var serviceResult = new ServiceResult(true);
            var loginResult = await LoginAsync();
            if (loginResult.Succeeded)
            {
                if (images.Count == 1 && videos.Count == 0)
                {
                    var uploadPhotoResult = await _instaApi.MediaProcessor
                        .UploadPhotoAsync(images.FirstOrDefault(), caption);

                    if (!uploadPhotoResult.Succeeded)
                        serviceResult.AddError(uploadPhotoResult.Info?.Exception?.Message);
                }
                else if (images.Count == 0 && videos.Count == 1)
                {
                    var uploadVideoResult = await _instaApi.MediaProcessor.UploadVideoAsync(videos.FirstOrDefault(),caption);

                    if(!uploadVideoResult.Succeeded)
                        serviceResult.AddError(uploadVideoResult.Info?.Exception?.Message);
                }
                else
                {

                }
            }
            else serviceResult.AddError("نام کاربری یا رمز عبور اشتباه می باشد");

            return serviceResult;
        }
    }
}
