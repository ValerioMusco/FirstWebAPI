using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models.Form;
using DemoASPMVC_DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IUserService _userService;
        private readonly IGameService _gameService;

        public UserController( IUserService userService, IGameService gameService ) {
            _userService = userService;
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetAllUsers() {

            return Ok( _userService.GetAll( "Users" ) );
        }

        [HttpGet("favorite/{id}")]
        public IActionResult GetUserFavorite( int id ) {

            return Ok( _gameService.GetByUserId( id ) );
        }

        [HttpPost("register/")]
        public IActionResult Register( UserFormRegister userFormRegister) {
            
            if(!ModelState.IsValid) 
                return BadRequest( "Erreur lors de l'entrée des informations." );
            if( _userService.Register( userFormRegister.Email, userFormRegister.Password, userFormRegister.Username ) )
                return Ok( "Nouvel utilisateur créé !" );
            return BadRequest( "Erreur lors de la création de l'utilisateur." );
        }

        [HttpPost("login/")]
        public IActionResult Login( UserFormLogin userFormLogin) {

            if( !ModelState.IsValid )
                return BadRequest( "Erreur lors de l'entrée des informations." );
            return Ok( _userService.Login( userFormLogin.Email, userFormLogin.Password ) );
        }
    }
}
