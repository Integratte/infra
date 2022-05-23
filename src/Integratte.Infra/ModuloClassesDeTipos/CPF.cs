namespace Integratte.Infra.ModuloClassesDeTipos;

public class CPF
{
    private string _cpfRecebido;
    private CPF(string cpf) { _cpfRecebido = cpf; CpfValido = ValidarSeCpfEValido(); }

    public bool CpfValido { get; private set; }
    public bool CpfInvalido => !CpfValido;

    public static CPF Criar(int cpf)
    {
        return new(cpf.ToString().PadLeft(11, '0'));

    }
    public static CPF Criar(string cpf)
    {
        return new(cpf);

    }

    private bool ValidarSeCpfEValido()
    {
        var cpf = _cpfRecebido ?? "";
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf[..9];
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf += digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito += resto.ToString();
        return cpf.EndsWith(digito);

    }

}
