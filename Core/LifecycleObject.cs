namespace Core;

public enum LifecycleState {
    UNINITIALIZED,
    INITIALIZED,
    STARTED,
    ENDED
}

public sealed class LifecycleObject
{
    private readonly Guid _id;
    public Guid Id => _id;

    private LifecycleState _state;
    public LifecycleState State => _state;
    
    private readonly ILogger _logger;

    private IDictionary<Guid, LifecycleComponent> _components;

    public LifecycleObject(ILogger logger) {
        _logger = logger;
        Init = () => {
            _state = LifecycleState.INITIALIZED;
            _logger.Info("Initialized");
        };
        Tick = () => {_logger.Info("Tick"); };
        Start = () => {
            _state = LifecycleState.STARTED;
            _logger.Info("Started");
        };
        _id = new Guid();
        _components = new Dictionary<Guid, LifecycleComponent>();
        _state = LifecycleState.UNINITIALIZED;
    }

    public void AddComponent(LifecycleComponent component) {
        _components[component.Id] = component;
        Init += component.Init;
        Start += component.Start;
        Tick += component.Tick;
    }
    
    public delegate void OnInitDelegate();
    public OnInitDelegate Init;

    public delegate void OnStartDelegate();
    public OnStartDelegate Start;

    public delegate void OnTickDelegate();
    public OnTickDelegate Tick;

}
