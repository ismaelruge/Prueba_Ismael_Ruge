using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba_Ismael_Ruge.Models;
using System.Data.SqlClient;
using Dapper;

namespace Prueba_Ismael_Ruge.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public string Cadena = @"Server=localhost;Database=Prueba_Ismael_ruge;Trusted_Connection=True;TrustServerCertificate=True;";


        [HttpGet]
        public async Task<ApiResponse> Get()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Cadena))
                {
                    string sql = @"SELECT * FROM Pacientes";
                    var r = await conn.QueryAsync<Pacientes>(sql, commandTimeout: 0);
                    return new ApiResponse { Issuccess = true, Data = r };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { Issuccess = false, Message = ex.Message };
            }
        }

        [HttpGet("List")]
        public async Task<ApiResponse> List([FromQuery] int Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);

                using (SqlConnection conn = new SqlConnection(Cadena))
                {
                    string sql = @"SELECT * FROM Pacientes WHERE Id = @Id";
                    var r = await conn.QueryAsync<Pacientes>(sql, parameters, commandTimeout: 0);
                    return new ApiResponse { Issuccess = true, Data = r };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { Issuccess = false, Message = ex.Message };
            }
        }

        [HttpPost]
        public async Task<ApiResponse> Post(Pacientes e)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TipoDocumento", e.TipoDocumento);
                parameters.Add("@NumeroDocumento", e.NumeroDocumento);
                parameters.Add("@Nombres", e.Nombres);
                parameters.Add("@Apellidos", e.Apellidos);
                parameters.Add("@CorreoElectronico", e.CorreoElectronico);
                parameters.Add("@Telefono", e.Telefono);
                parameters.Add("@FechaNacimiento", e.FechaNacimiento);
                parameters.Add("@EstadoAfiliacion", e.EstadoAfiliacion);

                using (SqlConnection conn = new SqlConnection(Cadena))
                {
                    string sql = @"INSERT INTO Pacientes (TipoDocumento, NumeroDocumento, Nombres, Apellidos
                                    , CorreoElectronico, Telefono, FechaNacimiento, EstadoAfiliacion)
                                                  VALUES (@TipoDocumento, @NumeroDocumento, @Nombres, @Apellidos
                                    , @CorreoElectronico, @Telefono, @FechaNacimiento, @EstadoAfiliacion)";
                    await conn.ExecuteAsync(sql, parameters, commandTimeout: 0);
                    return new ApiResponse { Issuccess = true };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { Issuccess = false, Message = ex.Message };
            }
        }

        [HttpPut]
        public async Task<ApiResponse> Put(Pacientes e)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", e.Id);
                parameters.Add("@TipoDocumento", e.TipoDocumento);
                parameters.Add("@NumeroDocumento", e.NumeroDocumento);
                parameters.Add("@Nombres", e.Nombres);
                parameters.Add("@Apellidos", e.Apellidos);
                parameters.Add("@CorreoElectronico", e.CorreoElectronico);
                parameters.Add("@Telefono", e.Telefono);
                parameters.Add("@FechaNacimiento", e.FechaNacimiento);
                parameters.Add("@EstadoAfiliacion", e.EstadoAfiliacion);

                using (SqlConnection conn = new SqlConnection(Cadena))
                {
                    string sql = @"UPDATE Pacientes 
                                    SET TipoDocumento = @TipoDocumento, NumeroDocumento = @NumeroDocumento, Nombres = @Nombres
                                    , Apellidos = @Apellidos, CorreoElectronico = @CorreoElectronico, Telefono = @Telefono
                                    , FechaNacimiento = @FechaNacimiento, EstadoAfiliacion = @EstadoAfiliacion
                                    WHERE Id = @Id";
                    await conn.ExecuteAsync(sql, parameters, commandTimeout: 0);
                    return new ApiResponse { Issuccess = true };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { Issuccess = false, Message = ex.Message };
            }
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete([FromQuery] int Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);

                using (SqlConnection conn = new SqlConnection(Cadena))
                {
                    string sql = @"DELETE Pacientes WHERE Id = @Id";
                    await conn.ExecuteAsync(sql, parameters, commandTimeout: 0);
                    return new ApiResponse { Issuccess = true };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { Issuccess = false, Message = ex.Message };
            }
        }
    }
}
