namespace RavenDBDemo1
{
    internal class Dog : IAnimal
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return base.ToString() + ": " + Name;
        }
    }
}