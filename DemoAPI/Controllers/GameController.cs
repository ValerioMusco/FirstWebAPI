using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models;
using DemoASPMVC_DAL.Models.Form;
using DemoASPMVC_DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class GameController : ControllerBase {

        private readonly IGameService _gameService;

        public GameController( IGameService gameService ) {

            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetAll() {

            return Ok( _gameService.GetGames() );
        }

        [HttpGet("Details/{id}")]
        public IActionResult GetById(int id) { 
            
            return Ok(_gameService.GetById(id));
        }

        [HttpGet("Genre/{idGenre}")]
        public IActionResult GetByGenreName(int idGenre ) {

            return Ok( _gameService.GetGames().Where( game => game.IdGenre == idGenre ) );
        }

        [HttpPost]
        [Authorize("AdminPolicy")]
        public IActionResult Create(GameForm game) {

            if(_gameService.Create(game))
                return Ok("Le jeu à été créé");
            return BadRequest( "Erreur lors de la création du jeu" );
        }

        [HttpDelete]
        [Authorize("AdminPolicy")]
        public IActionResult Delete(int id) {

            if( _gameService.Delete( id ) )
                return Ok( "Jeu supprimé avec succès" );
            return BadRequest( "Erreur lors de la suppression du jeu." );
        }
    }
}
