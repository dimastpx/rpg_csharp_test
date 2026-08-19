namespace Test.Enemies;

public enum ZombieStage
{
    Stage1, Stage2, Stage3, Stage4
}
public class Zombie(ZombieStage stage)
    : Entity("Зомби", GetByStage(stage)[0], GetByStage(stage)[1], 30)
{
    private static int[] GetByStage(ZombieStage _stage)
    {
        return _stage switch
        {
            ZombieStage.Stage1 => [50, 5],
            ZombieStage.Stage2 => [80, 10],
            ZombieStage.Stage3 => [100, 15],
            ZombieStage.Stage4 => [120, 20],
            _ => [0, 0],
        };
    }
}