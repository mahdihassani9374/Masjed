using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models;
using Varesin.Mvc.Models.Pagination;
using Varesin.Mvc.Models.Post;
using Varesin.Services;

namespace Varesin.Mvc.Controllers
{
    public class PostController : BaseController
    {
        private readonly UserService _userService;
        public PostController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(PostUserSearchViewModel searchModel)
        {
            var data = _userService.GetPosts(searchModel.ToDto());

            List<SelectListItem> pageSizeSelector = new List<SelectListItem>();
            pageSizeSelector.Add(new SelectListItem("12", "12", searchModel.PageSize == 12));
            pageSizeSelector.Add(new SelectListItem("24", "24", searchModel.PageSize == 24));
            pageSizeSelector.Add(new SelectListItem("36", "36", searchModel.PageSize == 36));
            pageSizeSelector.Add(new SelectListItem("48", "48", searchModel.PageSize == 48));
            pageSizeSelector.Add(new SelectListItem("60", "60", searchModel.PageSize == 60));

            ViewBag.PageSizeSelector = pageSizeSelector;

            return View(new SearchModel<PostUserSearchViewModel, PaginationViewModel<PostViewModel>>(searchModel, data.ToVewModel()));
        }
    }
}