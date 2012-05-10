namespace RavenDBDemo1
{
    internal class Monkey : IAnimal
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return base.ToString() + ": " + Name;
        }
    }
}