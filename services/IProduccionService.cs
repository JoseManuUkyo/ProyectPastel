namespace Proyecto_Pastel.services
{
    public interface IProduccionService
    {
        string ProducirPostre(int idReceta, int idUsuario, int cantidad);
    }
}