﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Relations.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        [Required]
        public string FirstName { get; set; }

        //Navigation Properties
        public virtual ICollection<Movie> Movies { get; set; }
    }
}