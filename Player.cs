public class Player : Entitiy
{
    public enum PlayerType
    {
        None = 0,
        Knight = 1,
        Archer = 2
    }

    public string? Name { get; set; }
    public int Gold { get; set; } = 10000;
    public int Exp = 0;

    public PlayerType playerType { get; protected set; }

    protected Player(PlayerType type) : base(EntitiyType.Player)
    {
        this.playerType = type;
    }

    public PlayerType GetPlayerType()
    {
        return playerType;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void GetExp(Player player)
    {
        int prior_exp = player.Exp;

        player.Exp++;

        Console.WriteLine($"경험치 획득 ({prior_exp}->{player.Exp})");
        Thread.Sleep(1000);

        if (player.Level == player.Exp)
        {
            LevelUp(player);
            player.Exp = 0;
        }
    }

    public void LevelUp(Player player)
    {
        int prior_level = player.Level;
        int prior_attack = player.Attack;
        int prior_defence = player.Defence;
        player.Level++;
        player.Attack ++;
        player.Defence ++;

        Console.WriteLine($"레벨 업!(Lv.{prior_level}->Lv.{player.Level})");
        Thread.Sleep(1000);
        Console.WriteLine($"공격력 : {prior_attack}->{player.Attack}");
        Thread.Sleep(1000);
        Console.WriteLine($"방어력 : {prior_defence}->{player.Defence}");
        Thread.Sleep(1000);
    }
}

class Knight : Player
{
    public Knight() : base(PlayerType.Knight)
    {
        SetInfo(100, 10, 10, 1, "기사");
    }
}

class Archer : Player
{
    public Archer() : base(PlayerType.Archer)
    {
        SetInfo(75, 15, 5, 1, "궁수");
    }
}



