using AutoMapper;
using LINQtoCSV;
using Microsoft.AspNetCore.Mvc;
using MovieFactoryWebAPI.DTo;
using MovieFactoryWebAPI.Interfaces;

namespace MovieFactoryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageProcessController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ManageProcessController(IRoleRepository roleRepository,
            IMovieRepository movieRepository,
            IActorRepository actorRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _mapper = mapper;
        }


        [HttpGet("SaveMovieToFile/{fileName}")]
        public ActionResult SaveRolesToFile(string fileName)
        {
            try
            {
                var roleStatistic = _mapper.Map<List<RoleCSVDto>>(_roleRepository.GetRolesForFile());

                if (!roleStatistic.Any())
                    NotFound("Any role have not been created yet");

                CsvFileDescription outputFileDescription = new CsvFileDescription
                {
                    SeparatorChar = ',',
                    FirstLineHasColumnNames = true,
                    FileCultureName = "en-GB",
                    EnforceCsvColumnAttribute = true
                };

                CsvContext cc = new CsvContext();

                cc.Write(
                roleStatistic,
                $"roleStatistic_{fileName}.csv",
                   outputFileDescription);

                var proces = new System.Diagnostics.Process();
                proces.StartInfo.UseShellExecute = true;
                proces.StartInfo.FileName = $"roleStatistic_{fileName}.csv";
                proces.Start();

                return Ok($"The file with {roleStatistic.Count} have been sucesfully created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database" + ex.Message);
            }
        }     

    }
}

