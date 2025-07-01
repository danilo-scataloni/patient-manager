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
    public async Task<PacientDto> GetPacient(int id)
    {
        return await _pacientService.GetPacient(id);
    }
        
        
    [HttpPost]
    [Route("/api/pacient")]
    public async Task<IActionResult> RegisterPacient(PacientDto pacient)
    {
        await _pacientService.RegisterPacient(pacient);
        return Ok("Usuário cadastrado com sucesso!");
    }

    [HttpPut]
    [Route("/api/pacient/{pacientId}")]
    public async Task<IActionResult> UpdatePacient(int pacientId, PacientDto pacient)
    {
       var existingPacient = await _pacientService.GetPacient(pacientId);
       
       await _pacientService.UpdatePacient(pacientId, pacient);
       return Ok();
    }
    
}