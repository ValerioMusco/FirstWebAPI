using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class GenreController : ControllerBase {

        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService){
            
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAll() {

            return Ok( _genreService.GetAll("Genre") );
        }

        [HttpGet("Details/{id}")]
        public IActionResult GetGenreDetails(int id) {

            return Ok( _genreService.GetById("Genre", id) );
        }

        [HttpPost]
        [Authorize( "AdminPolicy" )]
        public IActionResult CreateGenre(Genre genre) {

            if( _genreService.Add( genre.Label ) )
                return Ok( "Le nouveau genre à été créé." );
            return BadRequest( "Erreur lors de la création du genre." );
        }
    }
}
