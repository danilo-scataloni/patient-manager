using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using pacient_manager.DTOs;
using patient_manager.Interfaces;
using patient_manager.Services;

namespace patient_manager.Controllers;

[ApiController]
[Route("[controller]")] 
public class PatientController : ControllerBase
{
    private readonly PatientService _patientService;
    private readonly IPatientReadService _readService;
    private readonly IPatientWriteService _writeService;
    public PatientController(PatientService patientService, IPatientReadService readService, IPatientWriteService writeService)
    {
        _patientService = patientService;
        _readService = readService;
        _writeService = writeService;
    }

    [HttpGet]
    [Route("/api/patients")]
    public async Task<IEnumerable> GetAllPatients()
    {
        return await _readService.GetAllPatients();
    }

    [HttpGet]
    [Route("/api/patients/{id}")]
    public async Task<ActionResult<PatientDto>> GetPatient([FromRoute] Guid id)
    {
        try
        {
            return await _readService.GetPatient(id);
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
            await _writeService.RegisterPatient(patient);
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
       await _writeService.UpdatePatient(patientId, patient);
       return Ok();
    }
    
}