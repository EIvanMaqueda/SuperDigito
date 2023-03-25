using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Historial
    {
        public static ML.Result GetHistorialUsuario(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaSuperDigitoContext context = new DL.EmaquedaSuperDigitoContext())
                {
                    //var query = context.UsuarioGetAll().ToList();
                    var query = context.Historials.FromSqlRaw($"HistorialGetByIdUsuario {idUsuario}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Historial historial= new ML.Historial();
                            historial.IdHistorial = obj.IdHistorial;
                            historial.HoraFecha = obj.FechaHora.Value;
                            historial.Resultado = obj.Resultado.Value;
                            historial.Numero = obj.Numero.ToString();
                            historial.Usuario=new ML.Usuario(); 
                            historial.Usuario.IdUsuario= obj.IdUsuario.Value;
                           
                            result.Objects.Add(historial);
                        }
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error: " + ex.Message;

            }
            return result;
        }

        public static ML.Result AddHistorial(ML.Historial historial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaSuperDigitoContext context = new DL.EmaquedaSuperDigitoContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"HistorialAdd '{historial.Numero}',{historial.Resultado},{historial.Usuario.IdUsuario}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;

        }

        public static ML.Result DeletHistorialById(int idHistorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaSuperDigitoContext context = new DL.EmaquedaSuperDigitoContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"HistorialDelteById {idHistorial}");
                    if (query > 0)
                    {
                        result.Message = "Historial borrado exitosamente";
                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;

        }

        public static ML.Result DeletHistorialAll(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaSuperDigitoContext context = new DL.EmaquedaSuperDigitoContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"HistorialDeleteAll {IdUsuario}");
                    if (query > 0)
                    {
                        result.Message = "Historial borrado exitosamente";
                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;

        }
        public static ML.Result UpdateHistorial(int idHistorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaSuperDigitoContext context = new DL.EmaquedaSuperDigitoContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"HistorialUpdate {idHistorial}");
                    if (query > 0)
                    {
                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;

        }
        public static int SuperDigito(string numero) {
        
           // ML.Historial historial= new ML.Historial();
            string cadena = numero.ToString();
            char[] caracteres = cadena.ToCharArray();
            while (caracteres.Length > 1)
            {

                int[] numeros = new int[caracteres.Length];
                int sumatoria = 0;
                for (int j = 0; j < numeros.Length; j++)
                {
                    numeros[j] = (int)Char.GetNumericValue(caracteres[j]);
                    sumatoria = sumatoria + numeros[j];
                }
                cadena = sumatoria.ToString();
                caracteres = cadena.ToCharArray();
            }
            int resultado=int.Parse(cadena);

            return resultado;
        }
        public static ML.Result GetByNumeroAndUsername(string numero, int idUsuario)
        {
            ML.Result result = GetHistorialUsuario(idUsuario);
            ML.Historial historial = new ML.Historial();
            result.Correct = false;
            historial.Historiales = result.Objects;
            foreach (ML.Historial historial1 in historial.Historiales)
            {
                if (historial1.Numero == numero)
                {
                    result.Correct = true;
                    result.Object = historial1;
                    break;
                }
            }
            return result;
        }
    }
}
