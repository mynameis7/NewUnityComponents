namespace Core;
public abstract class LifecycleComponent {
    public Guid Id {get;} = new Guid();
    public abstract void Start();
    public abstract void Init();
    public abstract void Tick();
}