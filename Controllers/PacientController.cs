using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
    [Route("/pacients")]
    public async Task<ActionResult<List<Pacient>>> GetAllPacients()
    {
        _pacientService.GetAllPacients();
        return await _pacientService.GetAllPacients();
    }
    
    [HttpPost]
    [Route("/api/register-user")]
    public async Task<IActionResult> RegisterUser(Pacient pacient)
    {
        await _pacientService.RegisterPacient(pacient);
        return Ok("Usuário cadastrado com sucesso!");
    }
}