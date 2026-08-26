using Microsoft.AspNetCore.Mvc;

namespace MinhaPrimeiraAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
	[HttpGet]
	public ActionResult<IEnumerable<Usuario>> Listar()
	{
		var usuarios = new List<Usuario>
		{
			new(1, "Ana Silva", "ana.silva@email.com"),
			new(2, "Bruno Santos", "bruno.santos@email.com"),
			new(3, "Carla Oliveira", "carla.oliveira@email.com")
		};

		return Ok(usuarios);
	}
}

public record Usuario(int Id, string Nome, string Email);
