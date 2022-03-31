
namespace Core;

public sealed class LifecycleObjectBuilder {
    private IDictionary<string, Type> _registry;
    private ILogger _logger;
        
    public LifecycleObjectBuilder(ILogger logger) {
        _logger = logger;
        _registry = BuildRegistry();
        foreach(var entry in _registry) {
            Console.WriteLine(entry.Key);
        }
    }

    private IDictionary<string, Type> BuildRegistry() {
        // here's hoping fullname is never null for my usages of this
        return typeof(LifecycleComponent).GetSubclasses().ToDictionary(x => x.FullName!, x => x);
    }

    public LifecycleObject Build(LifecycleObjectDefinition obj) {
        var instance = new LifecycleObject(_logger);
        var contextManager = new ComponentContextManager();
        var componentBundle = obj.Components.ToDictionary(x => x, x => contextManager.BuildComponent(_registry[x]));
        instance.AddComponents(componentBundle);
        return instance;
    }
}
