using Autofac;
using MediatR;

var builder = new ContainerBuilder();
builder.RegisterType<Mediator>()
    .As<IMediator>()
    .InstancePerLifetimeScope();

builder.Register<ServiceFactory>(context =>
{
    var c = context.Resolve<IComponentContext>();
    return t => c.Resolve(t);
});

using var container = builder.Build();
var mediator = container.Resolve<IMediator>();
var response = await mediator.Send(new PingCommand());
Console.WriteLine($"We got a pong at {response.Timestamp}");


public class PongResponse
{
    public DateTime Timestamp;

    public PongResponse(DateTime timestamp)
    {
        Timestamp = timestamp;
    }
}

public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
{
    public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
    {
        return await Task
            .FromResult(new PongResponse(DateTime.UtcNow))
            .ConfigureAwait(false);
    }
}

public class PingCommand : IRequest<PongResponse>
{
    public string Message { get; set; }
}