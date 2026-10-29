using DSW_3P_PR01.Models;
using DSW_3P_PR01.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSW_3P_PR01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MisionController : ControllerBase
{
    private readonly IMisionService _service;

    public MisionController(IMisionService service)
    {
        _service = service;
    }

    [HttpGet]
    [EndpointSummary("Obtener todas las misiones")]
    [ProducesResponseType(typeof(List<Mision>), StatusCodes.Status200OK)]
    public IActionResult ObtenerTodas() => Ok(_service.ObtenerTodas());

    [HttpGet("{id:int}")]
    [EndpointSummary("Obtener por id")]
    [ProducesResponseType(typeof(List<Mision>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorId(int id)
    {
        var mision = _service.ObtenerPorId(id);
        return mision == null ? NotFound() : Ok(mision);
    }

    [HttpPost]
    [EndpointSummary("Agregar mision")]
    [ProducesResponseType(typeof(Mision), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Agregar([FromBody] Mision mision)
    {
        try
        {
            var creado = _service.Agregar(mision);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut]
    [EndpointSummary("Actualizar mision")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public IActionResult Actualizar(int id, [FromBody] Mision mision)
    {
        try
        {
            return _service.Actualizar(id, mision) ? NoContent() : NotFound();
        }
        catch(ArgumentException ex)
        {
            return BadRequest(new { mesanje = ex.Message });
        }
    }

    [HttpDelete]
    [EndpointSummary("Eliminar misiob")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Eliminar(int id)
    {
        try
        {
            return _service.Eliminar(id) ? NoContent() : NotFound();
        }
        catch(ArgumentNullException ex)
        {
            return Conflict(new { mensaje = ex.Message});
        }
    }











}
