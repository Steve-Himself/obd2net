using Obd2Net.InfrastructureContracts;

namespace Obd2Net
{
    public sealed class Test : ITest
    {
        public Test(string name, bool available, bool incomplete)
        {
            Name = name;
            Available = available;
            Incomplete = incomplete;
        }

        public string Name { get; set; }
        public bool Available { get; set; }
        public bool Incomplete { get; set; }

        public override string ToString()
        {
            var a = Available ? "Available" : "Unavailable";
            var c = Incomplete ? "Incomplete" : "Complete";
            return $"Test {Name}: {a}, {c}";
        }
    }
}