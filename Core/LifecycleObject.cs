namespace Core;

public enum LifecycleState {
    UNINITIALIZED,
    INITIALIZED,
    STARTED,
    ENDED
}

public record LifecycleObjectDefinition {
    public string Name = "";
    public string[] Components = {};
}

public sealed class LifecycleObjectBuilder {
    private IDictionary<string, LifecycleComponent> _registry;
    private ILogger _logger;
    public LifecycleObjectBuilder(ILogger logger, IDictionary<string, LifecycleComponent> componentRegistry) {
        _registry = componentRegistry;
        _logger = logger;
    }

    public LifecycleObject Build(LifecycleObjectDefinition obj) {
        var instance = new LifecycleObject(_logger);
        instance.AddComponents(obj.Components.Select(x => _registry[x]));
        return instance;
    }
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

    internal void AddComponents(IEnumerable<LifecycleComponent> components) {
        foreach(var component in components) {
            _components[component.Id] = component;
            Init += component.Init;
            Start += component.Start;
            Tick += component.Tick;
        }
    }
    
    public delegate void OnInitDelegate();
    public OnInitDelegate Init;

    public delegate void OnStartDelegate();
    public OnStartDelegate Start;

    public delegate void OnTickDelegate();
    public OnTickDelegate Tick;

}
