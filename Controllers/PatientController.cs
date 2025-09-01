using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using patient_manager.Models;
using patient_manager.Services;

namespace patient_manager.Controllers;

[ApiController]
[Route("[controller]")] 
public class PatientController(
    IPatientService service)
    : ControllerBase
{

    [HttpGet]
    [Route("/api/patients")]
    public async Task<IEnumerable> GetAllPatients()
    {
        return await service.GetAllPatients();
    }

    [HttpGet]
    [Route("/api/patients/{id}")]
    public async Task<ActionResult<PatientDto>> GetPatient([FromRoute] Guid id)
    {
        try
        {
            return await service.GetPatient(id);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete]
    [Route("/api/patients/{id}")]
    public async Task<IActionResult> DeletePatient([FromRoute] Guid id)
    {
        try
        {
            await service.DeletePatient(id);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Um paciente com esse id não foi encontrado");
        }
    }
        
        
    [HttpPost]
    [Route("/api/patients")]
    public async Task<IActionResult> RegisterPatient(PatientDto patient)
    {
        try
        {
            await service.CreatePatient(patient);
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

    [HttpPatch]
    [Route("/api/patients/{patientId}")]
    public async Task<IActionResult> UpdatePatient(Guid patientId, PatientDto patient)
    {
        try
        {
            await service.UpdatePatient(patientId, patient);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Paciente não encontrado!");
        }
    }
    
}