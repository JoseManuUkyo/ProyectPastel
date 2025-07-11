using Proyecto_Pastel.Entities;

namespace Proyecto_Pastel.DAOs
{
    public class UsuarioDAO
    {
        private proyecto_pastelContext db = new();
        

        public usuarios? ObtenerPorId(int id)
        {
            return db.usuarios.FirstOrDefault(u => u.id_usuario == id);
        }

        public List<usuarios> ObtenerTodos()
        {
            return db.usuarios.ToList();
        }

        public List<usuarios> BuscarPorNombre(string nombre)
        {
            return db.usuarios
                .Where(u => u.nombre.Contains(nombre))
                .ToList();
        }
        //Esta linea se copia en el proyectoFinal
        public void AgregarUsuario(usuarios usuario)
        {
            usuario.fecha_registro = DateTime.Now;
            db.usuarios.Add(usuario);
            db.SaveChanges();
        }
    }
}