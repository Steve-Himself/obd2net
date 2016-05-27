namespace Obd2Net
{
    public sealed class Test
    {
        public string Name { get; set; }
        public bool Available { get; set; }
        public bool Incomplete { get; set; }

        public Test(string name, bool available, bool incomplete)
        {
            Name = name;
            Available = available;
            Incomplete = incomplete;
        }

        public override string ToString()
        {
            var a = Available ? "Available" : "Unavailable";
            var c = Incomplete ? "Incomplete" : "Complete";
            return string.Format("Test {0}: {1}, {2}", Name, a, c);
        }
    }
}