using System.Runtime.Intrinsics.Arm;

public class Gamemanager
{
    //플레이어 저장
    Player? player;
    //페이지 목록
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
    //현재 페이지 표시
    Page currentPage = Page.Start;
    //상점 클래스
    Shop shop = new Shop();
    //소지한 장비를 저장하는 리스트
    List<Equipment> Inventory = new List<Equipment>();
    //키 입력을 처리하는 클래스
    InputKey inputKey = new InputKey();
    //무작위 값 생성을 위한 클래스
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
        //현재 맵에 해당하는 매서드 사용
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
        //새로운 플레이어를 생성

        Console.Clear();
        //이름을 입력
        Console.Write("이름을 입력하세요 : ");
        string name = Console.ReadLine();

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
        //커서 위치 초기화
        inputKey.cursor = 0;
        //로비에 진입

        do
        {
            Console.Clear();
            Console.WriteLine("<Lobby>");
            Console.WriteLine("로비에 진입했습니다.");

            //새로운 캐릭터 생성 또는 저장된 게임을 이어할지, 시작화면으로 돌아갈지 선택
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
                    //새로운 플레이어를 생성
                    CreatNewPlayer();
                    break;

                case 1:
                    //저장된 플레이어를 불러옴
                    if(EmptyPlayerCheck())
                    {
                        //세이브 파일이 비어있을 경우 새로운 캐릭터를 생성
                        Console.WriteLine("이전 플레이가 없습니다. 새로운 캐릭터를 생성합니다.");
                        Thread.Sleep(2000);
                        CreatNewPlayer();
                    }
                    else
                    {
                        //세이브 파일을 불러옴
                        GameLoad();
                        player.Reset();
                        Thread.Sleep(1000);
                        //마을로 진입
                        currentPage = Page.Town;
                    }
                    
                    break;

                case 2:
                    //게임 저장하기
                    //플레이어가 비어있을 경우
                    if(player != null)
                    {
                        //현재 플레이를 저장
                        GameSave();
                    }
                    else
                    {
                        //플레이어는 비어있으나 세이브가 있을 경우
                        if (!EmptyPlayerCheck())
                        {
                            //저장된 플레이어를 불러옴
                            GameLoad();
                            //불러온 플레이어를 저장
                            GameSave();
                        }
                        else
                        {
                            //플레이어가 없고, 저장된 내용도 없는 경우 새로운 플레이어를 생성
                            Console.WriteLine("저장할 내용이 없습니다. 새로운 캐릭터를 생성합니다.");
                            Thread.Sleep(1000);
                            CreatNewPlayer();
                        }
                    }
                    break;

                case 3:
                    //커서 위치를 초기화
                    inputKey.cursor = 0;
                    //시작화면으로 진입
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
            //직업과 이름을 표시
            Console.WriteLine($"[{player.TypeName} {player.Name}의 모험]");
            Console.WriteLine("마을에 진입했습니다.");
            //마을에서 선택 가능한 선택지: 상태창, 이동, 로비
            switch (inputKey.cursor)
            {
                case 0:
                    Console.WriteLine("-[1] 상태창&인벤토리\n[2] 이동\n[3]로비로 돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 상태창&인벤토리\n-[2] 이동\n[3]로비로 돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 상태창&인벤토리\n[2] 이동\n-[3]로비로 돌아가기");
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
                    Console.WriteLine("-[1] 던전\n[2] 상점\n[3]여관\n[4]돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 던전\n-[2] 상점\n[3]여관\n[4]돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 던전\n[2] 상점\n-[3]여관\n[4]돌아가기");
                    break;
                case 3:
                    Console.WriteLine("[1] 던전\n[2] 상점\n[3]여관\n-[4]돌아가기");
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
                    //여관으로 이동
                    Console.WriteLine("여관으로 간다.");
                    Thread.Sleep(500);
                    currentPage = Page.Inn;
                    break;

                case 3:
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
            //커서가 Equipment_List 범위 내에서만 움직일 수 있도록 함
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
                    player.ChangeGold(-shop.Equipment_List[inputKey.cursor].Price);
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
                //선택된 아이템을 판매 상태를 false로 함
                shop.Equipment_List[inputKey.cursor].isSold = false;
                //가격의 85% 가격으로 판매됨
                player.ChangeGold((int)(shop.Equipment_List[inputKey.cursor].Price * 0.85f));
                //인벤토리에서 아이템을 제거
                Inventory.Remove(shop.Equipment_List[inputKey.cursor]);
                //아이템이 장착 상태라면 장착 상태를 해제하고 장착시 효과를 제거
                if(shop.Equipment_List[inputKey.cursor].IsEquipped)
                {
                    shop.Equipment_List[inputKey.cursor].IsEquipped = false;
                    shop.Equipment_List[inputKey.cursor].DisarmEffect(player);
                }
                
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
        //여관 사용에 요구되는 골드를 소지하고 있는지 확인
        if(player.Gold > 500)
        {
            //최대 체력의 절반만큼 회복
            player.Heal(player.Max_Health / 2);
            Thread.Sleep(1500);
            Console.WriteLine("여관에 500G를 지불하였다.");
            player.ChangeGold(-500);

            Thread.Sleep(1500);
            currentPage = Page.Town;
        }
        else
        {
            //골드가 충분하지 않을 경우
            Console.WriteLine("돈이 부족하여 휴식을 취할 수 없다.\n여관에서 휴식하려면 500G가 필요하다.");
            Thread.Sleep(1500);
            currentPage = Page.Town;
        }
 
    }

    void Process_Dungeon()
    {
        inputKey.cursor = 0;

        //던전을 구분하는 변수인 limitDef(제한 방어력)와 던전 이름
        int limitDef = 0 ;
        string dungeon_Name = "";

        do
        {
            Console.Clear();
            Console.WriteLine("던전에 진입했습니다.");

            switch (inputKey.cursor)
            {
                case 0:
                    Console.WriteLine("-[1] 쉬운 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("[2] 일반 던전 | 방어력 16 이상 권장");
                    Console.WriteLine("[3] 어려운 던전 | 방어력 21 이상 권장");
                    Console.WriteLine("[4] 마을로 돌아가기");
                    break;
                case 1:
                    Console.WriteLine("[1] 쉬운 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("-[2] 일반 던전 | 방어력 16 이상 권장");
                    Console.WriteLine("[3] 어려운 던전 | 방어력 21 이상 권장");
                    Console.WriteLine("[4] 마을로 돌아가기");
                    break;
                case 2:
                    Console.WriteLine("[1] 쉬운 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("[2] 일반 던전 | 방어력 16 이상 권장");
                    Console.WriteLine("-[3] 어려운 던전 | 방어력 21 이상 권장");
                    Console.WriteLine("[4] 마을로 돌아가기");
                    break;
                case 3:
                    Console.WriteLine("[1] 쉬운 던전 | 방어력 11 이상 권장");
                    Console.WriteLine("[2] 일반 던전 | 방어력 16 이상 권장");
                    Console.WriteLine("[3] 어려운 던전 | 방어력 21 이상 권장");
                    Console.WriteLine("-[4] 마을로 돌아가기");
                    break;
            }

            inputKey.MoveCursor(3);

        } while (!inputKey.IsSelect);

        if (inputKey.IsSelect)
        {
            inputKey.IsSelect = false;

            //각 던전에 해당하는 정보를 저장
            switch (inputKey.cursor)
            {
                case 0:
                    limitDef = 11;
                    dungeon_Name = "쉬운 던전";
                    break;
                case 1:
                    limitDef = 16;
                    dungeon_Name = "일반 던전";
                    break;

                case 2:
                    limitDef = 21;
                    dungeon_Name = "어려운 던전";
                    break;

                case 3:
                    currentPage = Page.Town;
                    break;
            }

        }
        //마을을 선택했을 경우 limitDef가 0이므로 아래 과정을 거치지 않음
        if (limitDef > 0)
        {
            if(player.IsDead)
            {
                Console.WriteLine($"{player.TypeName} {player.Name}는 싸울 수 없다.\n여관에서 회복하자.");
                currentPage = Page.Inn;
                return;
            }

            //플레이어의 방어력이 제한 방어력 이상인지 이하인지 검사
            if (player.Total_Def < limitDef)
            {
                //제한 방어력에 미치지 못할 경우
                //0~9 사이의 숫자를 무작위로 생성
                int dungeon_clear_chance = random.Next(0, 10);
                //40% 확률로 던전을 클리어
                if (dungeon_clear_chance < 4)
                {
                    Console.WriteLine($"{dungeon_Name}을 클리어 했습니다.");
                    Dungeon_Success(limitDef);
                }
                //60% 확률로 실패->단순히 실패하는 것에서 몬스터와 전투하는 것으로 변경
                else
                {
                    //Console.WriteLine($"{dungeon_Name}을 클리어 하지 못했습니다.");
                    //Dungeon_Fail();
                    //난이도에 따른 전투 횟수를 지정
                    int battle_times = 0;
                    switch (limitDef)
                    {
                        case 11:
                            battle_times = 1;
                            break;

                        case 16:
                            battle_times = 3;
                            break;

                        case 21:
                            battle_times = 5;
                            break;
                    }
                    //전투 횟수를 측정
                    int battleCount = 0;
                    //전투 횟수를 충족할 때까지 또는 플레이어가 사망할 때까지 반복
                    while(battleCount < battle_times && !player.IsDead)
                    {
                        Console.Clear();
                        //현재 몇번째 전투인지 표시
                        Console.WriteLine($"{battleCount+1}번째 전투");
                        //던전 난이도에 따른 몬스터 생성
                        Monster summon_monster = Summon_Monster(limitDef);
                        //몬스터와 전투
                        Battle(player, summon_monster);
                        battleCount++;
                    }
                    //모든 전투를 죽지 않고 통과한 경우 클리어
                    if(!player.IsDead)
                    {
                        Console.WriteLine($"{dungeon_Name}을 클리어 했습니다.");
                        Dungeon_Success(limitDef);
                    }
                    //그렇지 못한 경우 클리어 하지 못함
                    else
                    {
                        Console.WriteLine($"{dungeon_Name}을 클리어 하지 못했습니다.");
                        Thread.Sleep(2000);
                    }
                }
            }
            //제한 방어력을 달성한 경우 100% 확률로 성공
            else
            {
                Console.WriteLine($"{dungeon_Name}을 클리어 했습니다.");
                Dungeon_Success(limitDef);
            }
        }
    }

    void Dungeon_Success(int limitDef)
    {
        //던전 클리어를 성공했을 경우
        //25~35 사이의 무작위 값을 생성
        int damage = random.Next(25, 36);
        //제한 방어력과의 초과분만큼 가해지는 데미지를 차감
        damage -= limitDef - player.Total_Def;

        //보상은 공격력에 따라 증가
        int dungeon_reward = 0;
        //공격력~공격력 * 2% 만큼 보상이 증가
        int dungeon_bonus = random.Next(player.Total_Atk, player.Total_Atk * 2 + 1);
        //공격력~공격력 * 2에서 정해진 숫자만큼 증가율을 계산 
        float dungeon_bonus_rate = dungeon_bonus * 0.01f;
        //제한 방어력에 따라 던전 레벨이 구분되므로 이에 따라 보상을 결정
        switch (limitDef)
        {
            case 11:
                dungeon_reward = 1000;
                break;

            case 16:
                dungeon_reward = 1700;
                break;

            case 21:
                dungeon_reward = 2500;
                break;
        }
        //결정된 보상과 증가율에 따라 최종 보상을 결정
        dungeon_reward = (int)(dungeon_reward * (1 + dungeon_bonus_rate));
        Console.WriteLine($"{dungeon_reward}G를 획득했다.");
        //플레이어에게 보상을 적용
        player.ChangeGold(dungeon_reward);
        Thread.Sleep(1000);
        //성공 시에 경험치 부여
        player.GetExp(player);
    }

    void Dungeon_Fail()
    {
        //실패했을 경우 최대 체력의 절반의 피해를 입음
        player.OnDamage(player.Max_Health / 2);
        Thread.Sleep(1000);
    }

    Monster Summon_Monster(int limitDef)
    {
        Monster monster = null;
        //어떤 몬스터를 소환할 지 무작위로 결정
        int spawn_rate = random.Next(0, 10);
        int Slime_rate = 0;
        int Goblin_rate = 0;

        switch(limitDef)
        {
            case 5:
                //spawn_rate 값이 7 미만일 때 슬라임을 소환
                Slime_rate = 7;
                //spawn_rate 값이 10(7+3) 미만일 때 고블린을 소환
                Goblin_rate = 3;
                break;

            case 11:
                //spawn_rate 값이 4 미만일 때 슬라임을 소환
                Slime_rate = 4;
                //spawn_rate 값이 9(4+5) 미만일 때 고블린을 소환
                Goblin_rate = 5;
                //그 외의 경우인 9일 때 오크를 소환
                break;

            case 17:
                //spawn_rate 값이 2 미만일 때 슬라임을 소환
                Slime_rate = 2;
                //spawn_rate 값이 8(2+6) 미만일 때 고블린을 소환
                Goblin_rate = 6;
                //그 외의 경우인 8,9일 때 오크를 소환
                break;
        }

        //스폰 확률에 따른 몬스터 생성
        if(spawn_rate < Slime_rate)
        {
            monster = new Slime();
        }
        else if(spawn_rate < Slime_rate + Goblin_rate)
        {
            monster = new Goblin();
        }
        else
        {
            monster = new Orc();
        }
        //생성한 몬스터를 반환
        return monster;
    }

    void Battle(Player player, Monster monster)
    {
        //몬스더와의 전투
        Console.WriteLine($"{monster.TypeName}이 나타났다!");
        //어느 한쪽이 죽을 때까지 반복
        while(!player.IsDead && !monster.IsDead)
        {
            //플레이어의 공격
            Console.WriteLine($"\n{player.TypeName} {player.Name}의 공격");
            //공격력-방어력 수치만큼 데미지를 줌
            monster.OnDamage(player.Total_Atk - monster.Total_Def);
            Thread.Sleep(1000);
            //몬스터 사망시
            if(monster.IsDead)
            {
                Console.WriteLine($"\n{monster.TypeName}을 잡았다.");
                Console.WriteLine($"{monster.gold_reward}G를 획득했다.");
                player.ChangeGold(monster.gold_reward);
                Thread.Sleep(1000);
                break;
            }

            //몬스터의 공격
            Console.WriteLine($"\n{monster.TypeName}의 공격");
            player.OnDamage(monster.Total_Atk - player.Total_Def);
            Thread.Sleep(1000);
            //플레이어 사망시
            if (player.IsDead)
            {
                Console.WriteLine($"{player.TypeName} {player.Name}이 쓰러졌다.");
                Console.WriteLine($"{monster.gold_reward}G를 잃었다.");
                player.ChangeGold(-monster.gold_reward);
                Thread.Sleep(1000);
                break;
            }
        }
    }

    void GameSave()
    {
        //게임 저장을 위해 save.txt 파일을 불러옴
        StreamWriter streamWriter = new StreamWriter("C:\\Users\\Allhoon\\source\\repos\\TextRPG\\TextRPG\\data\\save.txt");
        //플레이어 정보 저장
        streamWriter.WriteLine(player.playerType);
        streamWriter.WriteLine(player.Name);
        streamWriter.WriteLine(player.Level);
        streamWriter.WriteLine(player.Health);
        streamWriter.WriteLine(player.Max_Health);
        streamWriter.WriteLine(player.Attack);
        streamWriter.WriteLine(player.Defence);
        streamWriter.WriteLine(player.Gold);
        //인벤토리의 크기를 저장
        streamWriter.WriteLine(Inventory.Count);
        //인벤토리 정보 저장
        foreach (Equipment item in Inventory)
        {
            //인벤토리에 있는 아이템의 이름과 장착 여부를 저장
            streamWriter.WriteLine(item.Name);
            streamWriter.WriteLine(item.IsEquipped);
        }
        //save.txt 작성을 종료
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
        string[] playerData = new string[8];
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
        //save.txt 불러오기를 종료
        streamReader.Close();
        Console.WriteLine("인벤토리 데이터 불러오기 완료");
        //받아온 플레이어 데이터와 인벤토리 데이터에 따라 플레이어와 인벤토리를 구성
        LoadPlayer(playerData,inventoryData);
    }

    void LoadPlayer(string[] playerData, string[] inventoryData)
    {
        //save.txt 파일의 첫 행에 저장된 직업에 맞춰 플레이어를 새로 생성
        if (playerData[0] == Player.PlayerType.Knight.ToString())
        {
            player = new Knight();
            player.Name = playerData[1];
            player.Level = int.Parse(playerData[2]);
            player.Health = int.Parse(playerData[3]);
            player.Max_Health = int.Parse(playerData[4]);
            player.Attack = int.Parse(playerData[5]);
            player.Defence = int.Parse(playerData[6]);
            player.Gold = int.Parse(playerData[7]);
        }
        else if (playerData[0] == Player.PlayerType.Archer.ToString())
        {
            player = new Archer();
            player.Name = playerData[1];
            player.Level = int.Parse(playerData[2]);
            player.Health = int.Parse(playerData[3]);
            player.Max_Health = int.Parse(playerData[4]);
            player.Attack = int.Parse(playerData[5]);
            player.Defence = int.Parse(playerData[6]);
            player.Gold = int.Parse(playerData[7]);
        }

        Console.WriteLine("플레이어 데이터 적용");

        //현재 인벤토리를 초기화
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
                //인벤토리에 있는 아이템이므로 판매 상태를 true로 함
                shop.FindItem(itemData).isSold = true;
            }
            //홀수번째이므로 장착 여부를 판단
            else
            {
                //해당 행에 저장된 값이 "True"일 경우
                if(itemData == "True")
                {
                    //아이템을 장착 상태로 함
                    Inventory[itemCount/2].IsEquipped = true;
                    //아이템 장착 효과를 적용
                    Inventory[itemCount/2].EquipEffect(player);
                }
                else
                {
                    //아이템을 장착 해제 상태로 함
                    Inventory[itemCount/2].IsEquipped = false;
                }
            }
            
            itemCount++;
        }

        Console.WriteLine("인벤토리 데이터 적용");
    }

    bool EmptyPlayerCheck()
    {
        //저장된 파일이 있는지 여부를 판단
        //저장된 세이브 txt 파일을 불러옴
        StreamReader streamReader = new StreamReader("C:\\Users\\Allhoon\\source\\repos\\TextRPG\\TextRPG\\data\\save.txt");
        string line = streamReader.ReadLine();

        bool isEmpty;
        //첫 행 내용이 비어있는지 여부로 save 상태를 확인
        if (line == null)
            isEmpty = true;
        else
            isEmpty = false;

        streamReader.Close();

        return isEmpty;
    }
}

