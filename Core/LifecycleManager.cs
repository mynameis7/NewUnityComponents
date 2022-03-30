namespace Core;

public class LifecycleManager {
    private readonly ILogger _logger;
    private IDictionary<Guid, LifecycleObject> _objects;

    public LifecycleManager(ILogger logger) {
        _logger = logger;
        _objects = new Dictionary<Guid, LifecycleObject>();
    }

    public void Start() {
        foreach(var obj in _objects.Values) {
            obj.Init();
        }
    }

    public void Tick() {
        foreach(var obj in _objects.Values) {
            obj.Tick();
        }
    }
}