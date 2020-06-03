using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Varesin.Domain.DTO.Report;
using Varesin.Domain.Entities;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.Instagram;
using Varesin.Services;
using Varesin.Utility;

namespace Varesin.Mvc.Areas.Admin.Controllers
{
    public class InstagramController : BaseController
    {
        private readonly AdminService _adminService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private List<string> deletedFiles = new List<string>();
        public InstagramController(AdminService adminService,
            IHostingEnvironment hostingEnvironment)
        {
            _adminService = adminService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreateInstaPostViewModel model)
        {
            var servieResult = new ServiceResult(true);

            #region validation

            if (string.IsNullOrEmpty(model.Caption))
                servieResult.AddError("توضیحات پست را وارد نکرده اید");

            if (model.FileIds == null || model.FileIds.Count == 0)
                servieResult.AddError("فایلی را انتخاب نکرده اید");

            #endregion

            if (servieResult.IsSuccess)
            {
                var selectedFiles = _adminService.GetAllReportFiles(model.FileIds);

                var info = _adminService.GetAllInfoes().ToViewModel();

                var userSession = new UserSessionData
                {
                    UserName = info.InstagramUserName,
                    Password = info.InstagramPassword
                };

                var instaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .UseLogger(new DebugLogger(LogLevel.All))
                    .Build();

                var loginResult = await instaApi.LoginAsync();

                if (loginResult.Succeeded)
                {
                    if (selectedFiles.Count == 1)
                    {
                        var file = selectedFiles.FirstOrDefault();

                        if (file.Type == FileType.Image)
                        {
                            var image = CreateImageInstance(file);
                            if (model.Tags != null && model.Tags.Count > 0)
                                image.UserTags = CreateUserTagImageInstance(model.Tags);
                            var imageUploadResult = await instaApi.MediaProcessor.UploadPhotoAsync(image, model.Caption);
                            if (!imageUploadResult.Succeeded)
                                servieResult.AddError(imageUploadResult.Info.Exception.Message);
                            else
                            {
                                if (model.DisableComment)
                                    await instaApi.CommentProcessor.DisableMediaCommentAsync(imageUploadResult.Value.Pk);
                            }
                        }
                        else
                        {
                            var video = CreateVideoInstance(file);
                            if (model.Tags != null && model.Tags.Count > 0)
                                video.UserTags = CreateUserTagVideoInstance(model.Tags);
                            var videoUploadResult = await instaApi.MediaProcessor.UploadVideoAsync(video, model.Caption);

                            if (!videoUploadResult.Succeeded)
                                servieResult.AddError(videoUploadResult.Info.Exception.Message);
                            else
                            {
                                if (model.DisableComment)
                                    await instaApi.CommentProcessor.DisableMediaCommentAsync(videoUploadResult.Value.Pk);
                            }
                        }
                    }
                    else
                    {
                        var images = new List<InstaImageUpload>();
                        var videos = new List<InstaVideoUpload>();

                        foreach (var file in selectedFiles)
                        {
                            if (file.Type == FileType.Image)
                            {
                                var image = CreateImageInstance(file);

                                images.Add(image);
                            }
                            else
                            {
                                var video = CreateVideoInstance(file);

                                videos.Add(video);
                            }
                        }

                        if (model.Tags != null && model.Tags.Count > 0)
                        {
                            if (images.Count > 0)
                                images.FirstOrDefault().UserTags = CreateUserTagImageInstance(model.Tags);
                            else
                            {
                                if (videos.Count > 0)
                                    videos.FirstOrDefault().UserTags = CreateUserTagVideoInstance(model.Tags);
                            }
                        }

                        var uploadAlbumResult = await instaApi.MediaProcessor
                            .UploadAlbumAsync(images.ToArray(), videos.ToArray(), model.Caption);

                        if (!uploadAlbumResult.Succeeded)
                            servieResult.AddError(uploadAlbumResult.Info.Exception.Message);
                        else
                        {
                            if (model.DisableComment)
                                await instaApi.CommentProcessor.DisableMediaCommentAsync(uploadAlbumResult.Value.Pk);
                        }

                    }
                }

                else servieResult.AddError("نام کاربری یا رمز عبور اینستاگرام اشتباه می باشد");

                if (deletedFiles.Count > 0)
                {
                    foreach (var path in deletedFiles)
                    {
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    }
                }
            }

            return Json(servieResult);
        }

        private string ConvertToJPEG(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            try
            {
                var newPath = Path.Combine(_hostingEnvironment.WebRootPath, Guid.NewGuid().ToString()) + ".jpg";
                var img = Image.FromFile(path);
                using (var b = new Bitmap(img.Width, img.Height))
                {
                    b.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                    using (var g = Graphics.FromImage(b))
                    {
                        g.Clear(Color.White);
                        g.DrawImageUnscaled(img, 0, 0);
                    }
                    System.Threading.Thread.Sleep(100);
                    b.Save(newPath, ImageFormat.Jpeg);
                    return newPath;
                }
            }
            catch (Exception ex)
            {

            }
            return path;
        }
        private InstaImageUpload CreateImageInstance(ReportFileDto file)
        {
            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "Files", "ReportFile", file.FileName);

            var newImagePath = ConvertToJPEG(imagePath);

            deletedFiles.Add(newImagePath);

            return new InstaImageUpload(newImagePath, 0, 0);
        }

        private InstaVideoUpload CreateVideoInstance(ReportFileDto file)
        {
            var videoPath = Path.Combine(_hostingEnvironment.WebRootPath, "Files", "ReportFile", file.FileName);

            var cropVideo = Path.Combine(_hostingEnvironment.WebRootPath, Guid.NewGuid().ToString()) + ".mp4";

            deletedFiles.Add(cropVideo);

            new FFmpegFa.FFmpeg().CropVideoOrAudio(videoPath, cropVideo, TimeSpan.FromSeconds(0), TimeSpan.FromMinutes(1));

            var screenshot = Path.Combine(_hostingEnvironment.WebRootPath, Guid.NewGuid().ToString()) + ".jpg";

            new FFmpegFa.FFmpeg().ExtractImageFromVideo(videoPath, screenshot);

            deletedFiles.Add(screenshot);

            return new InstaVideoUpload
            {
                Video = new InstaVideo(cropVideo, 0, 0),
                VideoThumbnail = new InstaImage(screenshot, 0, 0)
            };
        }

        private List<InstaUserTagUpload> CreateUserTagImageInstance(List<string> tags)
        {
            var array = new List<double>() { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
            int index = 3;

            var result = new List<InstaUserTagUpload>();

            for (int i = 0; i < tags.Count; i++)
            {
                if (index == 9) index = 0;

                result.Add(new InstaUserTagUpload
                {
                    X = array[index],
                    Y = array[index],
                    Username = tags[i]
                });
                index++;
            }

            return result;
        }

        private List<InstaUserTagVideoUpload> CreateUserTagVideoInstance(List<string> tags)
        {
            var result = new List<InstaUserTagVideoUpload>();

            for (int i = 0; i < tags.Count; i++)
            {
                result.Add(new InstaUserTagVideoUpload
                {
                    Username = tags[i]
                });
            }

            return result;
        }
    }
}