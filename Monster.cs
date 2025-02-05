using System;

public class Monster : Entitiy
{
    public enum MonsterType
    {
        None = 0,
        Slime = 1,
        Goblin = 2,
        Orc = 3
    }
    //몬스터를 잡았을 때 보상으로 제공하는 골드
    public int gold_reward { get; protected set; }
    MonsterType monsterType;

    public Monster(MonsterType type) : base(EntitiyType.Monster)
    {
        this.monsterType = type;
    }
}
//Player의 직업을 생성할 때처럼 
public class Slime : Monster
{
    public Slime() : base(MonsterType.Slime)
    {
        SetInfo(15, 15, 0, 1, "슬라임");
        gold_reward = 10;
        Reset();
    }

}

public class Goblin : Monster
{
    public Goblin() : base(MonsterType.Goblin)
    {
        SetInfo(20, 18, 5, 2, "고블린");
        gold_reward = 20;
        Reset();
    }
}

public class Orc : Monster
{
    public Orc() : base(MonsterType.Orc)
    {
        SetInfo(50, 25, 10, 3, "오크");
        gold_reward = 50;
        Reset();
    }
}
