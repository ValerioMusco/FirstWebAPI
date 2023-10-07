using DemoAPI.Tools;
using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models.Form;
using DemoASPMVC_DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoAPI.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly TokenManager _tokenManager;

        public UserController( IUserService userService, IGameService gameService, TokenManager token ) {
            _userService = userService;
            _gameService = gameService;
            _tokenManager = token;
        }

        [HttpGet]
        public IActionResult GetAllUsers() {

            return Ok( _userService.GetAll( "Users" ) );
        }

        [HttpGet( "favorite/{id}" )]
        [Authorize( "IsConnected" )]
        public IActionResult GetUserFavorite( int id ) {

            return Ok( _gameService.GetByUserId( id ) );
        }

        [HttpPost( "register/" )]
        public IActionResult Register( UserFormRegister userFormRegister ) {

            if( !ModelState.IsValid )
                return BadRequest( "Erreur lors de l'entrée des informations." );
            if( _userService.Register( userFormRegister.Email, userFormRegister.Password, userFormRegister.Username ) )
                return Ok( "Nouvel utilisateur créé !" );
            return BadRequest( "Erreur lors de la création de l'utilisateur." );
        }

        [HttpPost( "login/" )]
        public IActionResult Login( UserFormLogin userFormLogin ) {

            string json;
            if( !ModelState.IsValid )
                return BadRequest( "Erreur lors de l'entrée des informations." );
            json = JsonConvert.SerializeObject( 
                new { 
                    token = _tokenManager.GenerateToken( _userService.Login( userFormLogin.Email, userFormLogin.Password ) ), 
                    user = _userService.GetAll( "Users" ).Where( user => user.Email == userFormLogin.Email ).FirstOrDefault() 
                } 
            );
            return Ok( json );
        }

        [HttpPost( "AddFavorite/" )]
        [Authorize("IsConnected")]
        public IActionResult AddToFavorite( UserFavorite userFavorite) {

            try {

                _gameService.AddFavorite( userFavorite.idUser, userFavorite.idGame );
                return Ok( "Jeu ajouté au favoris" );
            }
            catch( Exception ex ) {

                return BadRequest( ex.Message );
            }
            
        }
    }
}
