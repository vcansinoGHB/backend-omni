using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using desa.Interfaces;
using desa.Models;

namespace desa.Controllers;

[Route("json/data")]
[ApiController]
public class CompanyController : Controller
{
    private readonly ICompany _companyApiService;

    public CompanyController(ICompany companyApiService)
    {
        _companyApiService = companyApiService;
    }

    [HttpGet]
    [Route("getCompanies")]
    public async Task<IActionResult> getListCompany()
    {
        try {    
            var companies = await _companyApiService.GetCompanies(); 
            return Ok(companies);
        } catch (Exception e) {
              return StatusCode(500, e.Message.ToString());
        }
    }
}