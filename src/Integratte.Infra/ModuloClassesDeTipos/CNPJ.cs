﻿namespace Integratte.Infra.ModuloClassesDeTipos;

public class CNPJ
{
    private readonly string _cnpjRecebido;
    private CNPJ(string cnpj) { _cnpjRecebido = cnpj; CnpjValido = ValidarSeCnpjEValido(); }

    public bool CnpjValido { get; private set; }
    public bool CnpjInvalido => !CnpjValido;

    public static CNPJ Criar(int cnpj)
    {
        return new(cnpj.ToString().PadLeft(14, '0'));

    }
    public static CNPJ Criar(string cnpj)
    {
        return new(cnpj);

    }

    private bool ValidarSeCnpjEValido()
    {
        string CNPJ = _cnpjRecebido?.Replace(".", "") ?? "";
        CNPJ = CNPJ.Replace("/", "");
        CNPJ = CNPJ.Replace("-", "");
        int[] digitos, soma, resultado;
        int nrDig;
        string ftmt;
        bool[] CNPJOk;
        ftmt = "6543298765432";
        digitos = new int[14];
        soma = new int[2];
        soma[0] = 0;
        soma[1] = 0;
        resultado = new int[2];
        resultado[0] = 0;
        resultado[1] = 0;
        CNPJOk = new bool[2];
        CNPJOk[0] = false;
        CNPJOk[1] = false;

        try
        {
            for (nrDig = 0; nrDig < 14; nrDig++)
            {
                digitos[nrDig] = int.Parse(CNPJ.Substring(nrDig, 1));
                if (nrDig <= 11)
                    soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));
                if (nrDig <= 12)
                    soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));

            }

            for (nrDig = 0; nrDig < 2; nrDig++)
            {
                resultado[nrDig] = (soma[nrDig] % 11);
                if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                    CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                else
                    CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));

            }

            return CNPJOk[0] && CNPJOk[1];

        }
        catch { return false; }

    }

}
