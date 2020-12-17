using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApplication.Repository;
using SampleApplication.Repository.Exceptions;
using SampleApplication.Repository.Models;

namespace SampleApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : BaseController
    {
        private readonly IPeopleRepository _repository;

        public PeopleController(ILogger<PeopleController> logger, IPeopleRepository repository)
            : base(logger)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        /// <summary>
        /// Gets all existing people.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync().ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets the person with the specified id.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id).ConfigureAwait(false);
                if(result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets how many existing people.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("totalcount")]
        public async Task<ActionResult> TotalCountAsync()
        {
            try
            {
                var result = await _repository.CountAllAsync().ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a person.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /People
        ///     {
        ///        "firstName": "Santa",
        ///        "lastName": "Claus"
        ///     }
        ///
        /// </remarks>
        /// <param name="person"></param>
        /// <returns>A newly created Person</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] Person person)
        {
            try
            {
                var result = await _repository.InsertAsync(person).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates details of person with specified id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /People
        ///     {
        ///        "id": 1,
        ///        "firstName": "Father",
        ///        "lastName": "Xams"
        ///     }
        ///
        /// </remarks>
        /// <param name="person"></param>
        /// <returns>The updated Person</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] Person person)
        {
            try
            {
                var result = await _repository.UpdateAsync(person).ConfigureAwait(false);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes a person with the specified id.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id).ConfigureAwait(false);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
