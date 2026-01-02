using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformController(IPlatformRepo repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var platforms = repository.GetAllPlatforms();

        return Ok(platforms);
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        try
        {
            var platform = repository.GetPlatformById(id);

            if (platform != null)
            {
                return Ok(mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
    {
        var mappedPlatform = mapper.Map<Platform>(platform);
        repository.CreatePlatform(mappedPlatform);
        repository.SaveChanges();

        var mappedResponse = mapper.Map<PlatformReadDto>(mappedPlatform);

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = mappedResponse.Id }, mappedResponse);
    }
}