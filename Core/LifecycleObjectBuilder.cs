
namespace Core;

public sealed class LifecycleObjectBuilder {
    private IDictionary<string, Type> _registry;
    private ILogger _logger;
        
    public LifecycleObjectBuilder(ILogger logger, IDictionary<string, Type> componentRegistry) {
        _registry = componentRegistry;
        _logger = logger;
    }

    public LifecycleObject Build(LifecycleObjectDefinition obj) {
        var instance = new LifecycleObject(_logger);
        var contextManager = new ComponentContextManager();
        var componentBundle = obj.Components.ToDictionary(x => x, x => contextManager.BuildComponent(_registry[x]));
        instance.AddComponents(componentBundle);
        return instance;
    }
}
