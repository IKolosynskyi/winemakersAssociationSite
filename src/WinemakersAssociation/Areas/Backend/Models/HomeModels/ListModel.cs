using System.Collections.Generic;
using WinemakersAssociation.Data.Enums;

namespace WinemakersAssociation.Areas.Backend.Models.HomeModels
{
    public class ListModel
    {
        public Dictionary<PageType, string> Pages { get; set; }
    }
}