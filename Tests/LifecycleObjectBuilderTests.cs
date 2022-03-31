using NUnit.Framework;
using Core;

namespace Tests;

public class LifecycleObjectBuilderTests {

    private class TestComponent : LifecycleComponent {
        public LifecycleState TestState {get; set;} = LifecycleState.UNINITIALIZED;
        public override void Init() {
            TestState = LifecycleState.INITIALIZED;
        }
        public override void Start() {
            TestState = LifecycleState.STARTED;
        }
    }
    LifecycleObjectBuilder builder;
    [SetUp]
    public void Setup()
    {
        var logger = new Logger();
        var registry = new Dictionary<string, Type>{
            [nameof(TestComponent)] = typeof(TestComponent)
        };
        builder = new LifecycleObjectBuilder(logger, registry);
    }

    [Test]
    public void TestObjectBuilder() {
        var objDef = new LifecycleObjectDefinition {
            Name = "TestObject",
            Components = new string[]{"TestComponent"}
        };
        var obj = builder.Build(objDef);
        obj.Init();
        var component = obj.GetComponent<TestComponent>("TestComponent");
        Assert.That(component.TestState, Is.EqualTo(LifecycleState.INITIALIZED));
    }
}