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

        public void ActualizarPostre(postres postre)
        {
            var existente = db.postres.Find(postre.id_postre);
            if (existente != null)
            {
                existente.cantidad_disponible = postre.cantidad_disponible;
                db.SaveChanges();
            }
        }
        //Esta linea se copia al trabajo final
        public void Insertar(postres nuevo)
        {
            db.postres.Add(nuevo);
            db.SaveChanges();
        }
    }
}