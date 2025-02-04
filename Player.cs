public class Player : Entitiy
{
    //플레이어의 직업 종류를 구분
    public enum PlayerType
    {
        None = 0,
        Knight = 1,
        Archer = 2
    }
    //Entity에서 공통적이지 않고 플레이어에서만 쓰이는 변수
    public string Name { get; set; }
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
    //플레이어 이름을 저장
    public void SetName(string name)
    {
        Name = name;
    }
    //경험치 획득
    public void GetExp(Player player)
    {
        //이전 경험치 확인
        int prior_exp = player.Exp;
        //경험치 획득
        player.Exp++;

        Console.WriteLine($"경험치 획득 ({prior_exp}->{player.Exp})");
        Thread.Sleep(1000);
        //레벨만큼의 경험치를 획득하였을 때 레벨업
        if (player.Level == player.Exp)
        {
            LevelUp(player);
            //경험치를 초기화
            player.Exp = 0;
        }
    }

    public void LevelUp(Player player)
    {
        //레벨업 이전의 값을 저장
        int prior_level = player.Level;
        int prior_attack = player.Attack;
        int prior_defence = player.Defence;
        //레벨업을 통한 상승치를 적용
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
//각 클래스로 플레이어 직업을 저장
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



