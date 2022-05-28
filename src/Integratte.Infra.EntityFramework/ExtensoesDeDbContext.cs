using Integratte.Infra.ModuloExcecoesPersonalizadas;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Integratte.Infra.EntityFramework;

public static class ExtensoesDeDbContext
{
    public static DbSet<T> Tabela<T>(this DbContext context) where T : EntidadeDoBancoDeDados
    {
        return context.Set<T>();

    }

    public static void Salvar(this DbContext context)
    {
        context.SaveChanges();

    }

    public static void Adicionar<T>(this DbContext context, T entity) where T : EntidadeDoBancoDeDados
    {
        context.Add(entity);

    }
    public static void AdicionarESalvar<T>(this DbContext context, T entity) where T : EntidadeDoBancoDeDados
    {
        context.Add(entity);
        context.Salvar();

    }

    public static void Atualizar<T>(this DbContext context, T entity) where T : EntidadeDoBancoDeDados
    {
        context.Update(entity);

    }
    public static void AtualizarESalvar<T>(this DbContext context, T entity) where T : EntidadeDoBancoDeDados
    {
        context.Update(entity);
        context.Salvar();

    }

    public static IEnumerable<T> Listar<T>(this DbContext context) where T : EntidadeDoBancoDeDados
    {
        return context.Set<T>().ToList();

    }
    public static IEnumerable<T> FiltrarEListar<T>(this DbContext context, Expression<Func<T, bool>>? filtro = null) where T : EntidadeDoBancoDeDados
    {
        if (filtro == null)
            return context.Set<T>().ToList();

        return context.Set<T>().Where(filtro).ToList();

    }
    public static IEnumerable<T> IncluirEListar<T>(this DbContext context, params Expression<Func<T, object>>[] incluir) where T : EntidadeDoBancoDeDados
    {
        return FiltrarComInclusaoEListar(context, filtro: null, incluir);

    }
    public static IEnumerable<T> FiltrarComInclusaoEListar<T>(this DbContext context, Expression<Func<T, bool>>? filtro, params Expression<Func<T, object>>[] incluir) where T : EntidadeDoBancoDeDados
    {
        if (incluir == null || incluir.Length == 0)
            throw new ErroDeProgramacao("Uso incorreto do método listar do contexto entity framework. É preciso enviar as cláusulas include.");

        var consulta = context.Set<T>().Include(incluir[0]);
        for (int i = 1; i < incluir.Length; i++)
            consulta = consulta.Include(incluir[i]);

        if (filtro == null)
            return consulta.ToList();

        return consulta.Where(filtro).ToList();

    }

    public static T? ObterPorId<T>(this DbContext context, Guid id) where T : EntidadeDoBancoDeDados
    {
        return context.Set<T>().FirstOrDefault(x => x.Id == id);

    }
    public static T? ObterPorId<T>(this DbContext context, Guid id, params Expression<Func<T, object>>[] includes) where T : EntidadeDoBancoDeDados
    {
        if (includes == null || includes.Length == 0)
            throw new ErroDeProgramacao("Uso incorreto do método listar do contexto entity framework. É preciso enviar as cláusulas include.");

        var entity = context.Set<T>().Include(includes[0]);
        for (int i = 1; i < includes.Length; i++)
            entity = entity.Include(includes[i]);

        return entity.FirstOrDefault(x => x.Id == id);

    }

    public static T? Obter<T>(this DbContext context, Expression<Func<T, bool>> filtro) where T : EntidadeDoBancoDeDados
    {
        if (filtro == null)
            throw new ErroDeProgramacao("O filtro do método obter deve ser informado.");

        return context.Set<T>().FirstOrDefault(filtro);

    }
    public static T? Obter<T>(this DbContext context, Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] includes) where T : EntidadeDoBancoDeDados
    {
        if (includes == null || includes.Length == 0 || filtro == null)
            throw new ErroDeProgramacao("Uso incorreto do método obter. É preciso informar o filtro e o includes.");

        var entity = context.Set<T>().Include(includes[0]);
        for (int i = 1; i < includes.Length; i++)
            entity = entity.Include(includes[i]);

        return entity.FirstOrDefault(filtro);

    }

    public static void Remover<T>(this DbContext context, Guid id) where T : EntidadeDoBancoDeDados
    {
        T entity = ObterPorId<T>(context, id) ?? throw new ErroDeProgramacao("Tentativa de exclusão de um objeto não encontrado.");
        Remover(context, entity);

    }
    public static void Remover<T>(this DbContext context, T entity) where T : EntidadeDoBancoDeDados
    {
        context.Remove(entity);

    }

    public static void RemoverESalvar<T>(this DbContext context, Guid id) where T : EntidadeDoBancoDeDados
    {
        T entity = ObterPorId<T>(context, id) ?? throw new ErroDeProgramacao("Tentativa de exclusão de um objeto não encontrado.");
        RemoverESalvar(context, entity);

    }
    public static void RemoverESalvar<T>(this DbContext context, T entity) where T : EntidadeDoBancoDeDados
    {
        Remover(context, entity);
        context.Salvar();

    }

}