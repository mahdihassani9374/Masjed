using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.News;
using Varesin.Mvc.Models.Pagination;
using Varesin.Services;

namespace Varesin.Mvc.Controllers
{
    public class NewsController : BaseController
    {
        private readonly UserService _userService;
        public NewsController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(NewsUserSearchViewModel searchModel)
        {
            var data = _userService.GetNews(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("12", "12", searchModel.PageSize == 12));
            pageSizeSelector.Add(new SelectListItem("24", "24", searchModel.PageSize == 24));
            pageSizeSelector.Add(new SelectListItem("36", "36", searchModel.PageSize == 36));
            pageSizeSelector.Add(new SelectListItem("48", "48", searchModel.PageSize == 48));
            pageSizeSelector.Add(new SelectListItem("60", "60", searchModel.PageSize == 60));

            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("نوع خبر", ""));
            typeSelector.Add(new SelectListItem("اخبار محل",NewsType.Mahal.ToString(),searchModel.Type== NewsType.Mahal));
            typeSelector.Add(new SelectListItem("اقتصادی", NewsType.Eghtesadi.ToString(), searchModel.Type == NewsType.Eghtesadi));
            typeSelector.Add(new SelectListItem("فرهنگی", NewsType.Farhangi.ToString(), searchModel.Type == NewsType.Farhangi));
            typeSelector.Add(new SelectListItem("سیاسی", NewsType.Siasi.ToString(), searchModel.Type == NewsType.Siasi));

            ViewBag.PageSizeSelector = pageSizeSelector;
            ViewBag.TypeSelector = typeSelector;

            return View(new SearchModel<NewsUserSearchViewModel, PaginationViewModel<NewsViewModel>>(searchModel, data.ToVewModel()));
        }
    }
}