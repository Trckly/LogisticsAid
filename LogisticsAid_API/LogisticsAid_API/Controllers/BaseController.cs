using System.Text.Json;
using LogisticsAid_API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LogisticsAid_API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected async Task<ActionResult> ExecuteSafely(Func<Task<ActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {
            return BadRequest(new { message = pgEx.MessageText });
        }
        catch (TripAlreadyExistsException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status499ClientClosedRequest, 
                JsonSerializer.Serialize(new { message = "Operation was canceled" }));
        }
        catch (NullReferenceException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, 
                JsonSerializer.Serialize(new { message = e.Message }));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status409Conflict, 
                JsonSerializer.Serialize(new { message = e.Message }));
        }
    }
}