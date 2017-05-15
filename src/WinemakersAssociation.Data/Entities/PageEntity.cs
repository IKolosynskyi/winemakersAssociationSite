using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WinemakersAssociation.Data.Enums;

namespace WinemakersAssociation.Data.Entities
{
    [Table("page")]
    public class PageEntity : IBaseEntity
    {
        [Column("page_id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public PageType Type { get; set; }

        [Column("ismultipage")]
        public bool IsMultipage { get; set; }

        [ForeignKey("page_id")]
        public virtual ICollection<PageContentEntity> Contents { get; set; }


    }
}