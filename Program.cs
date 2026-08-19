using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Test;
using Test.Enemies;

Console.WriteLine("My test C# rpg game");

Player MainPlayer = new Player();
Entity? Enemy = null;
Start();



void Print(string str)
{
    Console.WriteLine(str);
}

void Start()
{
    Console.WriteLine("Здесь какое-то описание сюжета");
    ChooseAction();
}

ZombieStage GetZombieStageByLevel()
{
    return MainPlayer.Level switch
    {
        1 => ZombieStage.Stage1,
        2 => ZombieStage.Stage1,
        3 => ZombieStage.Stage2,
        4 => ZombieStage.Stage3,
        5 => ZombieStage.Stage4,
        _ => ZombieStage.Stage4,
    };
}

Entity CreateEnemy()
{
    int type = Random.Shared.Next(1, 4);
    return type switch
    {
        1 => new Zombie(GetZombieStageByLevel()),
        2 => new Wolf(),
        3 => new Skeleton(),
        _ => new Wolf(),
    };
}

void ShowStatus(Entity entity)
{
    Print($"===== {entity.Name} =====");
    Print($"Здоровье: {entity.Health}/{entity.MaxHealth}");
    Print($"Атака: {entity.Damage}");
    if(entity.IsAlive == true)
    {
        Print("Статус: жив");
    }
    else
    {
        Print("Статус: мёртв");
    }
    
    string strEffects = "Эффекты: ";
    foreach (Effect effect in entity.Effects)
    {
        switch (effect)
        {
            case Effect.Boost:
                strEffects += "Усиление ";
                break;
            case Effect.Regeneration:
                strEffects += "Регенерация ";
                break;
            case Effect.Poison:
                strEffects += "Отравление ";
                break;
            case Effect.Resistance:
                strEffects += "Сопротивление ";
                break;
            case Effect.Weakness:
                strEffects += "Слабость ";
                break;
        }
        Console.Write(strEffects);
    }
    Print("");
}

void ChooseAction()
{
    while (MainPlayer.IsAlive)
    {
        if(Enemy == null || !Enemy.IsAlive)
        {
            Enemy = CreateEnemy();
        }

        ShowStatus(MainPlayer);
        Print("");
        Print("");
        ShowStatus(Enemy);
        
        Print("0. Закрыть игру");
        Print("1. Атака");
        Print("2. Использовать предмет");
        Print("3. Попытаться сбежать");

        Console.Write(">> ");
        string Choose = Console.ReadLine() ?? "";

        switch (Choose)
        {
            case "0":
                return;

            case "1":
                Battle();
                break;

            case "2":
                UseItem();
                break;

            case "3":
                TryEscape();
                break;

            default:
                break;
        }   

        if (Enemy.IsAlive && !MainPlayer.IsAlive)
        {
        Print($"Вы умерли! Вас убил {Enemy.Name}");
        return;
        }
        else if(!Enemy.IsAlive && !MainPlayer.IsAlive)
        {
            Print($"Вы умерли одновременно с {Enemy.Name}");
            return;
        }
        else if(!Enemy.IsAlive && MainPlayer.IsAlive)
        {
            Print($"Вы победили {Enemy.Name}");
        }
    }
}

void Battle()
{
    MainPlayer.Attack(Enemy);
    Enemy.Attack(MainPlayer);
}

void UseItem()
{
    return;
}

void TryEscape()
{
    return;
}


