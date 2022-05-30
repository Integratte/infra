namespace Integratte.Infra.ModuloExtensoes;

public static class ExtensoesDeException
{
    public static string TextoAteExceptionRaiz(this Exception ex)
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
