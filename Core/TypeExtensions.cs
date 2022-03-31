namespace Core;
internal static class TypeExtensions {
    private static IDictionary<Type, IList<Type>> _typeSubclassCache = new Dictionary<Type, IList<Type>>();
    public static IEnumerable<Type> GetSubclasses(this Type type) {
        if(!_typeSubclassCache.ContainsKey(type)) {
            var subclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    from _type in assembly.GetTypes()
                    where _type.IsSubclassOf(type)
                    select _type;
            _typeSubclassCache[type] = subclasses.ToList();
        }

        return _typeSubclassCache[type];
    }
}