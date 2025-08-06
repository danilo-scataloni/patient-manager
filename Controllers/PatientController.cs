using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using pacient_manager.DTOs;
using patient_manager.Interfaces;
using patient_manager.Services;

namespace patient_manager.Controllers;

[ApiController]
[Route("[controller]")] 
public class PatientController(
    IPatientReadService readService,
    IPatientWriteService writeService)
    : ControllerBase
{

    [HttpGet]
    [Route("/api/patients")]
    public async Task<IEnumerable> GetAllPatients()
    {
        return await readService.GetAllPatients();
    }

    [HttpGet]
    [Route("/api/patients/{id}")]
    public async Task<ActionResult<PatientDto>> GetPatient([FromRoute] Guid id)
    {
        try
        {
            return await readService.GetPatient(id);
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
            writeService.DeletePatient(id);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Um paciente com esse id não foi encontrado");
        }
    }
        
        
    [HttpPost]
    [Route("/api/patient")]
    public async Task<IActionResult> RegisterPatient(PatientDto patient)
    {
        try
        {
            await writeService.RegisterPatient(patient);
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
    public async Task<IActionResult> UpdatePatient(Guid patientId, PatientDto patient)
    {
        try
        {
            await writeService.UpdatePatient(patientId, patient);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Paciente não encontrado!");
        }
    }
    
}