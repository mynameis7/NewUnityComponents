namespace Core;
public abstract class LifecycleComponent {
    public Guid Id {get;} = new Guid();
    public virtual void Start(){}
    public virtual void Init(){}
    public virtual void Tick(){}
}