using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using pacient_manager.DTOs;
using patient_manager.Services;

namespace patient_manager.Controllers;

[ApiController]
[Route("[controller]")] 
public class PatientController : ControllerBase
{
    private readonly PatientService _patientService;
    public PatientController(PatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    [Route("/api/patients")]
    public async Task<IEnumerable<PatientDto>> GetAllPatients()
    {
        return await _patientService.GetAllPatients();
    }

    [HttpGet]
    [Route("/api/patients/{id}")]
    public async Task<ActionResult<PatientDto>> GetPatient([FromRoute] int id)
    {
        try
        {
            return await _patientService.GetPatient(id);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
        
    }
        
        
    [HttpPost]
    [Route("/api/patient")]
    public async Task<IActionResult> RegisterPatient(PatientDto patient)
    {
        try
        {
            await _patientService.RegisterPatient(patient);
            return Created();
        }
        catch (ValidationException ex)
        {
            return Conflict($"Um patiente com o documento {patient.Document} já existe!");
        }
        catch (BadHttpRequestException)
        {
            return BadRequest("Dados inválidos!");
        }
        
    }

    [HttpPut]
    [Route("/api/patient/{patientId}")]
    public async Task<IActionResult> UpdatePatient(int patientId, PatientDto patient)
    {
       await _patientService.UpdatePatient(patientId, patient);
       return Ok();
    }
    
}