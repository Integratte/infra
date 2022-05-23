namespace Integratte.Infra.ModuloExcecoes;

public static class ExtensoesDeException
{
    public static string InnerExceptionRaiz(this Exception ex)
    {
        var innerExceptionMessages = ex.Message;
        Exception? innerExceptionTemp = ex.InnerException;
        while (innerExceptionTemp != null)
        {
            innerExceptionMessages += $" -> {innerExceptionTemp.Message}";
            innerExceptionTemp = innerExceptionTemp.InnerException;

        }

        return innerExceptionMessages;

    }

}
