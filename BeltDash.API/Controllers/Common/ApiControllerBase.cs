using Microsoft.AspNetCore.Mvc;

namespace BeltDash.API.Controllers.Common
{
    /// <summary>
    /// Base controller for all API controllers.
    /// Provides the base route and inherits from ControllerBase.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
