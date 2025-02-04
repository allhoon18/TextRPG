using System;
using System.Runtime.CompilerServices;

abstract public class Equipment
{
    public enum EquipmentType
    {
        None = 0,
        Weapon = 1,
        Armor = 2,
    }

    public EquipmentType equipmentType;

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Effect { get; private set; }
    public int Price { get; private set; }
    public bool isSold { get; set; } = false;
    public string Describe { get; private set; }
    public bool IsEquipped { get; set; }
    

    public Equipment(EquipmentType equipmentType)
    {
        this.equipmentType = equipmentType;
        
    }

    public void SetInfo(int id, string name, int effect, int price, List<Equipment> equipment_list)
    {
        this.Id = id;
        this.Name = name;
        this.Effect = effect;
        this.Price = price;
        equipment_list.Insert(id, this);
    }
    public void WriteDescription(string words)
    {
        Describe = words;
    }

    public abstract void EquipEffect(Player player);
    public abstract void DisarmEffect(Player player);
}

public class TrainingArmor : Equipment
{
    public TrainingArmor(List<Equipment> equipment_list) : base(Equipment.EquipmentType.Armor)
    {
        SetInfo(0, "훈련용 갑옷", 5, 1000, equipment_list);
        WriteDescription("수련에 도움을 주는 갑옷입니다.");         
    }

    public override void EquipEffect(Player player)
    {
        player.AddDefence(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddDefence(-Effect);
    }
}
public class CastIronArmor : Equipment
{
    public CastIronArmor(List<Equipment> equipment_list) : base(Equipment.EquipmentType.Armor)
    {
        SetInfo(1, "무쇠 갑옷", 9, 1500, equipment_list);
        WriteDescription("무쇠로 만들어져 튼튼한 갑옷입니다.");

    }

    public override void EquipEffect(Player player)
    {
        player.AddDefence(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddDefence(-Effect);
    }
}
public class SpartaArmor : Equipment
{
    public SpartaArmor(List<Equipment> equipment_list) : base(Equipment.EquipmentType.Armor)
    {
        SetInfo(2, "스파르타의 갑옷", 15, 3500, equipment_list);
        WriteDescription("스파르타의 전사들이 사용했다는 전설의 갑옷입니다.");
    }

    public override void EquipEffect(Player player)
    {
        player.AddDefence(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddDefence(-Effect);
    }
}

public class OldSword : Equipment
{
    public OldSword(List<Equipment> equipment_list) : base (Equipment.EquipmentType.Weapon)
    {
        SetInfo(3, "낡은 검", 2, 600, equipment_list);
        WriteDescription("쉽게 볼 수 있는 낡은 검 입니다.");
    }

    public override void EquipEffect(Player player)
    {
        player.AddAttack(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddAttack(-Effect);
    }
}

public class BronzeAxe : Equipment
{
    public BronzeAxe(List<Equipment> equipment_list) : base(Equipment.EquipmentType.Weapon)
    {
        SetInfo(4, "청동 도끼", 5, 1500, equipment_list);
        WriteDescription("어디선가 사용됐던거 같은 도끼입니다.");
    }

    public override void EquipEffect(Player player)
    {
        player.AddAttack(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddAttack(-Effect);
    }
}

public class SpartaSpear : Equipment
{
    public SpartaSpear(List<Equipment> equipment_list) : base(Equipment.EquipmentType.Weapon)
    {
        SetInfo(5, "스파르타의 창", 7, 2400, equipment_list);
        WriteDescription("스파르타의 전사들이 사용했다는 전설의 창입니다.");
    }

    public override void EquipEffect(Player player)
    {
        player.AddAttack(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddAttack(-Effect);
    }

}

public class GMSword : Equipment
{
    public GMSword(List<Equipment> equipment_list) : base(Equipment.EquipmentType.Weapon)
    {
        SetInfo(6, "운영자의 검", 9999, 99999, equipment_list);
        WriteDescription("정기점검, 임시점검, 긴급점검, 연장점검의 힘을 합친 명검.");
    }

    public override void EquipEffect(Player player)
    {
        player.AddAttack(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddAttack(-Effect);
    }
}

public class GMArmor : Equipment
{
    public GMArmor(List<Equipment> equipment_list) : base(Equipment.EquipmentType.Armor)
    {
        SetInfo(7, "운영자의 갑옷", 9999, 99999, equipment_list);
        WriteDescription("확률을 조작해 피해를 입지 않는 갑옷.");
    }

    public override void EquipEffect(Player player)
    {
        player.AddAttack(Effect);
    }
    public override void DisarmEffect(Player player)
    {
        player.AddAttack(-Effect);
    }

}
