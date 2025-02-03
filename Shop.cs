using System;

public class Shop
{
	public Equipment[] shopItems = new Equipment[0];
    public List<Equipment> Equipment_List = new List<Equipment>();

    public Shop()
    {
        TrainingArmor trainingArmor = new TrainingArmor(Equipment_List);
        CastIronArmor castIronArmor = new CastIronArmor(Equipment_List);
        SpartaArmor spaArmor = new SpartaArmor(Equipment_List);
        OldSword oldSword = new OldSword(Equipment_List);
        BronzeAxe bronzeAxe = new BronzeAxe(Equipment_List);
        SpartaSpear spartaSpear = new SpartaSpear(Equipment_List);
    }

}
