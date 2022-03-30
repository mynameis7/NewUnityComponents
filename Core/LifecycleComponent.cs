namespace Core;
public abstract class LifecycleComponent {
    public virtual void Start(){}
    public virtual void Init(){}
    public virtual void Tick(){}
}