using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Relations.Models
{
    public class Movie
    {
        public Movie()
        {
            Actors = new HashSet<Actor>();
        }

        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }

        //Navigation Properties
        public int CodeId {get;set;}
        public virtual Code Code { get; set; }

        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
        
    }
}