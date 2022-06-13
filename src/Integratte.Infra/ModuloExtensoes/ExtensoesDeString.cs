namespace Integratte.Infra.ModuloExtensoes;

public static class ExtensoesDeString
{
    public static bool NuloOuVazio(this string? texto)
    {
        return string.IsNullOrEmpty(texto);

    }

    public static bool ContemValor(this string? texto)
    {
        return !texto.NuloOuVazio();

    }

    public static string SomenteLetrasOuNumeros(this string texto)
    {
        if (texto.NuloOuVazio()) return "";

        return new string(texto.ToArray().Where(x => char.IsLetterOrDigit(x)).ToArray());

    }

    public static string SomenteNumeros(this string texto)
    {
        if (texto.NuloOuVazio()) return "";

        return new string(texto.ToArray().Where(x => char.IsNumber(x)).ToArray());

    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);

    }

    public static int ToInt32(this string value)
    {
        return Convert.ToInt32(value);

    }

    public static long ToInt64(this string value)
    {
        return Convert.ToInt64(value);

    }

}
