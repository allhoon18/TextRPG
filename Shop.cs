using System;

public class Shop
{
    //상점에서 파는 아이템을 저장하는 리스트
    public List<Equipment> Equipment_List = new List<Equipment>();

    public Shop()
    {
        //장비를 생성해서 리스트에 저장
        TrainingArmor trainingArmor = new TrainingArmor(Equipment_List);
        CastIronArmor castIronArmor = new CastIronArmor(Equipment_List);
        SpartaArmor spaArmor = new SpartaArmor(Equipment_List);
        OldSword oldSword = new OldSword(Equipment_List);
        BronzeAxe bronzeAxe = new BronzeAxe(Equipment_List);
        SpartaSpear spartaSpear = new SpartaSpear(Equipment_List);
        GMSword gmSword = new GMSword(Equipment_List);
        GMArmor gmArmor = new GMArmor(Equipment_List);
    }

    //저장된 인벤토리 정보를 불러올 때 사용
    //아이템 이름을 검색해서 찾을 수 있도록 함
    public Equipment FindItem(string name)
    {
        Equipment equipment = null;
        //장비 리스트를 돌면서 이름이 같은 장비를 반환
        foreach(Equipment item in Equipment_List)
        {
            if(item.Name == name)
                equipment = item;
        }

        return equipment;
    }

}
