using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pacient_manager.Models;
using pacient_manager.Services;

namespace pacient_manager.Controllers;

[ApiController]
public class RegisterPacientController : ControllerBase
{
    private readonly RegisterPacientService _registerPacientService;
    public RegisterPacientController(RegisterPacientService registerPacientService)
    {
        _registerPacientService = registerPacientService;
    }
    
    [HttpPost]
    [Route("/api/registeruser")]
    public async Task<IActionResult> RegisterUser(Pacient pacient)
    {
        await _registerPacientService.RegisterPacient(pacient);
        return Ok("Usuário cadastrado com sucesso!");
    }
}