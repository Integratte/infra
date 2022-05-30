namespace Integratte.Infra.ModuloExtensoes;

public static class ExtensoesDeEnum
{
    public static T ConvertToEnum<T>(this Enum valor)
    {
        return (T)Enum.Parse(typeof(T), valor.ToString(), true);

    }

    public static (int Id, string Nome) ToTuple(this Enum valor)
    {
        return (Convert.ToInt32(valor), valor.ToString());

    }

}
