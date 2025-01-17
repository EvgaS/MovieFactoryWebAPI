using AutoMapper;
using LINQtoCSV;

using Microsoft.AspNetCore.Mvc;

using MovieFactoryWebAPI.DTo;
using MovieFactoryWebAPI.Interfaces;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository,
            IMovieRepository movieRepository,
            IActorRepository actorRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        public IActionResult GetRoles()
        {
            var roles = _mapper.Map<List<RoleDto>>(_roleRepository.GetRoles());

            if (!roles.Any())
                return NotFound("Any roles have beeen created.Please enter role");

            return Ok(roles);
        }


        [HttpGet("{roleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult GetRole(int roleId)
        {
            var role = _mapper.Map<RoleDto>(_roleRepository.GetRole(roleId));

            if (role == null)
                return NotFound("The role not found");

            return Ok(role);
        }


        [HttpGet("GetRoleWithMaxBudget")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult GetRoleByBudget()
        {
            var role = _mapper.Map<RoleDto>(_roleRepository.GetRoleWithMaxBudget());

            if (role == null)
                return NotFound("Any role not consist the budget");

            return Ok(role);
        }


        [HttpGet("/Actors/{roleName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Actor))]
        public IActionResult GetActorForRole(string roleName)
        {
            var role = _mapper.Map<ActorDto>(
                _roleRepository.GetActorByRole(roleName));

            if (role == null)
                return NotFound("Any role not found for this actor");

            return Ok(role);
        }


        [HttpPost]
        public IActionResult CreateRole([FromQuery] int movieId, [FromBody] RoleDto roleCreate)
        {

            if (roleCreate == null)
                return BadRequest(ModelState);

            var roles = _roleRepository.GetRoles()?.FirstOrDefault(c => c.RoleName.Trim().ToUpper() == roleCreate.RoleName!.TrimEnd().ToUpper());

            if (roles != null)
            {
                ModelState.AddModelError("", "Role already exists");
                return StatusCode(422, ModelState);
            }

            var roleMap = _mapper.Map<Role>(roleCreate);

            roleMap.Movie = _movieRepository.GetMovie(movieId) ?? throw new Exception();

            if (!_roleRepository.CreateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRole(int roleId, [FromBody] RoleDto updatedRole)
        {
            if (updatedRole == null)
                return BadRequest(ModelState);

            var role = _roleRepository.GetRole(roleId);

            if (role == null)
                return NotFound();

            role.RoleName = updatedRole.RoleName!;
            role.RoleDescription = updatedRole.RoleDescription;

            if (!_roleRepository.UpdateRole(role))
            {
                ModelState.AddModelError("", "Something went wrong updating role");
                return StatusCode(500, ModelState);
            }

            return Ok($"The role-{updatedRole.RoleName} have been succesfully updated");
        }


        [HttpPut("{actorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRoleAddActor(int actorId, int roleId)
        {
            var role = _roleRepository.GetRole(roleId);

            if (role == null)
                return NotFound();

            var actor = _actorRepository.GetActor(actorId);

            if (actor == null)
                return BadRequest(ModelState);

            role.Actor = actor;

            var roleMap = _mapper.Map<Role>(role);

            if (!_roleRepository.UpdateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating role");
                return StatusCode(500, ModelState);
            }

            return Ok($"The role-{role.RoleName} have been succesfully updated");
        }


        [HttpDelete]
        public IActionResult DeleteRole(int roleId)
        {
            var roleToDelete = _roleRepository.GetRole(roleId);

            if (roleToDelete == null)
                return NotFound();

            if (!_roleRepository.DeleteRole(roleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting role");
            }

            return NoContent();
        }

    }
       
}
