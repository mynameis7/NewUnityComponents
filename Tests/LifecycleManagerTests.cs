using NUnit.Framework;
using Core;
namespace Tests;

public class Tests
{
    LifecycleManager manager;
    LifecycleObjectBuilder builder;
    [SetUp]
    public void Setup()
    {
        var logger = new Logger();
        manager = new LifecycleManager(logger);
        builder = new LifecycleObjectBuilder(logger);
    }

    [Test]
    public void TestBasicLifecycleManagment()
    {
        var objDef = new LifecycleObjectDefinition {
            Name = "TestObject"
        };
        var obj = builder.Build(objDef);
        manager.AddObject(obj);
        manager.Init();
        Assert.That(obj.State, Is.EqualTo(LifecycleState.INITIALIZED));
    }
}
