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

public sealed class LifecycleObject
{
    private readonly Guid _id;
    public Guid Id => _id;

    private LifecycleState _state;
    public LifecycleState State => _state;
    
    private readonly ILogger _logger;

    private IDictionary<string, LifecycleComponent> _components;

    public LifecycleObject(ILogger logger) {
        _logger = logger;
        _id = new Guid();
        _components = new Dictionary<string, LifecycleComponent>();
        _state = LifecycleState.UNINITIALIZED;
        Init = () => {
            _state = LifecycleState.INITIALIZED;
            _logger.Info("Initialized");
        };
        Tick = () => {_logger.Info("Tick"); };
        Start = () => {
            _state = LifecycleState.STARTED;
            _logger.Info("Started");
        };
    }

    internal void AddComponents(IDictionary<string, LifecycleComponent> components) {
        foreach(var componentKVP in components) {
            var component = componentKVP.Value;
            _components[componentKVP.Key] = component;
            Init += component.Init;
            Start += component.Start;
            Tick += component.Tick;
        }
    }
    public T GetComponent<T>(string componentName) where T: LifecycleComponent {
        return (T)_components[componentName];
    }
    
    public delegate void OnInitDelegate();
    public OnInitDelegate Init;

    public delegate void OnStartDelegate();
    public OnStartDelegate Start;

    public delegate void OnTickDelegate();
    public OnTickDelegate Tick;

}
