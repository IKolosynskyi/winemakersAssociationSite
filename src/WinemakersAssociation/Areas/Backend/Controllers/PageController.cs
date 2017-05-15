using Microsoft.AspNetCore.Mvc;
using WinemakersAssociation.Areas.Backend.Models.HomeModels;
using WinemakersAssociation.Business.Repositories.Interfaces;
using WinemakersAssociation.Data.Enums;

namespace WinemakersAssociation.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public IActionResult List()
        {
            var pages = _pageRepository.GetPages();
            return View(new ListModel
            {
                Pages = pages
            });
        }

        public IActionResult Edit(PageType type)
        {
            var page = _pageRepository.Get(type);
            return View(new EditModel()
            {
                Page = page
            });
        }
    }
}