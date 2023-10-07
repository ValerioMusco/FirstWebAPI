using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPMVC_DAL.Models.Form {
    public class GameForm {

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int GenreId { get; set; }
    }
}
