namespace MyPortfolio.Application.Interfaces.CQRS
{
    public interface IQueryHandler<in TQuery, out TResult>
    {
        TResult Handle(TQuery query);
    }
} 