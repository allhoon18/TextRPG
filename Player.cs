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



