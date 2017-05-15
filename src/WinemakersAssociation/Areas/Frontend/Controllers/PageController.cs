using System.Linq;
using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Mvc;
using WinemakersAssociation.Areas.Frontend.Models.HomeModels;
using WinemakersAssociation.Business.Repositories.Interfaces;
using WinemakersAssociation.Data.Enums;

namespace WinemakersAssociation.Areas.Frontend.Controllers
{

    [Area("Frontend")]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public IActionResult Index(PageType type = PageType.About)
        {
            var page = _pageRepository.Get(type, true);
    

            return View(new IndexModel
            {
                Page = page
            });
        }

        public IActionResult Details(long id)
        {
            var subPage = _pageRepository.GetContent(id);
            return View(subPage);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
