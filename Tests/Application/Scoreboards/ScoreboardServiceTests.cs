using System.Linq;
using DL.Application.Infrastructure;
using DL.Application.Monsters;
using DL.Application.Scoreboards;
using DL.Domain.Monsters;
using DL.Tests.Infrastructure.TestBases;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DL.Tests.Application.Scoreboards
{
    [TestClass]
    public class ScoreboardServiceTests
        : IntegratedFor<IScoreboardService>
    {
        [TestCleanup]
        public void AfterEach()
        {
            var unitOfWork = Resolve<IUnitOfWork>();
            var scoreboardRepository = Resolve<IScoreboardRepository>();
            var monsterRepository = Resolve<IMonsterRepository>();
            unitOfWork.Worker(() => 
            {
                scoreboardRepository.RemoveRange(scoreboardRepository.All());
                monsterRepository.RemoveRange(monsterRepository.All());
                unitOfWork.SaveChanges();
            });
        }

        [TestMethod]
        public void CreateScoreboard_When_AllIsWell()
        {
            var monsterId1 = 0;
            var monsterId2 = 0;

            Arrange(() =>
            {
                var unitOfWork = Resolve<IUnitOfWork>();
                var monsterRepo = Resolve<IMonsterRepository>();
                unitOfWork.Worker(() => 
                {
                    var monster1 = monsterRepo.Add(new Monster { Name = "Rawrgnar", Power = 99 });
                    monsterId1 = monster1.Id;

                    var monster2 = monsterRepo.Add(new Monster { Name = "Grrtastic", Power = 42 });
                    monsterId2 = monster2.Id;

                    unitOfWork.SaveChanges();
                });
            });

            var result = Act(SUT.CreateScoreboard);

            Assert(() => 
            {
                result.ScoreboardEntries.Count.Should().Be(2, "it should have created 2 entries");
                result.ScoreboardEntries.Any(x => x.MonsterId == monsterId1).Should().BeTrue("it did not create an entry for monster 1");
                result.ScoreboardEntries.Any(x => x.MonsterId == monsterId2).Should().BeTrue("it did not create an entry for monster 2");
                result.ScoreboardEntries.All(x => x.PlayersDefeated == 0).Should().BeTrue("they should have initialized as no players defeated");
            });
        }
    }
}