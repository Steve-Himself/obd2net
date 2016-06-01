namespace Obd2Net.InfrastructureContracts
{
    public interface ITest
    {
        bool Available { get; set; }
        bool Incomplete { get; set; }
        string Name { get; set; }
    }
}