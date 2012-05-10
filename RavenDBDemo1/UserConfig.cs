using System;
using System.Collections.Generic;

namespace RavenDBDemo1
{
    internal class UserConfig
    {
        ICollection<IAnimal> _animals;
        public string Id { get; set; }

        public string PreferredSpecFramework { get; set; }

        public ICollection<IAnimal> Animals
        {
            get { return _animals ?? (_animals = new List<IAnimal>()); }
            set { _animals = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}\tPreferredSpecFramework: {1}\t\t{2}{1}\tAnimals: {1}\t\t{3}", Id,
                                 Environment.NewLine, PreferredSpecFramework,
                                 string.Join(Environment.NewLine + "\t\t", Animals));
        }
    }
}