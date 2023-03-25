using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result AddUsuario(ML.Usuario usuario) { 
            ML.Result result = new ML.Result();
			try
			{
				using (DL.EmaquedaSuperDigitoContext context= new DL.EmaquedaSuperDigitoContext())
				{
					int query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.Contraseña}'");
					if (query>0) {
						result.Correct = true;
						result.Message = "Usuario Agregado correctamente";
					}
				}
			}
			catch (Exception ex)
			{

			result.Correct= false;
				result.Message = ex.Message;
			}
            return result;

        }

		public static ML.Result GetByName(string name)
		{
			ML.Result result = new ML.Result();
			try
			{
				using (DL.EmaquedaSuperDigitoContext context = new DL.EmaquedaSuperDigitoContext())
				{
					var query = context.Usuarios.FromSqlRaw($"UsuarioGetByName '{name}'").AsEnumerable().FirstOrDefault();

					if (query != null)
					{
						ML.Usuario usuario = new ML.Usuario();

						usuario.IdUsuario = query.IdUsuario;
						usuario.Nombre = query.Nombre;
						usuario.Contraseña = query.Contraseña;
					
						result.Correct = true;


						result.Object = usuario;
					}
					else
					{
						result.Correct = false;
					}

				}

			}
			catch (Exception ex)
			{
				result.Correct = false;
				result.Message = "Error usuario no enecontrado: " + ex.Message;

			}
			return result;

		}

	}
}