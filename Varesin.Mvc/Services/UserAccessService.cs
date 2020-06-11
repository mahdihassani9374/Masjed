using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Models;

namespace Varesin.Mvc.Services
{
    public static class UserAccessService
    {
        public static List<UserAccessModel> GetAccess(IEnumerable<Claim> claims)
        {
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (roles.Any(i => i.Value == AccessCode.FullAccess.ToString()))
                return All();
            else
            {
                var result = new List<UserAccessModel>();
                if (roles.Any(i => i.Value == AccessCode.ViewUser.ToString()))
                    result.Add(Filter(AccessCode.ViewUser));
                if (roles.Any(i => i.Value == AccessCode.CreateUser.ToString()))
                    result.Add(Filter(AccessCode.CreateUser));
                if (roles.Any(i => i.Value == AccessCode.CreateSlideShow.ToString()))
                    result.Add(Filter(AccessCode.CreateSlideShow));
                if (roles.Any(i => i.Value == AccessCode.ViewSlideShow.ToString()))
                    result.Add(Filter(AccessCode.ViewSlideShow));
                if (roles.Any(i => i.Value == AccessCode.ViewPayment.ToString()))
                    result.Add(Filter(AccessCode.ViewPayment));
                if (roles.Any(i => i.Value == AccessCode.ViewAndManageInfo.ToString()))
                    result.Add(Filter(AccessCode.ViewAndManageInfo));
                if (roles.Any(i => i.Value == AccessCode.ViewContactUs.ToString()))
                    result.Add(Filter(AccessCode.ViewContactUs));
                if (roles.Any(i => i.Value == AccessCode.InstagramTagManagement.ToString()))
                    result.Add(Filter(AccessCode.InstagramTagManagement));
                if (roles.Any(i => i.Value == AccessCode.CreatePost.ToString()))
                    result.Add(Filter(AccessCode.CreatePost));
                if (roles.Any(i => i.Value == AccessCode.ViewPost.ToString()))
                    result.Add(Filter(AccessCode.ViewPost));
                if (roles.Any(i => i.Value == AccessCode.CreateEvent.ToString()))
                    result.Add(Filter(AccessCode.CreateEvent));
                if (roles.Any(i => i.Value == AccessCode.ViewEvent.ToString()))
                    result.Add(Filter(AccessCode.ViewEvent));
                if (roles.Any(i => i.Value == AccessCode.CreateNews.ToString()))
                    result.Add(Filter(AccessCode.CreateNews));
                if (roles.Any(i => i.Value == AccessCode.ViewNews.ToString()))
                    result.Add(Filter(AccessCode.ViewNews));
                return result;
            }
        }
        public static string GetFullName(IEnumerable<Claim> claims)
        {
            var nameClaim = claims.Where(c => c.Type == ClaimTypes.GivenName).FirstOrDefault();
            return nameClaim?.Value;
        }
        public static string GetImageName(IEnumerable<Claim> claims)
        {
            var thumbprintClaim = claims.Where(c => c.Type == ClaimTypes.Thumbprint).FirstOrDefault();
            return thumbprintClaim?.Value;

        }
        private static UserAccessModel Filter(AccessCode accessCode)
        {
            if (accessCode == AccessCode.CreateUser)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "UserManagement",
                    Enum = AccessCode.CreateUser,
                    Title = "ایجاد کاربر جدید"
                };
            }
            else if (accessCode == AccessCode.ViewUser)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "UserManagement",
                    Enum = AccessCode.ViewUser,
                    Title = "مشاهده کاربران سامانه",
                };
            }

            else if (accessCode == AccessCode.ViewSlideShow)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "SlideShow",
                    Enum = AccessCode.ViewSlideShow,
                    Title = "مشاهده اسلایدشوهای سامانه",
                };
            }
            else if (accessCode == AccessCode.CreateSlideShow)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "SlideShow",
                    Enum = AccessCode.CreateSlideShow,
                    Title = "ایجاد اسلایدشو جدید",
                };
            }
            else if (accessCode == AccessCode.ViewPayment)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "Payment",
                    Enum = AccessCode.ViewPayment,
                    Title = "مشاهده پرداخت های سامانه",
                };
            }

            else if (accessCode == AccessCode.ViewAndManageInfo)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "Info",
                    Enum = AccessCode.ViewAndManageInfo,
                    Title = "مشاهده و مدیریت اینفوها",
                };
            }
            else if (accessCode == AccessCode.ViewContactUs)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "ContactUs",
                    Enum = AccessCode.ViewContactUs,
                    Title = "مشاهده تماس با ما - انتقادات و پیشنهادات",
                };
            }

            else if (accessCode == AccessCode.InstagramTagManagement)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "InstagramTag",
                    Enum = AccessCode.InstagramTagManagement,
                    Title = "مدیریت تگ های اینستاگرام",
                };
            }
            else if (accessCode == AccessCode.ViewPost)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "Post",
                    Enum = AccessCode.ViewPost,
                    Title = "مشاهده پست های سامانه",
                };
            }
            else if (accessCode == AccessCode.CreatePost)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "Post",
                    Enum = AccessCode.CreatePost,
                    Title = "ایجاد پست جدید",
                };
            }
            else if (accessCode == AccessCode.ViewEvent)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "Event",
                    Enum = AccessCode.ViewEvent,
                    Title = "مشاهده اطلاع رسانی و گزارش گیری سامانه",
                };
            }
            else if (accessCode == AccessCode.CreateEvent)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "Event",
                    Enum = AccessCode.CreateEvent,
                    Title = "ایجاد برنامه جدید",
                };
            }
            else if (accessCode == AccessCode.ViewNews)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "News",
                    Enum = AccessCode.ViewNews,
                    Title = "مشاهده خبرهای سامانه",
                };
            }
            else if (accessCode == AccessCode.CreateNews)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "News",
                    Enum = AccessCode.CreateNews,
                    Title = "ایجاد خبر جدید",
                };
            }
            else
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "UserManagement",
                    Enum = AccessCode.ViewUser,
                    Title = "مشاهده کاربران سامانه",
                };
            }
        }
        private static List<UserAccessModel> All()
        {
            var result = new List<UserAccessModel>();

            result.Add(Filter(AccessCode.CreateUser));
            result.Add(Filter(AccessCode.ViewUser));
            result.Add(Filter(AccessCode.CreateSlideShow));
            result.Add(Filter(AccessCode.ViewSlideShow));
            result.Add(Filter(AccessCode.ViewPayment));
            result.Add(Filter(AccessCode.ViewAndManageInfo));
            result.Add(Filter(AccessCode.ViewContactUs));
            result.Add(Filter(AccessCode.InstagramTagManagement));
            result.Add(Filter(AccessCode.CreatePost));
            result.Add(Filter(AccessCode.ViewPost));
            result.Add(Filter(AccessCode.CreateEvent));
            result.Add(Filter(AccessCode.ViewEvent));
            result.Add(Filter(AccessCode.CreateNews));
            result.Add(Filter(AccessCode.ViewNews));

            return result;
        }
        public static List<UserAccessGroupingModel> GetGroupingAccess(List<string> roles)
        {
            var result = new List<UserAccessGroupingModel>();

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت کاربران",
                Enum = AccessCode.UserManagement,
                Items = new List<UserAccessItemModel>
                 {
                     new UserAccessItemModel
                     {
                          Title="مشاهده کاربران سامانه",
                          Enum= AccessCode.ViewUser,
                          Id=(int)AccessCode.ViewUser,
                          Checked=roles.Any(i=>i==AccessCode.ViewUser.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="ایجاد کاربر جدید",
                          Id=(int)AccessCode.CreateUser,
                          Enum= AccessCode.CreateUser,
                          Checked=roles.Any(i=>i==AccessCode.CreateUser.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="مدیریت دسترسی های کاربر",
                          Id=(int)AccessCode.AccessManagement,
                          Enum= AccessCode.AccessManagement,
                          Checked=roles.Any(i=>i==AccessCode.AccessManagement.ToString())
                     }
                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت اسلایدشو ",
                Enum = AccessCode.SlideShowManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده اسلایدشو",
                          Id=(int)AccessCode.ViewSlideShow,
                          Enum= AccessCode.ViewSlideShow,
                          Checked=roles.Any(i=>i==AccessCode.ViewSlideShow.ToString())
                     },
                    new UserAccessItemModel
                     {
                          Title="ایجاد اسلایدشو جدید",
                          Id=(int)AccessCode.CreateSlideShow,
                          Enum= AccessCode.CreateSlideShow,
                          Checked=roles.Any(i=>i==AccessCode.CreateSlideShow.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="ویرایش اسلایدشو",
                          Id=(int)AccessCode.EditSlideShow,
                          Enum= AccessCode.EditSlideShow,
                          Checked=roles.Any(i=>i==AccessCode.EditSlideShow.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="حذف اسلایدشو",
                          Id=(int)AccessCode.DeleteSlideShow,
                          Enum= AccessCode.DeleteSlideShow,
                          Checked=roles.Any(i=>i==AccessCode.DeleteSlideShow.ToString())
                     }

                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت پرداخت های سامانه ",
                Enum = AccessCode.PaymentManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده پرداخت های سامانه",
                          Id=(int)AccessCode.ViewPayment,
                          Enum= AccessCode.ViewPayment,
                          Checked=roles.Any(i=>i==AccessCode.ViewPayment.ToString())
                     }
                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت اینفوها ",
                Enum = AccessCode.InfoManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده و مدیریت اینفوها",
                          Id=(int)AccessCode.ViewAndManageInfo,
                          Enum= AccessCode.ViewAndManageInfo,
                          Checked=roles.Any(i=>i==AccessCode.ViewAndManageInfo.ToString())
                     }
                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت تماس با ما ",
                Enum = AccessCode.ContatUsManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده تماس با ما - انتقادات و پیشنهادات",
                          Id=(int)AccessCode.ViewContactUs,
                          Enum= AccessCode.ViewContactUs,
                          Checked=roles.Any(i=>i==AccessCode.ViewContactUs.ToString())
                     }
                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "اینستاگرام ",
                Enum = AccessCode.InstagramManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مدیریت تگ های اینستاگرام",
                          Id=(int)AccessCode.InstagramTagManagement,
                          Enum= AccessCode.InstagramTagManagement,
                          Checked=roles.Any(i=>i==AccessCode.InstagramTagManagement.ToString())
                     }
                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت پست ها",
                Enum = AccessCode.PostManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده پست ها",
                          Id=(int)AccessCode.ViewPost,
                          Enum= AccessCode.ViewPost,
                          Checked=roles.Any(i=>i==AccessCode.ViewPost.ToString())
                     },
                    new UserAccessItemModel
                     {
                          Title="ایجاد پست جدید",
                          Id=(int)AccessCode.CreatePost,
                          Enum= AccessCode.CreatePost,
                          Checked=roles.Any(i=>i==AccessCode.CreatePost.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="ویرایش پست",
                          Id=(int)AccessCode.EditPost,
                          Enum= AccessCode.EditPost,
                          Checked=roles.Any(i=>i==AccessCode.EditPost.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="حذف پست",
                          Id=(int)AccessCode.DeletePost,
                          Enum= AccessCode.DeletePost,
                          Checked=roles.Any(i=>i==AccessCode.DeletePost.ToString())
                     },
                      new UserAccessItemModel
                     {
                          Title="مدیریت فایل ها",
                          Id=(int)AccessCode.PostFileManagement,
                          Enum= AccessCode.PostFileManagement,
                          Checked=roles.Any(i=>i==AccessCode.PostFileManagement.ToString())
                     },
                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مشاهده اطلاع رسانی و گزارش گیری سامانه",
                Enum = AccessCode.EventManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده برنامه ها",
                          Id=(int)AccessCode.ViewEvent,
                          Enum= AccessCode.ViewEvent,
                          Checked=roles.Any(i=>i==AccessCode.ViewEvent.ToString())
                     },
                    new UserAccessItemModel
                     {
                          Title="ایجاد برنامه جدید",
                          Id=(int)AccessCode.CreateEvent,
                          Enum= AccessCode.CreateEvent,
                          Checked=roles.Any(i=>i==AccessCode.CreateEvent.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="ویرایش برنامه",
                          Id=(int)AccessCode.EditEvent,
                          Enum= AccessCode.EditEvent,
                          Checked=roles.Any(i=>i==AccessCode.EditEvent.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="حذف برنامه",
                          Id=(int)AccessCode.DeleteEvent,
                          Enum= AccessCode.DeleteEvent,
                          Checked=roles.Any(i=>i==AccessCode.DeleteEvent.ToString())
                     },
                      new UserAccessItemModel
                     {
                          Title="مدیریت فایل ها",
                          Id=(int)AccessCode.EventFileManagement,
                          Enum= AccessCode.EventFileManagement,
                          Checked=roles.Any(i=>i==AccessCode.EventFileManagement.ToString())
                     },
                      

                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت خبر ها",
                Enum = AccessCode.PostManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده خبر ها",
                          Id=(int)AccessCode.ViewNews,
                          Enum= AccessCode.ViewNews,
                          Checked=roles.Any(i=>i==AccessCode.ViewNews.ToString())
                     },
                    new UserAccessItemModel
                     {
                          Title="ایجاد خبر جدید",
                          Id=(int)AccessCode.CreateNews,
                          Enum= AccessCode.CreateNews,
                          Checked=roles.Any(i=>i==AccessCode.CreateNews.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="ویرایش خبر",
                          Id=(int)AccessCode.EditNews,
                          Enum= AccessCode.EditNews,
                          Checked=roles.Any(i=>i==AccessCode.EditNews.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="حذف خبر",
                          Id=(int)AccessCode.DeleteNews,
                          Enum= AccessCode.DeleteNews,
                          Checked=roles.Any(i=>i==AccessCode.DeleteNews.ToString())
                     },
                      new UserAccessItemModel
                     {
                          Title="مدیریت فایل ها",
                          Id=(int)AccessCode.NewsFileManagement,
                          Enum= AccessCode.NewsFileManagement,
                          Checked=roles.Any(i=>i==AccessCode.NewsFileManagement.ToString())
                     },
                 }
            });

            return result;
        }
        public static bool UserIsInRole(ClaimsPrincipal user, AccessCode accessCode)
        {
            if (!user.IsInRole(AccessCode.FullAccess.ToString()))
            {
                return user.IsInRole(accessCode.ToString());
            }
            return true;
        }
    }
}
