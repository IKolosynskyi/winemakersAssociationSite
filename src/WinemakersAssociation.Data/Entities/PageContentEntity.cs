using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace WinemakersAssociation.Data.Entities
{
    [Table("pagecontent")]
    public class PageContentEntity : IBaseEntity
    {
        [Column("pagecontent_id")]
        public long Id { get; set; }

        [ForeignKey("page_id")]
        public PageEntity Page { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("title")]
        public string Title { get; set; }
       
        [Column("published")]
        public bool Published { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

    }
}