using DSW_3P_PR01.Models;
using DSW_3P_PR01.Repositories;

namespace DSW_3P_PR01.Services;

public class MisionService : IMisionService
{
    private readonly IMisionRepository _repository;

    public MisionService(IMisionRepository repository)
    {
        _repository = repository;
    }

    private static void Validar(Mision mision)
    {
        if(string.IsNullOrWhiteSpace(mision.Nombre))
            throw new ArgumentException("Nombre obligatorio");

        if(string.IsNullOrWhiteSpace(mision.Dificultad))
            throw new ArgumentException("Dificultad obligatoria");

        switch (mision.Dificultad)
        {
            case "Facil":
                break;
            case "Media":
                break;
            case "Dificil":
                break;
            default:
                throw new ArgumentException("Dificultad erronea");
        }

        if(mision.RecompensaXp < 0)
            throw new ArgumentException("Xp mayor a 0");

        if(!mision.Completada == false)
            throw new ArgumentException("Mision en no completada");   
    }

    public List<Mision> ObtenerTodas()
    {
        return _repository.ObtenerTodas();
    }

    public Mision? ObtenerPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public Mision Agregar(Mision nueva)
    {
        Validar(nueva);
        return _repository.Agregar(nueva);
    }

    public bool Actualizar(int id, Mision actulizada)
    {
        Validar(actulizada);
        return _repository.Actualizar(id, actulizada);
    }

    public bool Eliminar(int id)
    {
        var mision = _repository.ObtenerPorId(id);

        if(mision == null)
            return false;

        return _repository.Eliminar(id);
    }
}
