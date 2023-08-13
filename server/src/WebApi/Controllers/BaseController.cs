using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
   
}