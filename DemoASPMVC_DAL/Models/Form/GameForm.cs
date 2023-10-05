using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPMVC_DAL.Models.Form {
    public class GameForm {

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int GenreId { get; set; }
    }
}
