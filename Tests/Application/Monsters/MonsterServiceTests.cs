using System.Collections.Generic;
using System.Linq;
using DL.Application.Domain.Monsters;
using DL.Application.Monsters;
using DL.Tests.Infrastructure.TestBases;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DL.Tests.Application.Monsters
{
    [TestClass]
    public class MonsterServiceTests
        : IntegratedFor<IMonsterService>
    {
        [TestCleanup]
        public void AfterEach()
        {
            var repository = Resolve<IMonsterRepository>();
            repository.Worker(() => 
            {
                repository.RemoveRange(repository.All());
                repository.SaveChanges();
            });
        }

        [TestMethod]
        public void All_When_AllIsWell()
        {
            Arrange(() =>
            {
                var repository = Resolve<IMonsterRepository>();
                repository.Worker(() => 
                {
                    repository.Add(new Monster 
                    { 
                        Name = "Rawrgnar", 
                        Power = 99,
                        Rewards = new List<Reward>
                        {
                            new Reward { Name = "Gems", Value = 310 }
                        }
                    });
                    repository.SaveChanges();
                });
            });

            var result = Act(() => SUT.All());

            Assert(() => 
            {
                result.Count().Should().Be(1, "id was invliad.");
                result.First().Rewards.Any().Should().BeFalse("rewards should not have been returned.");
            });
        }

        [TestMethod]
        public void Find_When_AllIsWell()
        {
            Arrange(() =>
            {
                var repository = Resolve<IMonsterRepository>();
                repository.Worker(() => 
                {
                    repository.Add(new Monster 
                    { 
                        Name = "Rawrgnar", 
                        Power = 99,
                        Rewards = new List<Reward>
                        {
                            new Reward { Name = "Gems", Value = 310 }
                        }
                    });
                    repository.SaveChanges();
                });
            });

            var result = Act(() => SUT.Find(1));

            Assert(() => 
            {
                result.Id.Should().NotBe(default(int), "id was invliad.");
                result.Rewards.First().Id.Should().NotBe(default(int), "rewards were not returned.");
            });
        }

        [TestMethod]
        public void Find_When_RecordDoesNotExist()
        {
            Arrange(() => {});

            var result = Act(() => SUT.Find(1));

            Assert(() => 
            {
                result.Should().BeNull("result was found");
            });
        }

        [TestMethod]
        public void AddReward_When_AllIsWell()
        {
            int monsterId = 0;
            Arrange(() =>
            {
                var repository = Resolve<IMonsterRepository>();
                repository.Worker(() => 
                {
                    var monster = repository.Add(new Monster 
                    { 
                        Name = "Rawrgnar", 
                        Power = 99
                    });
                    repository.SaveChanges();
                    monsterId = monster.Id;
                });
            });

            var result = Act(() => 
            {
                var reward = new Reward { Name = "Coins", Value = 3 };
                return SUT.AddReward(monsterId, reward);
            });

            Assert(() => 
            {
                result.Id.Should().NotBe(default(int), "id was invliad.");
                result.Rewards.First().Id.Should().NotBe(default(int), "reward did not have an id set");
                result.Rewards.First().Name.Should().Be("Coins", "reward did not have name set");
                result.Rewards.First().Value.Should().Be(3, "reward did not have value set");
            });

        }
    }
}