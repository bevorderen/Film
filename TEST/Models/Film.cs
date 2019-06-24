using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet;

namespace TEST.Models
{
    public class Film
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public DateTime Created_post { get; set; }

        [Required]
        public string Regisseur { get; set; }

        public string CreatorName { get; set; }

        public String Path { get; set; }

    }

    public class FilmEditModel
    {
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [Required]
        public string Regisseur { get; set; }

        public string CreatorName { get; set; }

       public HttpPostedFileBase File { get; set; }
    }
}