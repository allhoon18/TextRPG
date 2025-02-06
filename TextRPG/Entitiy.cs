abstract public class Entitiy
{
    //플레이어와 몬스터를 구분할 수 있도록 하는 Type 변수
    //(다만 몬스터는 구현하지 못함)
    protected enum EntitiyType
    {
        None = 0,
        Player = 1,
        Monster = 2
    }
    //플레이어와 몬스터가 공유하는 변수
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }
    public int Level { get; set; }
    public string TypeName{ get; set; }
    EntitiyType Type;
    //장비 장착에 따른 스탯 증가치를 저장
    public int Add_Hp;
    public int Add_Atk;
    public int Add_Def;
    //최대체력 및 증가치를 반영한 수치를 저장
    public int Max_Health;
    public int Total_Hp;
    public int Total_Atk;
    public int Total_Def;

    public bool IsDead = false;

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

    //공격력 증가치에 값을 더함
    public void AddAttack(int value)
    {
        Add_Atk += value;
    }
    //방어력 증가치에 값을 더함
    public void AddDefence(int value)
    {
        Add_Def += value;
    }
    //체력 증가치에 값을 더함
    public void AddHelth(int value)
    {
        Add_Hp += value;
    }
    //플레이어 생성이나 장비 장착시에 이후에 전투에서 적용될 Total 스탯으로 종합하여 저장
    public void Reset()
    {
        Total_Hp = Health + Add_Hp;
        Total_Def = Defence + Add_Def;
        Total_Atk = Attack + Add_Atk;
        Max_Health = Max_Health + Add_Hp;
    }

    //체력 회복 기능
    public void Heal(int value)
    {
        //값이 음수일 경우 적용하지 않음
        if (value < 0)
            return;

        //사망 상태를 false로 함
        IsDead = false;

        //기존 체력을 저장
        int currrenHp = Total_Hp;

        //입력된 회복치만큼 회복
        Total_Hp += value;

        //해당 체력이 최대 체력을 초과할 경우 최대 체력으로 맞춤
        if(Total_Hp > Max_Health)
        {
            Total_Hp = Max_Health;
        }
        //Add_Hp를 제외한 Health 값을 저장
        Health = Total_Hp - Add_Hp;

        Console.WriteLine($"체력을 {Total_Hp - currrenHp}만큼 회복했다.");
        Console.WriteLine($"HP : {currrenHp}->{Total_Hp}");
    }

    public void OnDamage(int value)
    {
        //값이 음수일 경우 적용하지 않음
        if (value < 0)
            return;

        //기존 체력을 저장
        int currrenHp = Total_Hp;

        //입력된 데미지만큼 피해
        Total_Hp -= value;

        //체력 값이 음수가 되지 않도록 함
        if (Total_Hp <= 0)
        {
            Total_Hp = 0;
            IsDead = true;
        }
        //Add_Hp를 제외한 Health 값을 저장
        Health = Total_Hp - Add_Hp;
        Console.WriteLine($"{value}만큼의 피해를 입었다.");
        Console.WriteLine($"HP : {currrenHp}->{Total_Hp}");
    }
}


