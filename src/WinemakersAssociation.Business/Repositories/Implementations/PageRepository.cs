using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WinemakersAssociation.Business.Repositories.Interfaces;
using WinemakersAssociation.Data.Entities;
using WinemakersAssociation.Data.Enums;
using WinemakersAssociation.Persistence.Common;

namespace WinemakersAssociation.Business.Repositories.Implementations
{
    public class PageRepository : IPageRepository
    {
        private readonly MainContext _context;

        public PageRepository(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Page contents
        /// </summary>
        /// <returns></returns>
        public PageEntity Get(PageType type, bool onlyPublished = false)
        {
            return
                _context.Pages.Include(pg => pg.Contents)
                    .OrderByDescending(pg => pg.Id)
                    .FirstOrDefault(pg => pg.Type == type);
        }


        /// <summary>
        /// Available pages
        /// </summary>
        /// <returns></returns>
        public Dictionary<PageType, string> GetPages()
        {
            return _context.Pages.OrderByDescending(p => p.Id).ToDictionary(pg => pg.Type, pg => pg.Name);
        }

        /// <summary>
        /// Get page content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageContentEntity GetContent(long id)
        {
            return _context.PageContents.Include(p => p.Page).OrderByDescending(pg => pg.Id).FirstOrDefault(pgContent => pgContent.Id == id);
        }

        /// <summary>
        /// Save content
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entity"></param>
        public void Save(PageType type, PageContentEntity entity)
        {

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Entry(entity).State = EntityState.Added;
                entity.Page = Get(type);
                entity.Created = DateTime.Now;
                _context.PageContents.Add(entity);
            }

            _context.SaveChanges();


            if (!entity.Page.IsMultipage && entity.Published)
            {
                foreach (
                    var pageContentEntity in
                    _context.PageContents.Where(subPage => subPage.Page.Id == entity.Page.Id && subPage.Id != entity.Id)
                )
                {
                    pageContentEntity.Published = false;
                }
            }

            _context.SaveChanges();

        }

        /// <summary>
        /// Delete content
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long id)
        {
            _context.PageContents.Remove(_context.PageContents.First(subPage => subPage.Id == id));
            _context.SaveChanges();
        }
    }
}