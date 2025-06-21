using Proyecto_Pastel.Entities;

namespace Proyecto_Pastel.DAOs
{
    public class PostreDAO
    {
        private proyecto_pastelContext db = new();

        public List<postres> ObtenerPostres()
        {
            return db.postres.ToList();
        }

        public postres? ObtenerPorId(int id)
        {
            return db.postres.FirstOrDefault(p => p.id_postre == id);
        }

        public List<postres> ObtenerTodos()
        {
            return db.postres.ToList();
        }

        public void Insertar(postres nuevo)
        {
            db.postres.Add(nuevo);
            db.SaveChanges();
        }
    }
}
