 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Proyecto_Pastel.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            // Si ya está autenticado, redirige
            if (HttpContext.Session.GetString("User") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string connectionString = "server=localhost;user=root;password=Camila2015;database=proyecto_pastel;";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM usuarios WHERE usuario = @username AND contrasena = @password";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@username", Username);
            command.Parameters.AddWithValue("@password", Password);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                HttpContext.Session.SetString("User", Username);
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Usuario o contraseña incorrectos.";
            return Page();
        }
    }
}
