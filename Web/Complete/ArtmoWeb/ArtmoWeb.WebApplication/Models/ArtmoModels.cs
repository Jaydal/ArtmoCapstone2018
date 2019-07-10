using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtmoWeb.WebApplication.Models
{
    public class ArtmoModels
    {
        public int Id { get; set; }

        [Range(0,300, ErrorMessage = "Please enter valid Marker ID (1 - 300)"), RegularExpression("([0-9]+)", ErrorMessage = "Invalid Input")]
        public int Marker { get; set; }

        public byte[] Image { get; set; }

        [Required(), StringLength(20, ErrorMessage = "Maximum of 20 Characters only!"), Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "Maximum of 20 Characters only!"), Display(Name = "Generic Term")]
        public string GenericTerm { get; set; }

        [StringLength(30, ErrorMessage = "Maximum of 30 Characters only!"), Display(Name = "Donor")]
        public string Donor { get; set; }

        [DataType(DataType.MultilineText), Required, StringLength(1000, ErrorMessage = "Maximum of 1000 Characters only!"), Display(Name = "Description in Englsh")]
        public string EngDesc { get; set; }

        //[DataType(DataType.MultilineText), Required, StringLength(255, ErrorMessage = "Maximum of 255 Characters only!"), Display(Name = "Description in Filipino")]
        //public string FilDesc { get; set; }

        [DataType(DataType.MultilineText), StringLength(1000, ErrorMessage = "Maximum of 1000 Characters only!"), Display(Name = "Description in Iloco")]
        public string IlocoDesc { get; set; }

        [Display(Name = "Date Acquired"), DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateAcquired { get; set; }

        [Display(Name = "Date Added")]
        public string DateAdded { get; set; }

        [Display(Name = "Last Modified")]
        public string LastModified { get; set; }

        public bool IsEdit { get; set; }

        public string _img64 { get; set; }

    }
}