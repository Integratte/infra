namespace Integratte.Infra.ModuloExtensoes;

public static class ExtensoesDeEnum
{
    public static T ConvertToEnum<T>(this Enum valor)
    {
        return (T)Enum.Parse(typeof(T), valor.ToString(), true);

    }

    public static (int id, string nome) ToTuple(this Enum valor)
    {
        return (Convert.ToInt32(valor), valor.ToString());

    }

    public static (int id, string nome)[] ListarTudo<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>()
                            .Select(v => v.ToTuple())
                            .ToArray();
    }

}
