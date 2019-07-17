# data-layer-dotnetcore

Data Layer practices and helpers in ASP.NET Core.

Focus of this repository is to highlight the Data layer and how the others interact with it. Other layers may/will contain shortcuts.

This repository shows the Data Layer in a soft [onion architecture](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/), but is by no means restricted to that.

---

## Setup

#### EntityFramework Setup

- Command line > Run: dotnet ef database update --project Data

#### Built With

- ASP.NET Core 2.2
- EntityFrameworkCore
- SQLite (For simplicity sake)

---

## Intent

#### Database Bleedthrough

While some databases/orms will force their needs to bleed out into the domain entities/models, this example highlights how controlling the data layer is to keep db specifics contained within.

#### Where Is The Query Run?

RepositoryBase deals in IQueryable while the Repository implementations ensure that the query has been run and return Lists.

#### UnitOfWork Use

The services in the application layer coordinates the unit of work, allowing the repositories to work together. Underneith the ContextSessionProvider is controlling the DbContext sharing.

```
Application/Scoreboards/ScoreboardService

_unitOfWork.Worker(() => 
{
    scoreboard = new Scoreboard();
    scoreboard = _scoreboardRepository.Add(scoreboard);
    _unitOfWork.SaveChanges();

    var monsters = _monsterRepository.All();
    monsters.ForEach(monster => 
    {
        scoreboard.ScoreboardEntries.Add(new ScoreboardEntry
        {
            MonsterId = monster.Id,
            PlayersDefeated = 0
        });
    });
    scoreboard = _scoreboardRepository.Update(scoreboard);

    _unitOfWork.SaveChanges();
});
```

#### Entity Children Fetching

By default this will not return an entities children, there are fetching overloads on the base repositories to help with this.

##### No Children
```
public Monster Find(int id)
{
    return FindBy<Monster>(selectBy => selectBy.Id == id);
}
```

##### With Children
```
public Monster FindComplete(int id)
{
    return FindBy<Monster>(
        selectBy => selectBy.Id == id,
        includeChildren => includeChildren.Rewards);
}
```
