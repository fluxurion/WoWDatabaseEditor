using WDE.SqlQueryGenerator;

namespace WDE.QueryGenerators.Base;

internal class QueryGenerator<R> : IQueryGenerator<R>
{
    private IInsertQueryProvider<R>? insertProvider;
    private IDeleteQueryProvider<R>? deleteProvider;
    private IUpdateQueryProvider<R>? updateProvider;
    
    public QueryGenerator(IEnumerable<IInsertQueryProvider<R>> insertProviders,
        IEnumerable<IDeleteQueryProvider<R>> deleteProviders,
        IEnumerable<IUpdateQueryProvider<R>> updateProviders)
    {
        foreach (var p in insertProviders)
            insertProvider = p;
        
        foreach (var p in deleteProviders)
            deleteProvider = p;

        foreach (var p in updateProviders)
            updateProvider = p;

        if (insertProvider == null && deleteProvider == null && updateProvider == null)
            Console.WriteLine("Couldn't find a provider for " + typeof(R));
    }

    public IQuery? Insert(R element) => insertProvider?.Insert(element);
    public IQuery? Delete(R element) => deleteProvider?.Delete(element);
    public IQuery? Update(R element) => updateProvider?.Update(element);

    public string? TableName => insertProvider?.TableName;
}