using System;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WinemakersAssociation.Areas.Backend.Models.PageContentModels;
using WinemakersAssociation.Business.Repositories.Interfaces;
using WinemakersAssociation.Data.Entities;
using WinemakersAssociation.Data.Enums;

namespace WinemakersAssociation.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class PageContentController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageContentController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public IActionResult List(PageType type)
        {
            var pages = _pageRepository.Get(type);
            return View(new ListModel()
            {
                Page = pages
            });
        }

        public IActionResult Edit(long? id, PageType? type)
        {
            EditModel model;
            if (id.HasValue)
            {
                model = Mapper.Map<EditModel>(_pageRepository.GetContent(id.Value));
            }
            else
            {
                if (!type.HasValue)
                {
                    throw new ArgumentException("Не можна створити елемент невідомої сторінки", nameof(type));
                }

                model = new EditModel()
                {
                    Type = type.Value
                };
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Id.HasValue ? _pageRepository.GetContent(model.Id.Value) : new PageContentEntity();

                Mapper.Map(model, entity);
                _pageRepository.Save(model.Type, entity);

                if (!model.Id.HasValue)
                    return RedirectToAction("Edit", new { entity.Id });
            }

            return View(model);
        }

        public IActionResult Delete(PageType type, long id)
        {
            _pageRepository.Delete(id);
            return RedirectToAction("List", new {type});
        }
    }
}