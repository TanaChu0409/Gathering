using eGathering.Domain.SeedWork;
using System.Linq.Expressions;

namespace eGathering.Persistence.Specifications;

public abstract class Specification<TEntity>
    where TEntity : Entity
{
    private readonly List<Expression<Func<TEntity, object>>> _includeExpressions = new();

    public bool IsSplitQuery { get; protected set; }

    protected Specification(Expression<Func<TEntity, bool>>? criteria) =>
        Criteria = criteria;

    public Expression<Func<TEntity, bool>>? Criteria { get; }

    public IReadOnlyCollection<Expression<Func<TEntity, object>>> IncludeExpressions => _includeExpressions;

    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
        _includeExpressions.Add(includeExpression);

    protected void AddOrderBy(
        Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpression = orderByExpression;

    protected void AddOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpression = orderByDescendingExpression;
}