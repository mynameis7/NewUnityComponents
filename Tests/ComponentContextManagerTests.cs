using NUnit.Framework;
using Core;

namespace Tests;

public class ComponentContextManagerTests {
    
    private class TestComponentContext {
        public LifecycleState State {get; set;} = LifecycleState.UNINITIALIZED; 
    }
    private class TestComponent : LifecycleComponent {
        private readonly TestComponentContext _componentContext;
        public TestComponent(TestComponentContext componentContext) {
            _componentContext = componentContext;
        }
        public override void Init() {
            _componentContext.State = LifecycleState.INITIALIZED;
        }
        public override void Start() {
            _componentContext.State = LifecycleState.STARTED;
        }

        public LifecycleState GetState() {
            return _componentContext.State;
        }
    }

    [Test]
    public void TestComponentBuilding() {
        var manager = new ComponentContextManager();
        var component = manager.BuildComponent<TestComponent>();
        component.Init();
        Assert.That(component.GetState(), Is.EqualTo(LifecycleState.INITIALIZED));
    }

}