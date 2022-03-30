namespace Core;
public sealed class LifecycleObject
{
    private readonly Guid _id;
    public Guid Id => _id;
    
    private readonly ILogger _logger;

    public LifecycleObject(ILogger logger) {
        _logger = logger;
        Init = () => {_logger.Info("Initialized"); };
        Tick = () => {_logger.Info("Tick"); };
        _id = new Guid();
    }
    
    public delegate void OnInitDelegate();
    public OnInitDelegate Init;

    public delegate void OnTickDelegate();
    public OnTickDelegate Tick;

}
