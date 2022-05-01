namespace Core;

public class LifecycleManager {
    private readonly ILogger _logger;
    private IDictionary<Guid, LifecycleObject> _objects;
    private LifecycleState _globalState;

    public LifecycleManager(ILogger logger) {
        _logger = logger;
        _objects = new Dictionary<Guid, LifecycleObject>();
        _globalState = LifecycleState.UNINITIALIZED;
    }

    public void Init() {
        _globalState = LifecycleState.INITIALIZED;
        foreach(var obj in _objects.Values) {
            obj.Init();
        }
    }

    public void Start() {
        _globalState = LifecycleState.STARTED;
        foreach(var obj in _objects.Values) {
            obj.Start();
        }
    }

    public void Tick() {
        foreach(var obj in _objects.Values) {
            obj.Tick();
        }
    }

    public void AddObject(LifecycleObject obj) {
        _objects[obj.Id] = obj;
        switch(_globalState) {
            case LifecycleState.INITIALIZED:
                obj.Init();
                break;
            case LifecycleState.STARTED:
                obj.Init();
                obj.Start();
                break;
        }
    }

    public void Submit<T>(T _event) where T: EventBase {
        var targetObjects = _event.TargetTags.Any() ? _objects.Values.Where(x => TagIntersection(_event.TargetTags, x.Tags)) : _objects.Values;
        foreach(var obj in targetObjects) {
            obj.Submit(_event);
        }
    }

    private bool TagIntersection(string[] targetTags, string[] comparisonTags) {
        return targetTags.Intersect(comparisonTags).Any();
    }
}