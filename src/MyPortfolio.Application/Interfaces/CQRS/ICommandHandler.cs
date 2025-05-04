namespace MyPortfolio.Application.Interfaces.CQRS
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
} 