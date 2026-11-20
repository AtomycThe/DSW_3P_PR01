using DSW_3P_PR01.Data;
using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Repositories;

public class MisionSQLRepository : IMisionRepository
{
    private readonly AppDbContext _context; // Propieda privada empieza con un guion bajo

    public MisionSQLRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Mision> ObtenerTodas()
    {
        return _context.Misiones.ToList();
    }

    public Mision? ObtenerPorId(int id)
    {
        return _context.Misiones.Find(id);
    }

    public Mision Agregar(Mision mision)
    {
        mision.Id = 0; // Asegurarse de que el Id sea 0 para que se genere automáticamente

        _context.Misiones.Add(mision);
        _context.SaveChanges();

        return mision;
    }

    public bool Actualizar(int id, Mision mision)
    {
        var actual = _context.Misiones.Find(id);
        if(actual == null)
            return false;

        actual.Nombre = mision.Nombre;
        actual.Dificultad = mision.Dificultad;
        actual.RecompensaXp = mision.RecompensaXp;
        actual.Completada = mision.Completada;

        _context.SaveChanges();
        return true;
    }

    public bool Eliminar(int id)
    {
        var mision = _context.Misiones.Find(id);
        if(mision == null)
            return false;

        _context.Misiones.Remove(mision);
        _context.SaveChanges();
        return true;
    }
}