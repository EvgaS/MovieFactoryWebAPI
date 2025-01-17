using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieFactoryWebAPI.DTo;
using MovieFactoryWebAPI.Interfaces;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorController(IActorRepository actorRepository,
           IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
        public IActionResult GetActors()
        {
            var actors = _mapper.Map<List<ActorDto>>(_actorRepository.GetActors());

            if (!actors.Any())
                  return NotFound("Any actors have not beeen created yet.Please add actors");

            return Ok(actors);
        }


        [HttpGet("{actorId}")]
        [ProducesResponseType(200, Type = typeof(Actor))]
        [ProducesResponseType(400)]
        public IActionResult GetActor(int actorId)
        {         
            var actor = _mapper.Map<ActorDto>(_actorRepository.GetActor(actorId));

            if (actor == null)
                return NotFound($"Actor with - {actorId} have not been found!");

            return Ok(actor);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateActor([FromBody] ActorDto actorCreate)
        {
            if (actorCreate == null)
                return BadRequest(ModelState);

            var actors = _actorRepository.GetActorTrimToUpper(actorCreate);

            if (actors != null)
            {
                ModelState.AddModelError("", "Actor already exists");
                return StatusCode(422, ModelState);
            }

            var actorMap = _mapper.Map<Actor>(actorCreate);

            if (!_actorRepository.CreateActor( actorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok($"Successfully created");
        }


        [HttpPut("{actorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateActor(int actorId,
            [FromBody] ActorDto updatedActor)
        {

            if(updatedActor  == null)
                return BadRequest("The actor should not be null");         
               

           var actor = _actorRepository.GetActor(actorId);

            if(actor == null)
                return NotFound();

            var actorMap = _mapper.Map<Actor>(updatedActor);

            if (actorMap == null)
                return NotFound($"Actor with - {actorId} have not been found!");

            if (!_actorRepository.UpdateActor(actorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating actor");
                return StatusCode(500, ModelState);
            }

            return Ok($"Successfully updated");
        }


        [HttpDelete("{actorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteActor(int actorId)
        {
             var actorToDelete = _actorRepository.GetActor(actorId);

            if (actorToDelete == null)
                return NotFound("The actor have not been found.We can t delete it");

            if (!_actorRepository.DeleteActor(actorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong during deleting actor");
            }

            return NoContent();
        }
    }
}
