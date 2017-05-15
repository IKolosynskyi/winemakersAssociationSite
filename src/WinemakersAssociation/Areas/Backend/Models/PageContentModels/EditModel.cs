using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WinemakersAssociation.Data.Entities;
using WinemakersAssociation.Data.Enums;

namespace WinemakersAssociation.Areas.Backend.Models.PageContentModels
{
    public class EditModel
    {
        [HiddenInput]
        public long? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool Published { get; set; }

        [HiddenInput]
        public PageType Type { get; set; }
    }
}