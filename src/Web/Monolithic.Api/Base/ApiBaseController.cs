namespace Monolithiv.Api.Base;
public class ApiBaseController : ControllerBase
    {
        protected OkObjectResult OKResponse(IEnumerable<object> rows, long? total = null, string message = null)
        {
            var data = new { rows, total };

            return Ok(new GenericResponse(true, message, (int)HttpStatusCode.OK, data));
        }

        protected OkObjectResult OKResponse(string message = null, object data = null)
        {
            return Ok(new GenericResponse(true, message, (int)HttpStatusCode.OK, data));
        }

        protected OkObjectResult OKResponse()
        {
            return Ok(new GenericResponse());
        }

        protected OkObjectResult OKResponse(string message)
        {
            return Ok(new GenericResponse(true, message, (int)HttpStatusCode.OK));
        }

        protected BadRequestObjectResult BadRequestResponse(string message)
        {
            return BadRequest(new GenericResponse(false, message, (int)HttpStatusCode.BadRequest));
        }

        protected BadRequestObjectResult BadRequestResponse(string message, object data)
        {
            return BadRequest(new GenericResponse(false, message, (int)HttpStatusCode.BadRequest, data));
        }

        protected NotFoundObjectResult NotFoundResponse(string message, object data)
        {
            return NotFound(new GenericResponse(false, message, (int)HttpStatusCode.NotFound, data));
        }

        protected GenericResponse InternalErrorResponse(string message, Exception exception = null)
        {
            var problem = Problem(message);

            var newProblem = new { problem.Value, problem.ContentTypes, problem.DeclaredType, exception };
            return new GenericResponse(false, message, (int)HttpStatusCode.BadRequest, newProblem);
        }
    }