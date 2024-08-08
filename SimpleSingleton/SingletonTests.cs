using Autofac;
using NUnit.Framework;

[TestFixture]
public class SingletonTests
{
    private IContainer container;

    [SetUp]
    public void SetUp()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<DummyDatabase>()
            .As<IDatabase>()
            .SingleInstance();
        builder.RegisterType<ConfigurableRecordFinder>();
        container = builder.Build();
    }
    
    [Test]
    public void IsSingletonTest()
    {
        var db = container.Resolve<IDatabase>();
        var db2 = container.Resolve<IDatabase>();

        Assert.That(db, Is.SameAs(db2));
    }

    [Test]
    public void SingletonTotalPopulationTest()
    {
        var finder = new SingletonRecordFinder();
        var names = new[] { "Seoul", "Mexico City" };
        var population = finder.TotalPopulation(names);

        Assert.That(population, Is.EqualTo(17_500_000 + 17_400_000));
    }

    [Test]
    public void DependentTotalPopulationTest()
    {
        var finder = container.Resolve<ConfigurableRecordFinder>();

        Assert.That(finder.TotalPopulation(new[] { "alpha", "gamma"}), Is.EqualTo(4));
    }
}