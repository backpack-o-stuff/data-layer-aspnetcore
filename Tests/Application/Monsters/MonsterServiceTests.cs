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
        public void Find_When_AllIsWell()
        {
            Arrange(() =>
            {
                var repository = Resolve<IMonsterRepository>();
                repository.Worker(() => 
                {
                    repository.Add(new Monster { Name = "Rawrgnar", Power = 99 });
                    repository.SaveChanges();
                });
            });

            var result = Act(() => SUT.Find(1));

            Assert(() => 
            {
                result.Id.Should().Be(1, "id was invliad.");
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
    }
}