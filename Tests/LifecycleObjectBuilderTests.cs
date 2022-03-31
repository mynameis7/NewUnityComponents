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
        builder = new LifecycleObjectBuilder(logger);
    }

    [Test]
    public void TestObjectBuilder() {
        // the + instead of the . means it's a private class
        var fullName = $"{nameof(Tests)}.{nameof(LifecycleObjectBuilderTests)}+{nameof(TestComponent)}";
        var objDef = new LifecycleObjectDefinition {
            Name = "TestObject",
            Components = new string[]{fullName}
        };
        var obj = builder.Build(objDef);
        obj.Init();
        var component = obj.GetComponent<TestComponent>(fullName);
        Assert.That(component.TestState, Is.EqualTo(LifecycleState.INITIALIZED));
    }
}