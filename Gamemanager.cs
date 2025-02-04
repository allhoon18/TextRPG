public class Gamemanager
{
    Player? player;

    enum Page
    {
        None = 0,
        Start = 1,
        Lobby = 2,
        Town = 3,
        Dungeon = 4,
        Shop = 5,
        Status = 6,
        Inn = 7,
    }

    Page currentPage = Page.Start;

    string name;

    Shop shop = new Shop();

    List<Equipment> Inventory = new List<Equipment>();

    InputKey inputKey = new InputKey();

    Random random = new Random();

    public void GameSystem()
    {
        while (true)
        {
            //PageManager에서 실행하는 내용을 반복
            PageManager();
        }
        
    }

    void PageManager()
    {
        //현재 맵에 해당하는 기능을 사용
        switch (currentPage)
        {
            case Page.Start:
                Process_Start();
                break;
            case Page.Lobby:
                Process_Lobby();
                break;
            case Page.Town:
                Process_Town();
                break;
            case Page.Dungeon:
                Process_Dungeon();
                break;
            case Page.Shop:
                Process_Shop();
                break;
            case Page.Status:
                ShowInfo();
                break;
            case Page.Inn:
                Process_Inn(); 
                break;
            default:
                break;
        }
    }

    void Process_Start()
    {
        //시작화면
        Console.Clear();
        Console.WriteLine("게임 시작하기");
        switch (inputKey.cursor)
        {
            case 0:
                Console.WriteLine("-[1] 예\n[2] 아니오");
                break;
            case 1:
                Console.WriteLine("[1] 예\n-[2] 아니오");
                break;
        }

        inputKey.MoveCursor(1);
        //커서를 이동해, 선택 중인 선택지에 따라 기능을 실행
        if(inputKey.IsSelect)
        {
            switch (inputKey.cursor)
            {
                case 0:
                    //로비로 이동
                    currentPage = Page.Lobby;
                    break;
                case 1:
                    //게임 종료
                    Console.WriteLine("게임을 종료합니다.");
                    Environment.Exit(0);
                    break;
            }

            inputKey.IsSelect = false;
        }

        
    }

    void CreatNewPlayer()
    {
        Console.Clear();
        //이름을 입력
        Console.Write("이름을 입력하세요 : ");
        name = Console.ReadLine();

        do
        {
            Console.Clear();
            Console.WriteLine("원하는 직업을 선택하세요.");

            switch (inputKey.cursor)
            {
                //선택 중인 선택지를 "-"를 이용해 표시
                case 0:
                    Console.WriteLine("-[1] 기사\n[2] 궁수\n[3]시작화면으로 돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 기사\n-[2] 궁수\n[3]시작화면으로 돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 기사\n[2] 궁수\n-[3]시작화면으로 돌아가기");
                    break;
            }

            inputKey.MoveCursor(2);

        } while (!inputKey.IsSelect);

        //선택지에 따른 기능 실행
        if (inputKey.IsSelect)
        {
            switch (inputKey.cursor)
            {
                case 0:
                    //플레이어 직업을 Knight로 선택하여 생성
                    Console.WriteLine("기사를 선택했습니다.");
                    player = new Knight();
                    player.SetName(name);
                    currentPage = Page.Town;
                    player.Reset();
                    break;

                case 1:
                    //플레이어 직업을 Archer로 선택하여 생성
                    Console.WriteLine("궁수를 선택했습니다.");
                    player = new Archer();
                    player.SetName(name);
                    currentPage = Page.Town;
                    player.Reset();
                    break;

                case 2:
                    //시작화면으로 돌아가기
                    currentPage = Page.Start;
                    inputKey.cursor = 0;
                    break;
            }

            inputKey.IsSelect = false;
        }
    }

    void Process_Lobby()
    {
        inputKey.cursor = 0;
        //로비에 진입
        

        do
        {
            Console.Clear();
            Console.WriteLine("<Lobby>");
            Console.WriteLine("로비에 진입했습니다.");

            //이름과 직업 선택 없이 게임을 이어할지, 시작화면으로 돌아갈지 선택
            switch (inputKey.cursor)
            {
                case 0:
                    Console.WriteLine("-[1] 새로 시작하기\n[2] 이어하기\n[3] 저장하기\n[4]시작화면으로 돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 새로 시작하기\n-[2] 이어하기\n[3] 저장하기\n[4]시작화면으로 돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 새로 시작하기\n[2] 이어하기\n-[3] 저장하기\n[4]시작화면으로 돌아가기");
                    break;
                case 3:
                    Console.WriteLine("[1] 새로 시작하기\n[2] 이어하기\n[3] 저장하기\n-[4]시작화면으로 돌아가기");
                    break;
            }

            inputKey.MoveCursor(3);
        } while (!inputKey.IsSelect);


        if (inputKey.IsSelect)
        {
            inputKey.IsSelect = false;

            switch (inputKey.cursor)
            {
                case 0:
                    CreatNewPlayer();
                    break;

                case 1:
                    if(EmptyPlayerCheck())
                    {
                        Console.WriteLine("이전 플레이가 없습니다. 새로운 캐릭터를 생성합니다.");
                        Thread.Sleep(2000);
                        CreatNewPlayer();
                    }
                    else
                    {
                        //마을로 진입
                        GameLoad();
                        Thread.Sleep(3000);
                        currentPage = Page.Town;
                    }
                    
                    break;

                case 2:
                    //게임 저장하기
                    if(player == null)
                    {
                        Console.WriteLine("저장할 내용이 없습니다. 새로운 캐릭터를 생성합니다.");
                        Thread.Sleep(2000);
                        CreatNewPlayer();
                    }
                    else
                      GameSave();

                    break;
                case 3:
                    //시작화면으로 진입
                    inputKey.cursor = 0;
                    currentPage = Page.Start;
                    break;
            }
        }
    }

    void Process_Town()
    {
        //inputKey의 cursor 값을 초기화
        inputKey.cursor = 0;

        //마을로 진입
        do
        {
            Console.Clear();
            Console.WriteLine($"[{player.TypeName} {player.Name}의 모험]");
            Console.WriteLine("마을에 진입했습니다.");

            switch (inputKey.cursor)
            {
                case 0:
                    Console.WriteLine("-[1] 상태창\n[2] 이동\n[3]로비로 돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 상태창\n-[2] 이동\n[3]로비로 돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 상태창\n[2] 이동\n-[3]로비로 돌아가기");
                    break;
            }

            inputKey.MoveCursor(2);

        } while(!inputKey.IsSelect);
        
        if (inputKey.IsSelect)
        {
            inputKey.IsSelect = false;

            switch (inputKey.cursor)
            {
                case 0:
                    //스탯 등 정보를 확인할 수 있는 상태창 보여주기
                    currentPage = Page.Status;
                    break;
                case 1:
                    //던전, 상점 등으로 이동
                    ChangeLocation();
                    break;

                case 2:
                    //로비로 이동
                    currentPage = Page.Lobby;
                    break;
            }

        }
    }

    void ChangeLocation()
    {
        //inputKey의 cursor 값을 초기화
        inputKey.cursor = 0;

        //이동할 장소를 선택
        do
        {
            Console.Clear();
            Console.WriteLine("어디로 갈까?");
            switch (inputKey.cursor)
            {
                case 0:
                    Console.WriteLine("-[1] 던전\n[2] 상점\n[3]여관\n[4]던전\n[5]돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 던전\n-[2] 상점\n[3]여관\n[4]던전\n[5]돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 던전\n[2] 상점\n-[3]여관\n[4]던전\n[5]돌아가기");
                    break;
                case 3:
                    Console.WriteLine("[1] 던전\n[2] 상점\n[3]여관\n-[4]던전\n[5]돌아가기");
                    break;
                case 4:
                    Console.WriteLine("[1] 던전\n[2] 상점\n[3]여관\n[4]던전\n-[5]돌아가기");
                    break;
            }
            inputKey.MoveCursor(4);
        } while (!inputKey.IsSelect);

        if (inputKey.IsSelect)
        {
            inputKey.IsSelect = false;

            switch (inputKey.cursor)
            {
                case 0:
                    //던전으로 이동
                    Console.WriteLine("던전으로 간다.");
                    Thread.Sleep(500);
                    currentPage = Page.Dungeon;
                    break;

                case 1:
                    //상점으로 이동
                    Console.WriteLine("상점으로 간다.");
                    Thread.Sleep(500);
                    currentPage = Page.Shop;
                    break;

                case 2:
                    Console.WriteLine("여관으로 간다.");
                    Thread.Sleep(500);
                    currentPage = Page.Inn;
                    break;

                case 3:
                    Console.WriteLine("던전으로 간다.");
                    Thread.Sleep(500);
                    currentPage = Page.Dungeon;
                    break;

                case 4:
                    //마을로 돌아간다
                    break;

            }
        }
    }

    void Process_Shop()
    {
        //inputKey의 cursor 값을 초기화
        inputKey.cursor = 0;

        //상점으로 진입
        do
        {
            Console.Clear();
            Console.WriteLine("<상점>");
            //보유하고 있는 골드 표기
            Console.WriteLine($"보유 골드 : {player.Gold}G");

            //shop.Equipment_List에 저장된 장비를 표시
            foreach (Equipment a in shop.Equipment_List)
            {
                //현재 선택 중인 장비는 번호로, 선택 중이지 않은 장비는 "-" 표시로 구분
                if (inputKey.cursor == a.Id)
                {
                    Console.Write($"{a.Id + 1}.");
                }
                else
                {
                    Console.Write("- ");
                }

                //장비 이름
                Console.Write($"{a.Name} | ");
                //장비의 종류(Weapon/Armor)에 따라 적용되는 효과가 다르므로 이를 구분하여 표기
                if (a.equipmentType == Equipment.EquipmentType.Weapon)
                {
                    Console.Write($"공력력 +{a.Effect} | ");
                }
                else if (a.equipmentType == Equipment.EquipmentType.Armor)
                {
                    Console.Write($"방어력 +{a.Effect} | ");
                }
                //장비 설명
                Console.Write($"{a.Describe} |");
                //구매가 완료된 장비라면 "구매완료"를 전시하고, 그렇지 않으면 가격을 표시
                if (a.isSold)
                {
                    Console.Write(" 구매완료\n");
                }
                else
                {
                    Console.Write($"{a.Price} G\n");
                }
            }

            //제일 하단에 마을로 귀환하는 선택지를 표시
            if(inputKey.cursor == shop.Equipment_List.Count)
            {
                Console.WriteLine("\n -[마을로 돌아가기]");
            }
            else
            {
                Console.WriteLine("\n [마을로 돌아가기]");
            }

            Console.WriteLine("Enter를 눌러 선택 중인 아이템을 구매");
            Console.WriteLine("구매한 아이템을 다시 선택하면 85% 가격에 판매됩니다.");
            inputKey.MoveCursor(shop.Equipment_List.Count);
        }
        while (!inputKey.IsSelect) ;

        if (inputKey.IsSelect)
        {
            inputKey.IsSelect = false;

            //마을로 돌아가기
            if (inputKey.cursor == shop.Equipment_List.Count)
            {
                currentPage = Page.Town;
                return;
            }

            //선택한 아이템이 아직 구매되지 않았다면 구매 절차로 넘어감
            if (!shop.Equipment_List[inputKey.cursor].isSold)
            {
                //플레이어가 가진 돈이 가격보다 클 경우 구매 가능
                if(player.Gold >= shop.Equipment_List[inputKey.cursor].Price)
                {
                    //가격만큼 플레이어 골드에서 차감
                    player.Gold -= shop.Equipment_List[inputKey.cursor].Price;
                    //아이템의 판매 상태를 참으로 함
                    shop.Equipment_List[inputKey.cursor].isSold = true;
                    //인벤토리에 아이템을 추가
                    Inventory.Add(shop.Equipment_List[inputKey.cursor]);
                    Console.WriteLine($"{shop.Equipment_List[inputKey.cursor].Name}을 구매했습니다.");
                    Thread.Sleep(500);
                }
                //돈이 충분하지 않으므로 구매 불가
                else
                {
                    Console.WriteLine("골드가 충분하지 않습니다.");
                    Thread.Sleep(1000);
                }
                
            }
            //이미 구매된 아이템을 다시 선택할 경우 판매됨
            else
            {
                shop.Equipment_List[inputKey.cursor].isSold = false;
                player.Gold += (int)Math.Round((float)(shop.Equipment_List[inputKey.cursor].Price) * 0.85f);
                Inventory.Remove(shop.Equipment_List[inputKey.cursor]);
                shop.Equipment_List[inputKey.cursor].IsEquipped = false;
                shop.Equipment_List[inputKey.cursor].DisarmEffect(player);
                Console.WriteLine($"{shop.Equipment_List[inputKey.cursor].Name}을 판매했습니다.");
                Thread.Sleep(500);
            }

        }
       
    }

    void ShowInfo()
    {
        //inputKey의 cursor 값을 초기화
        inputKey.cursor = 0;

        do
        {
            Console.Clear();
            //플레이어의 스탯 표시(Health, Attack, Defence)
            //기본 능력치와 증가분을 표기
            Console.WriteLine("<상태창>");
            Console.WriteLine($"[{player.Name}] Lv.{player.Level}");
            Console.WriteLine($"Hp : {player.Health} + {player.Add_Hp}");
            Console.WriteLine($"Atk : {player.Attack} + {player.Add_Atk}");
            Console.WriteLine($"Def : {player.Defence} + {player.Add_Def}");

            //플레이어가 소지한 아이템 표시
            Console.WriteLine("\n <인벤토리>");

            //아이템이 없는 경우
            if (Inventory.Count == 0)
            {
                Console.WriteLine("(비어 있음)");
            }

            //인벤토리에 있는 아이템의 번호 표시를 위한 변수
            int count = 0;

            foreach (var item in Inventory)
            {
                //현재 선택 중인 아이템을 "-"로 표기
                if (count == inputKey.cursor)
                    Console.Write("-");
                //장착 중인 아이템을 "[E]"로 표기
                if (item.IsEquipped)
                {
                    Console.Write("[E] ");
                }
                //인벤토리에 저장된 순서에 따른 아이템의 번호와 이름
                Console.Write($"{count + 1} . {item.Name} | ");
                //아이템의 장착 효과
                if (item.equipmentType == Equipment.EquipmentType.Weapon)
                {
                    Console.Write($"공력력 +{item.Effect} | ");
                }
                else if (item.equipmentType == Equipment.EquipmentType.Armor)
                {
                    Console.Write($"방어력 +{item.Effect} | ");
                }
                //아이템 설명 및 줄바꿈
                Console.Write($"{item.Describe} \n");
                count++;
            }
            //마을로 돌아가기 선택지가 가장 마지막에 위치
            if (inputKey.cursor == Inventory.Count)
            {
                Console.WriteLine("\n -[마을로 돌아가기]");
            }
            else
            {
                Console.WriteLine("\n [마을로 돌아가기]");
            }
            Console.WriteLine("Enter를 눌러 선택 중인 아이템을 장착");
            inputKey.MoveCursor(Inventory.Count);

        } while (!inputKey.IsSelect);

        //Console.WriteLine(inputKey.cursor);

        //커서가 선택되었을 때
        if (inputKey.IsSelect)
        {
            //선택됨 상태를 초기화
            inputKey.IsSelect = false;

            //마을로 돌아가기
            //커서가 가장 마지막 항목인 마을로 돌아가기를 가리킬 경우
            if (inputKey.cursor == Inventory.Count)
            {
                currentPage = Page.Town;
                return;
            }
            //그렇지 않을 경우 선택 중인 아이템에 접근
            //장착하지 않은 상태일 경우 아이템을 장착
            if (!Inventory[inputKey.cursor].IsEquipped)
            {
                //장비 종류에 따라 겹치는 장비를 장착할 수 없게 함
                //장착하려는 장비가 무기일 경우
                if (Inventory[inputKey.cursor].equipmentType == Equipment.EquipmentType.Weapon)
                {
                    foreach (Equipment item in Inventory)
                    {
                        //기존에 장착 중인 장비를 해제
                        if (item.equipmentType == Equipment.EquipmentType.Weapon && item.IsEquipped)
                        {
                            item.IsEquipped = false;
                            item.DisarmEffect(player);
                        }
                    }
                }
                //장착하려는 장비가 갑옷일 경우
                else if (Inventory[inputKey.cursor].equipmentType == Equipment.EquipmentType.Armor)
                {
                    foreach (Equipment item in Inventory)
                    {
                        //기존에 장착 중인 장비를 해제
                        if (item.equipmentType == Equipment.EquipmentType.Armor && item.IsEquipped)
                        {
                            item.IsEquipped = false;
                            item.DisarmEffect(player);
                        }
                    }
                }

                Inventory[inputKey.cursor].IsEquipped = true;
                Inventory[inputKey.cursor].EquipEffect(player);
            }
            //장착하고 있는 상태일 경우 아이템을 해체
            else
            {
                Console.WriteLine($"{Inventory[inputKey.cursor].Name}의 장착을 해제합니다.");
                Inventory[inputKey.cursor].IsEquipped = false;
                Inventory[inputKey.cursor].DisarmEffect(player);
                Thread.Sleep(1000);
            }

            player.Reset();
        }
    }

    void Process_Inn()
    {
        Console.Clear();
        Console.WriteLine("여관에서 휴식한다.");
        if(player.Gold > 500)
        {
            int value = 50;
            player.Heal(value);

            Console.WriteLine("여관에 -500G를 지불하였다.");
            player.Gold -= 500;

            Thread.Sleep(1500);
            currentPage = Page.Town;
        }
        else
        {
            Console.WriteLine("돈이 부족하여 휴식을 취할 수 없다.\n여관에서 휴식하려면 500G가 필요하다.");
            Thread.Sleep(1500);
            currentPage = Page.Town;
        }
 
    }

    void Process_Dungeon()
    {
        inputKey.cursor = 0;

        int limitDef = 0 ;
        string dungeon_Name = "";

        do
        {
            Console.Clear();
            Console.WriteLine("던전에 진입했습니다.");

            switch (inputKey.cursor)
            {
                case 0:
                    Console.WriteLine("-[1] 쉬운 던전 | 방어력 5 이상 권장");
                    Console.WriteLine("[2] 일반 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("[3] 어려운 던전 | 방어력 17 이상 권장");
                    Console.WriteLine("[4] 마을로 돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 쉬운 던전 | 방어력 5 이상 권장");
                    Console.WriteLine("-[2] 일반 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("[3] 어려운 던전 | 방어력 17 이상 권장");
                    Console.WriteLine("[4] 마을로 돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 쉬운 던전 | 방어력 5 이상 권장");
                    Console.WriteLine("[2] 일반 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("-[3] 어려운 던전 | 방어력 17 이상 권장");
                    Console.WriteLine("[4] 마을로 돌아가기");
                    break;
                case 3:
                    Console.WriteLine("[1] 쉬운 던전 | 방어력 5 이상 권장");
                    Console.WriteLine("[2] 일반 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("[3] 어려운 던전 | 방어력 17 이상 권장");
                    Console.WriteLine("-[4] 마을로 돌아가기");
                    break;
            }

            inputKey.MoveCursor(3);

        } while (!inputKey.IsSelect);

        if (inputKey.IsSelect)
        {
            inputKey.IsSelect = false;

            switch (inputKey.cursor)
            {
                case 0:
                    limitDef = 5;
                    dungeon_Name = "쉬운 던전";
                    break;
                case 1:
                    limitDef = 11;
                    dungeon_Name = "일반 던전";
                    break;

                case 2:
                    limitDef = 17;
                    dungeon_Name = "어려운 던전";
                    break;

                case 3:
                    currentPage = Page.Town;
                    break;
            }

        }

        

        if(player.Total_Def <  limitDef)
        {
            int dungeon_clear_chance = random.Next(0,10);
            if(dungeon_clear_chance < 4)
            {
                Console.WriteLine($"{dungeon_Name}을 클리어 했습니다.");
                Dungeon_Success(limitDef);
            }
            else
            {
                Console.WriteLine($"{dungeon_Name}을 클리어 하지 못했습니다.");
                Dungeon_Fail();
            }
        }
        else
        {
            Console.WriteLine($"{dungeon_Name}을 클리어 했습니다.");
            Dungeon_Success(limitDef);
        }
    }

    void Dungeon_Success(int limitDef)
    {
        int damage = random.Next(25, 36);
        damage -= limitDef - player.Total_Def;

        int dungeon_reward = 0;
        int dungeon_bonus = random.Next(player.Total_Atk, player.Total_Atk * 2 + 1);
        float dungeon_bonus_rate = dungeon_bonus * 0.01f;

        switch (limitDef)
        {
            case 5:
                dungeon_reward = 1000;
                break;

            case 11:
                dungeon_reward = 1700;
                break;

            case 17:
                dungeon_reward = 2500;
                break;
        }

        dungeon_reward = (int)(dungeon_reward * (1 + dungeon_bonus_rate));
        Console.WriteLine($"{dungeon_reward}G를 획득했다.");
        player.Gold += dungeon_reward;
        Thread.Sleep(1000);
        currentPage = Page.Town;
    }

    void Dungeon_Fail()
    {
        player.OnDamage(player.Total_Hp / 2);
        Thread.Sleep(1000);
        currentPage = Page.Town;
    }

    void GameSave()
    {
        StreamWriter streamWriter = new StreamWriter("C:\\Users\\Allhoon\\source\\repos\\TextRPG\\TextRPG\\data\\save.txt");
        //플레이어 정보 저장
        streamWriter.WriteLine(player.playerType);
        streamWriter.WriteLine(player.Name);
        streamWriter.WriteLine(player.Level);
        streamWriter.WriteLine(player.Health);
        streamWriter.WriteLine(player.Attack);
        streamWriter.WriteLine(player.Defence);
        streamWriter.WriteLine(player.Gold);

        streamWriter.WriteLine(Inventory.Count);

        //인벤토리 정보 저장
        foreach (Equipment item in Inventory)
        {
            streamWriter.WriteLine(item.Name);
            streamWriter.WriteLine(item.IsEquipped);
        }

        streamWriter.Close();
        Console.WriteLine("게임이 저장되었습니다.");
        inputKey.cursor = 0;
        Thread.Sleep(1000);
    }

    void GameLoad()
    {
        Console.WriteLine("게임 불러오기 시작");

        //저장된 세이브 txt 파일을 불러옴
        StreamReader streamReader = new StreamReader("C:\\Users\\Allhoon\\source\\repos\\TextRPG\\TextRPG\\data\\save.txt");
        //플레이어에 관한 정보를 불러온 것을 담는 배열
        string[] playerData = new string[7];
        Console.WriteLine("플레이어 데이터 초기화");

        int inventoryCount = 0;
        //인벤토리에 관한 정보를 불러온 것을 담는 배열
        string[] inventoryData = Array.Empty<string>();
        Console.WriteLine("인벤토리 데이터 초기화");

        //몇번째 행까지 읽었는지를 저장
        int count = 0;
        //0번째 행의 정보를 불러옴
        string line = streamReader.ReadLine();
        while (line != null)
        {
            //playerData.Length-1까지는 Player에 관한 정보를 저장하고 있으므로 이를 불러옴
            if (count < playerData.Length)
            {
                //읽은 정보를 플레이어 데이터로 저장
                playerData[count] = line;

                //확인한 행 번호를 업데이트
                count++;

                //다음 행을 불러옴
                line = streamReader.ReadLine();
            }
            //playerData.Length의 번호에 인벤토리의 크기를 저장해 두었으므로 이를 불러옴
            else if (count == playerData.Length)
            {
                Console.WriteLine("플레이어 데이터 불러오기 완료");

                //7부터는 인벤토리에 관한 정보, 인벤토리의 크기를 확인
                inventoryCount = int.Parse(line);
                inventoryData = new string[inventoryCount * 2];

                //확인한 행 번호를 업데이트
                count++;

                //다음 행을 불러옴(첫번째로 저장된 장비 이름에 관한 정보)
                line = streamReader.ReadLine();
                
            }
            //playerData.Length+1번부터 인벤토리 크기의 2배(이름과 장착 여부까지 2칸을 각 장비가 사용하므로)까지 저장해두었던 장비 목록을 불러옴
            else if (count > playerData.Length && count < playerData.Length + inventoryCount * 2 && inventoryCount > 0)
            {
                //장비 이름 저장
                inventoryData[count - (playerData.Length+1)] = line;

                //확인한 행 번호를 업데이트
                count++;

                //다음 행을 불러옴(첫번째로 저장된 장비 이름에 관한 정보)
                line = streamReader.ReadLine();

                //장비의 장착 여부를 저장(홀수 번째 정보는 전부 Boolean 형태여야함)
                inventoryData[count - (playerData.Length + 1)] = line;

                //확인한 행 번호를 업데이트
                count++;

                //다음 행을 불러옴(두번째로 저장된 장비 이름에 관한 정보)
                line = streamReader.ReadLine();
            }
            
        }
        //close the file
        streamReader.Close();
        Console.WriteLine("인벤토리 데이터 불러오기 완료");

        LoadPlayer(playerData,inventoryData);
    }

    void LoadPlayer(string[] playerData, string[] inventoryData)
    {
        if (playerData[0] == Player.PlayerType.Knight.ToString())
        {
            player = new Knight();
            player.Name = playerData[1];
            player.Level = int.Parse(playerData[2]);
            player.Health = int.Parse(playerData[3]);
            player.Attack = int.Parse(playerData[4]);
            player.Defence = int.Parse(playerData[5]);
            player.Gold = int.Parse(playerData[6]);
        }
        else if (playerData[0] == Player.PlayerType.Archer.ToString())
        {
            player = new Archer();
            player.Name = playerData[1];
            player.Level = int.Parse(playerData[2]);
            player.Health = int.Parse(playerData[3]);
            player.Attack = int.Parse(playerData[4]);
            player.Defence = int.Parse(playerData[5]);
            player.Gold = int.Parse(playerData[6]);
        }

        Console.WriteLine("플레이어 데이터 적용");

        Inventory.Clear();

        int itemCount = 0;
        foreach(string itemData in inventoryData)
        {
            //짝수번째에는 아이템의 이름, 홀수번째에는 장착 여부가 저장되어 있음
            //(0,1)에 같은 장비에 관한 정보가 저장되어 있고, (2,3)에 같은 구조가 반복...
            //Inventory[0]을 구성하기 위해서는 0번째와 1번째에 있는 정보가 필요-> itemCount/2를 해줌
            if (itemCount % 2 == 0)
            {
                //아이템의 이름으로 전체 장비 목록에서 아이템을 검색하여 추가
                Inventory.Add(shop.FindItem(itemData));
                shop.FindItem(itemData).isSold = true;
            }
            //홀수번째이므로 장착 여부를 판단
            else
            {
                if(itemData == "True")
                {
                    Inventory[itemCount/2].IsEquipped = true;
                    Inventory[itemCount/2].EquipEffect(player);
                }
                else
                {
                    Inventory[itemCount/2].IsEquipped = false;
                }
            }
            
            itemCount++;
        }

        Console.WriteLine("인벤토리 데이터 적용");
    }

    bool EmptyPlayerCheck()
    {
        //저장된 세이브 txt 파일을 불러옴
        StreamReader streamReader = new StreamReader("C:\\Users\\Allhoon\\source\\repos\\TextRPG\\TextRPG\\data\\save.txt");
        string line = streamReader.ReadLine();

        bool isEmpty;

        if (line == null)
            isEmpty = true;
        else
            isEmpty = false;

        streamReader.Close();

        return isEmpty;

        
    }
}

