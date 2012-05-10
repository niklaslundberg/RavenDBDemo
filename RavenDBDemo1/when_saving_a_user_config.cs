using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace RavenDBDemo1
{
    [Subject(typeof (UserConfig))]
    public class when_saving_a_user_config
    {
        static UserConfig user_config;
        static DocumentStore store;
        static UserConfig saved_user_config;
        Cleanup cleanup = () => store.Dispose();

        Establish context = () =>
            {
                user_config = new UserConfig
                                  {
                                      Id = "John",
                                      PreferredSpecFramework = "MSpec",
                                      Animals =
                                          new List<IAnimal> {new Monkey {Name = "Tarzan"}, new Dog {Name = "Steve"}}
                                  };

                store = new DocumentStore {ConnectionStringName = "RavenDB"};
                store.Initialize();
            };

        Because of =
            () =>
                {
                    using (var session = store.OpenSession())
                    {
                        session.Store(user_config);
                        session.SaveChanges();
                    }

                    using (var session = store.OpenSession())
                    {
                        saved_user_config = session.Load<UserConfig>("John");
                    }

                    using (var session = store.OpenSession())
                    {
                        query_config =
                            session.Query<UserConfig>().Where(
                                userConfig => userConfig.Animals.Any(animal => animal.Name == "Tarzan"));
                    }

                    Console.WriteLine(saved_user_config);
                    query_config.ToList().ForEach(Console.WriteLine);
                };

        It should_have_two_animals =
            () => saved_user_config.Animals.Count.ShouldEqual(2);

        It should_set_the_preferred_spec_framework_to_mspec =
            () => saved_user_config.PreferredSpecFramework.ShouldEqual("MSpec");
        
        static IEnumerable<UserConfig> query_config;
    }
}