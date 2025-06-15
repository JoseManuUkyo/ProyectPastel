using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Proyecto_Pastel.Services
{
    public class UserService
    {
        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public bool ValidarUsuario(string nombre, string contraseña)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            var query = "SELECT COUNT(*) FROM usuarios WHERE nombre = @nombre AND contraseña = @contraseña";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@contraseña", contraseña); // En producción, usa hashing

            var count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }
}
