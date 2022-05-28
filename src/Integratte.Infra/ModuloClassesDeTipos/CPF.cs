using Integratte.Infra.ModuloExtensoes;

namespace Integratte.Infra.ModuloClassesDeTipos;

public class CPF
{
    private readonly string _cpfRecebido;
    private CPF(string cpf) { _cpfRecebido = cpf; Valido = ValidarSeCpfEValido(); }

    public ulong Numero => Convert.ToUInt64(_cpfRecebido);
    public string Texto => ToString();
    public bool Valido { get; private set; }
    public bool Invalido => !Valido;

    public static CPF Criar(ulong cpf)
    {
        return new(cpf.ToString().PadLeft(11, '0'));

    }
    public static CPF Criar(string cpf)
    {
        return new(cpf.SomenteNumeros());

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

    public override string ToString()
    {
        return Numero.ToString(@"000\.000\.000\-00");

    }

    public override bool Equals(object? obj)
    {
        return obj is CPF cpf && Numero == cpf.Numero;

    }

    public static bool operator ==(CPF cpf1, CPF cpf2)
    {
        return cpf1.Equals(cpf2);
    }

    public static bool operator !=(CPF cpf1, CPF cpf2)
    {
        return !cpf1.Equals(cpf2);
    }

    public override int GetHashCode()
    {
        return Convert.ToInt32(_cpfRecebido.Substring(0, _cpfRecebido.Length - 2));

    }

}
