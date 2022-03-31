
namespace Core;

public sealed class ComponentContextManager {
    private IDictionary<Type, object> _contextCache = new Dictionary<Type, object>();

    public T BuildComponent<T>() where T: LifecycleComponent {
        return (T)BuildComponent(typeof(T));
    }
    public LifecycleComponent BuildComponent(Type type) {
        var componentContextTypes = type.GetConstructors()
            .First(x => x.GetParameters().Length > 0)
            .GetParameters()
            .Select(x => x.ParameterType);
        foreach(var contextType in componentContextTypes) {
            if(!_contextCache.ContainsKey(contextType)) {
                _contextCache[contextType] = Activator.CreateInstance(contextType);
            }
        }

        var componentContexts = componentContextTypes.Select(x => _contextCache[x]).ToArray();
        return (LifecycleComponent) Activator.CreateInstance(type, componentContexts);
    }
    
}