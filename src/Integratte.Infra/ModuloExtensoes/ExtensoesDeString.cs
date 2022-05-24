namespace Integratte.Infra.ModuloExtensoes;

public static class ExtensoesDeString
{
    public static bool NuloOuVazio(this string? texto)
    {
        return string.IsNullOrEmpty(texto);

    }

    public static string SomenteLetrasOuNumeros(this string texto)
    {
        return new string(texto.ToArray().Where(x => char.IsLetterOrDigit(x)).ToArray());

    }

    public static string SomenteNumeros(this string texto)
    {
        return new string(texto.ToArray().Where(x => char.IsNumber(x)).ToArray());

    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);

    }

}
