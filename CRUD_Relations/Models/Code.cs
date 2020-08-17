using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_Relations.Models
{
    public class Code
    {
        [ForeignKey("Movie")]
        public int CodeId { get; set; }
        [Required]
        public string Number { get; set; }

        //Navigation Properties
        public virtual Movie Movie { get; set; }
    }
}