using System.Collections.Generic;
using System.Linq;
using WinemakersAssociation.Data.Entities;
using WinemakersAssociation.Data.Enums;

namespace WinemakersAssociation.Business.Repositories.Interfaces
{
    public interface IPageRepository
    {
        PageEntity Get(PageType type, bool onlyPublished = false);

        Dictionary<PageType, string> GetPages();

        PageContentEntity GetContent(long id);

        void Save(PageType type, PageContentEntity entity);

        void Delete(long id);
    }
}