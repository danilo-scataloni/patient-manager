using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pacient_manager.DTOs;
using pacient_manager.Models;
using pacient_manager.Services;

namespace pacient_manager.Controllers;

[ApiController]
[Route("[controller]")] 
public class PacientController : ControllerBase
{
    private readonly PacientService _pacientService;
    public PacientController(PacientService pacientService)
    {
        _pacientService = pacientService;
    }

    [HttpGet]
    [Route("/api/pacients")]
    public async Task<IEnumerable<PacientDto>> GetAllPacients()
    {
        return await _pacientService.GetAllPacients();
    }

    [HttpGet]
    [Route("/api/pacients/{id}")]
    public async Task<ActionResult<PacientDto>> GetPacient(int id)
    {
        try
        {
            return await _pacientService.GetPacient(id);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
        
    }
        
        
    [HttpPost]
    [Route("/api/pacient")]
    public async Task<IActionResult> RegisterPacient(PacientDto pacient)
    {
        try
        {
            await _pacientService.RegisterPacient(pacient);
            return Created();
        }
        catch (ValidationException ex)
        {
            return Conflict($"Um paciente com o documento {pacient.Document} já existe!");
        }
        catch (BadHttpRequestException)
        {
            return BadRequest("Dados inválidos!");
        }
        
    }

    [HttpPut]
    [Route("/api/pacient/{pacientId}")]
    public async Task<IActionResult> UpdatePacient(int pacientId, PacientDto pacient)
    {
       await _pacientService.UpdatePacient(pacientId, pacient);
       return Ok();
    }
    
}