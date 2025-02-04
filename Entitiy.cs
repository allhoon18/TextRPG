abstract public class Entitiy
{
    //
    protected enum EntitiyType
    {
        None = 0,
        Player = 1,
        Monster = 2
    }

    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }
    public int Level { get; set; }
    public string TypeName{ get; set; }
    EntitiyType Type;

    public int Add_Hp;
    public int Add_Atk;
    public int Add_Def;

    public int Max_Health;
    public int Total_Hp;
    public int Total_Atk;
    public int Total_Def;

    protected Entitiy(EntitiyType type)
    {
        this.Type = type;
    }

    protected void SetInfo(int health, int attack, int defence, int level, string typeName)
    {
        this.Health = health;
        this.Attack = attack;
        this.Defence = defence;
        this.Level = level;
        this.TypeName = typeName;
        this.Max_Health = health;

        Add_Hp = 0;
        Add_Atk = 0;
        Add_Def = 0;
    }

    public void AddAttack(int value)
    {
        Add_Atk += value;
    }

    public void AddDefence(int value)
    {
        Add_Def += value;
    }

    public void AddHelth(int value)
    {
        Add_Hp += value;
    }

    public void Reset()
    {
        Total_Hp = Health + Add_Hp;
        Total_Def = Defence + Add_Def;
        Total_Atk = Attack + Add_Atk;
        Max_Health = Total_Hp;
    }

    public void Heal(int value)
    {
        int currrenHp = Total_Hp;

        Total_Hp += value;

        if(Total_Hp > Max_Health)
        {
            Total_Hp = Max_Health;
        }

        Health = Total_Hp - Add_Hp;

        Console.WriteLine($"체력을 {Total_Hp - currrenHp}만큼 회복했다.");
        Console.WriteLine($"{currrenHp}->{Total_Hp}");
    }

    public void OnDamage(int value)
    {
        Total_Hp -= value;
        Console.WriteLine($"{value}만큼의 피해를 입었다.");
    }
}


