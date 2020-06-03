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
                if (roles.Any(i => i.Value == AccessCode.ViewWorkingGroup.ToString()))
                    result.Add(Filter(AccessCode.ViewWorkingGroup));
                if (roles.Any(i => i.Value == AccessCode.CreateWorkingGroup.ToString()))
                    result.Add(Filter(AccessCode.CreateWorkingGroup));
                if (roles.Any(i => i.Value == AccessCode.ViewProject.ToString()))
                    result.Add(Filter(AccessCode.ViewProject));
                if (roles.Any(i => i.Value == AccessCode.CreateProject.ToString()))
                    result.Add(Filter(AccessCode.CreateProject));
                if (roles.Any(i => i.Value == AccessCode.CreateReport.ToString()))
                    result.Add(Filter(AccessCode.CreateReport));
                if (roles.Any(i => i.Value == AccessCode.ViewReport.ToString()))
                    result.Add(Filter(AccessCode.ViewReport));
                if (roles.Any(i => i.Value == AccessCode.PrimaryResponsibble.ToString()))
                    result.Add(Filter(AccessCode.PrimaryResponsibble));
                if (roles.Any(i => i.Value == AccessCode.Interviewer.ToString()))
                    result.Add(Filter(AccessCode.Interviewer));
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
                if (roles.Any(i => i.Value == AccessCode.ProjectTypeManagement.ToString()))
                    result.Add(Filter(AccessCode.ProjectTypeManagement));
                if (roles.Any(i => i.Value == AccessCode.InstagramTagManagement.ToString()))
                    result.Add(Filter(AccessCode.InstagramTagManagement));
                if (roles.Any(i => i.Value == AccessCode.CreatePost.ToString()))
                    result.Add(Filter(AccessCode.CreatePost));
                if (roles.Any(i => i.Value == AccessCode.ViewPost.ToString()))
                    result.Add(Filter(AccessCode.ViewPost));
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

            else if (accessCode == AccessCode.ViewWorkingGroup)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "WorkingGroup",
                    Enum = AccessCode.ViewWorkingGroup,
                    Title = "مشاهده کارگروه های سامانه",
                };
            }
            else if (accessCode == AccessCode.CreateWorkingGroup)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "WorkingGroup",
                    Enum = AccessCode.CreateWorkingGroup,
                    Title = "ایجاد کارگروه جدید",
                };
            }

            else if (accessCode == AccessCode.ViewProject)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "Project",
                    Enum = AccessCode.ViewProject,
                    Title = "مشاهده پروژه های سامانه",
                };
            }
            else if (accessCode == AccessCode.CreateProject)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "Project",
                    Enum = AccessCode.CreateProject,
                    Title = "ایجاد پروژه جدید",
                };
            }

            else if (accessCode == AccessCode.ViewReport)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "Report",
                    Enum = AccessCode.ViewReport,
                    Title = "مشاهده گزارش های سامانه",
                };
            }
            else if (accessCode == AccessCode.CreateReport)
            {
                return new UserAccessModel
                {
                    ActionName = "Create",
                    AreaName = "Admin",
                    ControllerName = "Report",
                    Enum = AccessCode.CreateReport,
                    Title = "ایجاد گزارش جدید",
                };
            }
            else if (accessCode == AccessCode.PrimaryResponsibble)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "Member",
                    Enum = AccessCode.PrimaryResponsibble,
                    Title = "کارتابل مسیول اصلی گزینش",
                };
            }
            else if (accessCode == AccessCode.Interviewer)
            {
                return new UserAccessModel
                {
                    ActionName = "Interviewer",
                    AreaName = "Admin",
                    ControllerName = "Member",
                    Enum = AccessCode.Interviewer,
                    Title = "کارتابل گزینشگر",
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
                    Enum = AccessCode.CreateReport,
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

            else if (accessCode == AccessCode.ProjectTypeManagement)
            {
                return new UserAccessModel
                {
                    ActionName = "Index",
                    AreaName = "Admin",
                    ControllerName = "ProjectType",
                    Enum = AccessCode.ProjectTypeManagement,
                    Title = "مدیریت انواع پروژه",
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
            result.Add(Filter(AccessCode.ViewWorkingGroup));
            result.Add(Filter(AccessCode.CreateWorkingGroup));
            result.Add(Filter(AccessCode.CreateProject));
            result.Add(Filter(AccessCode.ViewProject));
            result.Add(Filter(AccessCode.CreateReport));
            result.Add(Filter(AccessCode.ViewReport));
            result.Add(Filter(AccessCode.PrimaryResponsibble));
            result.Add(Filter(AccessCode.Interviewer));
            result.Add(Filter(AccessCode.CreateSlideShow));
            result.Add(Filter(AccessCode.ViewSlideShow));
            result.Add(Filter(AccessCode.ViewPayment));
            result.Add(Filter(AccessCode.ViewAndManageInfo));
            result.Add(Filter(AccessCode.ViewContactUs));
            result.Add(Filter(AccessCode.ProjectTypeManagement));
            result.Add(Filter(AccessCode.InstagramTagManagement));
            result.Add(Filter(AccessCode.CreatePost));
            result.Add(Filter(AccessCode.ViewPost));

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
                Title = "مدیریت پروژه ها",
                Enum = AccessCode.ProjectManagement,
                Items = new List<UserAccessItemModel>
                 {
                     new UserAccessItemModel
                     {
                          Title="ایجاد پروژه",
                          Id=(int)AccessCode.CreateProject,
                          Enum= AccessCode.CreateProject,
                          Checked=roles.Any(i=>i==AccessCode.CreateProject.ToString())
                     },
                      new UserAccessItemModel
                     {
                          Title="مشاهده پروژه",
                          Id=(int)AccessCode.ViewProject,
                          Enum= AccessCode.ViewProject,
                          Checked=roles.Any(i=>i==AccessCode.ViewProject.ToString())
                     },
                       new UserAccessItemModel
                     {
                          Title="حذف پروژه",
                          Id=(int)AccessCode.DeleteProject,
                          Enum= AccessCode.DeleteProject,
                          Checked=roles.Any(i=>i==AccessCode.DeleteProject.ToString())
                     },
                          new UserAccessItemModel
                     {
                          Title="ویرایش پروژه",
                          Id=(int)AccessCode.EditProject,
                          Enum= AccessCode.EditProject,
                          Checked=roles.Any(i=>i==AccessCode.EditProject.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="الصاق و مشاهده و ویرایش گزارش",
                          Id=(int)AccessCode.AttachReport,
                          Enum= AccessCode.AttachReport,
                          Checked=roles.Any(i=>i==AccessCode.AttachReport.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="مشاهده پرداخت های پروژه",
                          Id=(int)AccessCode.ViewProjectPayment,
                          Enum= AccessCode.ViewProjectPayment,
                          Checked=roles.Any(i=>i==AccessCode.ViewProjectPayment.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="مدیریت انواع پروژه",
                          Id=(int)AccessCode.ProjectTypeManagement,
                          Enum= AccessCode.ProjectTypeManagement,
                          Checked=roles.Any(i=>i==AccessCode.ProjectTypeManagement.ToString())
                     }
                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت کارگروه ها",
                Enum = AccessCode.WorkingGroupManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده کارگروه ها",
                          Id=(int)AccessCode.ViewWorkingGroup,
                          Enum= AccessCode.ViewWorkingGroup,
                          Checked=roles.Any(i=>i==AccessCode.ViewWorkingGroup.ToString())
                     },
                    new UserAccessItemModel
                     {
                          Title="ایجاد کارگروه جدید",
                          Id=(int)AccessCode.CreateWorkingGroup,
                          Enum= AccessCode.CreateWorkingGroup,
                          Checked=roles.Any(i=>i==AccessCode.CreateWorkingGroup.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="ویرایش کارگروه",
                          Id=(int)AccessCode.EditWorkingGroup,
                          Enum= AccessCode.EditWorkingGroup,
                          Checked=roles.Any(i=>i==AccessCode.EditWorkingGroup.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="حذف کارگروه",
                          Id=(int)AccessCode.DeleteWorkingGroup,
                          Enum= AccessCode.DeleteWorkingGroup,
                          Checked=roles.Any(i=>i==AccessCode.DeleteWorkingGroup.ToString())
                     }

                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "مدیریت گزارش ها",
                Enum = AccessCode.ReportManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مشاهده گزارش ها",
                          Id=(int)AccessCode.ViewReport,
                          Enum= AccessCode.ViewReport,
                          Checked=roles.Any(i=>i==AccessCode.ViewReport.ToString())
                     },
                    new UserAccessItemModel
                     {
                          Title="ایجاد گزارش جدید",
                          Id=(int)AccessCode.CreateReport,
                          Enum= AccessCode.CreateReport,
                          Checked=roles.Any(i=>i==AccessCode.CreateReport.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="ویرایش گزارش",
                          Id=(int)AccessCode.EditReport,
                          Enum= AccessCode.EditReport,
                          Checked=roles.Any(i=>i==AccessCode.EditReport.ToString())
                     },
                     new UserAccessItemModel
                     {
                          Title="حذف گزارش",
                          Id=(int)AccessCode.DeleteReport,
                          Enum= AccessCode.DeleteReport,
                          Checked=roles.Any(i=>i==AccessCode.DeleteReport.ToString())
                     },
                      new UserAccessItemModel
                     {
                          Title="مدیریت فایل ها",
                          Id=(int)AccessCode.ReportFileManagement,
                          Enum= AccessCode.ReportFileManagement,
                          Checked=roles.Any(i=>i==AccessCode.ReportFileManagement.ToString())
                     },
                      new UserAccessItemModel
                     {
                          Title="اشتراک گذاری در اینستاگرام",
                          Id=(int)AccessCode.InstagramSharing,
                          Enum= AccessCode.InstagramManagement,
                          Checked=roles.Any(i=>i==AccessCode.InstagramSharing.ToString())
                     }

                 }
            });

            result.Add(new UserAccessGroupingModel
            {
                Title = "گزینش",
                Enum = AccessCode.InterViewManagement,
                Items = new List<UserAccessItemModel>
                 {
                    new UserAccessItemModel
                     {
                          Title="مسیول اصلی گزینش",
                          Id=(int)AccessCode.PrimaryResponsibble,
                          Enum= AccessCode.PrimaryResponsibble,
                          Checked=roles.Any(i=>i==AccessCode.PrimaryResponsibble.ToString())
                     },
                    new UserAccessItemModel
                     {
                          Title="گزینشگر",
                          Id=(int)AccessCode.Interviewer,
                          Enum= AccessCode.Interviewer,
                          Checked=roles.Any(i=>i==AccessCode.Interviewer.ToString())
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
                Enum = AccessCode.ReportManagement,
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
                      new UserAccessItemModel
                     {
                          Title="اشتراک گذاری در اینستاگرام",
                          Id=(int)AccessCode.InstagramSharing,
                          Enum= AccessCode.InstagramManagement,
                          Checked=roles.Any(i=>i==AccessCode.InstagramSharing.ToString())
                     }

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
