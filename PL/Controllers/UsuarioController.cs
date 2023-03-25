using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
		[TempData] 
		public string numerotemp { get; set; }
        [TempData]
        public int resultadotemp { get; set; }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Login(ML.Usuario usuario)
		{
	

			ML.Result result = BL.Usuario.GetByName(usuario.Nombre);

			if (result.Correct)
			{
				usuario = (ML.Usuario)result.Object;

				if (usuario.Contraseña != usuario.Contraseña || usuario.Contraseña == null)
				{
					ViewBag.Message = "Contraseña Incoirrecta";
					return PartialView("Modal");
				}
				else
				{
					
					HttpContext.Session.SetString("Usuario", usuario.IdUsuario.ToString()); ;
					var user = HttpContext.Session.GetString("Usuario");
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				ViewBag.Message = "Usuario no encontrado";
				return PartialView("Modal");
			}
		}
		[HttpGet]
        public ActionResult Registrarse()
        {
            return View();
        }
		[HttpPost]
		public ActionResult  Registrarse(string nombre, string password)
		{
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = nombre;
            usuario.Contraseña = password; 
            ML.Result result = BL.Usuario.AddUsuario(usuario);
            if (result.Correct)
            {
				return RedirectToAction("Login", "Usuario");
			}
            else {
				return View();
			}
			
		}

		[HttpGet]
        public ActionResult GetAll()
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("Usuario"));
			ML.Result result = BL.Historial.GetHistorialUsuario(idUsuario);
            ML.Historial historial=new ML.Historial();
			historial.Historiales = result.Objects;
			historial.Numero = numerotemp;
			historial.Resultado = resultadotemp;
            return View(historial);
        }

        [HttpPost]
        public ActionResult Superdigito(ML.Historial historial)
        {
			ML.Result result=new ML.Result();
			historial.Usuario=new ML.Usuario();
			historial.Usuario.IdUsuario = int.Parse(HttpContext.Session.GetString("Usuario"));

			historial.Resultado = BL.Historial.SuperDigito(historial.Numero.ToString());
			result = BL.Historial.GetByNumeroAndUsername(historial.Numero,historial.Usuario.IdUsuario);
			if (result.Correct)
			{
				ML.Historial historial1=new ML.Historial();
				historial1 = (ML.Historial)result.Object;
				result = BL.Historial.UpdateHistorial(historial1.IdHistorial);
			}
			else {
                result = BL.Historial.AddHistorial(historial);
            }
			//         
            resultadotemp = historial.Resultado;
			numerotemp = historial.Numero;
			
            return RedirectToAction("GetAll", "Usuario");
        }

		[HttpGet]
		public ActionResult DeleteByIdHistorial(int IdHistorial) {
			ML.Result result = BL.Historial.DeletHistorialById(IdHistorial);
			ViewBag.Message = result.Message;
            return PartialView("Modal");
		}

        [HttpGet]
        public ActionResult DeleteAllByIdUsuario()
        {
			int idUsuario = int.Parse(HttpContext.Session.GetString("Usuario"));
			ML.Result result = BL.Historial.DeletHistorialAll(idUsuario);
            ViewBag.Message = result.Message;
            return PartialView("Modal");
        }


    }
}
