# A-07 칠전팔기
## 첫 개인 과제
### 스파르타 던전 (Text 게임) 만들기
#### 파일명 : FirstAssignment
#### 내용
시작시 아래의 텍스트를 출력   
>스파르타 마을에 오신 여러분 환영합니다.
>이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.
>
>1. 상태보기
>2. 인벤토리
>3. 상점
>0. 나가기
>
>원하시는 행동을 입력해주세요.

1번 입력시 상태를 표시

>상태보기
>캐릭터의 정보르 표시합니다.   
>   
>Lv.1   
>Chad(전사)   
>공격력 : 10   
>방어력 : 5    
>체력 : 100    
>Gold : 1500 G 
>
>0. 나가기   
>    
>원하시는 행동을 입력해주세요.   

2번 입력시 인벤토리를 표시하며 아이템을 장착하던가 정렬할 수 있습니다.( 기본 아이템 4가지가 있습니다.)

>인벤토리 - 아이템 판매
>보유 중인 아이템을 관리할 수 있습니다.
>
>[아이템 목록]   
>-무쇠갑옷            |      방어력 +5       | 무쇠로 만들어져 튼튼한 갑옷입니다.   
>-낡은 검             |      공격력 +2       | 쉽게 볼 수 있는 낡은 검입니다.   
>-노가다장갑          |      방어력 +1       | 무 코팅의 흰색 장갑입니다.   
>-청바지              |      방어력 +1       | 일반 청바지입니다.   
>
>1. 장착 관리
>2. 아이템 정렬
>0. 나가기
>
>원하시는 행동을 입력해주세요.

3번 입력시 상점을 이용할 수 있습니다. 상점에서는 아이템을 판매하던가 구매할 수 있습니다.

>상점
>필요한 아이템을 얻을 수 있는 상점입니다.
>
>[보유 골드]
>Gold : 1500 G
>
>[아이템 목록]
>- 수련자 갑옷            |      방어력 +5       | 수련에 도움을 주는 갑옷입니다.                             | 1000G
>- 무쇠갑옷               |      방어력 +9       | 무쇠로 만들어져 튼튼한 갑옷입니다.                         | 2000G
>- 스파르타의 갑옷        |      방어력 +15      | 스파르타의 전사들이 사용했다는 전설의 갑옷                 | 3500G
>- 낡은 검                |      공격력 +2       | 쉽게 볼 수 있는 낡은 검입니다.                             | 600G
>- 청동 도끼              |      공격력 +5       | 어디선가 사용됐던거 같은 도끼입니다.                       | 1500G
>- 스파르타의 창          |      공격력 +7       | 스파르타의 전사들이 사용했다는 전설의 창입니다.            | 2500G
>
>1. 아이템 구매
>2. 아이템 판매
>0. 나가기

원하시는 행동을 입력해주세요.
##### 코드
```
using System;
using System.ComponentModel;
using System.Security.Cryptography;

internal class Program
{
    private static Character player;
    private static List<Item> storage;

    public enum ItemIndexNumber
    {
        defaultArmor,
        defaultWeapon,
        workGlovesr,
        jeans, //3 까지는 디폴드
        nobiceArmor,
        castIronArmor,
        spartanArmor,
        oldSword,
        bronzeAX,
        spartanLance
    }
    // -1 정렬에서 내림차순 오름차순 선택, 0 인트로, 1 상태창, 2 인벤토리,  3 장착 관리, 4 아이템 정렬, 5 상점
    // 6 아이템 구매, 7 아이템 판매
    public enum SelectMenuNumber
    {
        ASCorDESC = -1,
        Intro = 0,
        Status,
        Inventory,
        Equip,
        Arrange,
        Shop,
        BuyItem,
        SaleItem
    }
    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
        storage = new List<Item>();
        Armor defaultArmor = new Armor(ItemType.Armor, "무쇠갑옷", 5, "무쇠로 만들어져 튼튼한 갑옷입니다.",1000);
        Weapon defaultWeapon = new Weapon(ItemType.Weapon, "낡은 검", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600);
        Armor workGlovesr = new Armor(ItemType.Hand_Armor, "노가다장갑", 1, "무 코팅의 흰색 장갑입니다.", 200);
        Armor jeans = new Armor(ItemType.Bottom_Armor, "청바지", 1, "일반 청바지입니다.", 200);
        Armor nobiceArmor = new Armor(ItemType.Tops_Armor, "수련자 갑옷", 5,"수련에 도움을 주는 갑옷입니다.", 1000);
        Armor castIronArmor = new Armor(ItemType.Armor, "무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000);
        Armor spartanArmor = new Armor(ItemType.Tops_Armor, "스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷", 3500);
        Weapon oldSword = new Weapon(ItemType.Weapon, "낡은 검", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600);
        Weapon bronzeAX = new Weapon(ItemType.Weapon,"청동 도끼",5,"어디선가 사용됐던거 같은 도끼입니다.", 1500);
        Weapon spartanLance = new Weapon(ItemType.Weapon,"스파르타의 창",7,"스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500);
        storage.Add(defaultArmor);
        storage.Add(defaultWeapon);
        storage.Add(workGlovesr);
        storage.Add(jeans);
        storage.Add(nobiceArmor);
        storage.Add(castIronArmor);
        storage.Add(spartanArmor);
        storage.Add(oldSword);
        storage.Add(bronzeAX);
        storage.Add(spartanLance);


        player.PickUpItem(storage[(int)ItemIndexNumber.defaultArmor]);
        player.PickUpItem(storage[(int)ItemIndexNumber.defaultWeapon]);
        player.PickUpItem(storage[(int)ItemIndexNumber.workGlovesr]);
        player.PickUpItem(storage[(int)ItemIndexNumber.jeans]);
    }

    static void DisplayGameIntro()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            DisplaySelectMenu(SelectMenuNumber.Intro);

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    return;
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop(new int[] { (int)ItemIndexNumber.nobiceArmor,
                        (int)ItemIndexNumber.castIronArmor,
                        (int)ItemIndexNumber.spartanArmor,
                        (int)ItemIndexNumber.oldSword,
                        (int)ItemIndexNumber.bronzeAX,
                        (int)ItemIndexNumber.spartanLance
                    });
                    break;
            }
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보르 표시합니다.");
        DisplaySelectMenu(SelectMenuNumber.Status);

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                return;
        }
    }

    static void DisplayInventory()
    {
        while (true)
        {
            Console.Clear();

            DisplayItemList(SelectMenuNumber.Inventory);
            DisplaySelectMenu(SelectMenuNumber.Inventory);

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    return;
                case 1:
                    DisplayItemUsing();
                    break;
                case 2:
                    DisplayInventoryArrange(SelectMenuNumber.Arrange);
                    break;
            }
        }
    }
    /// <summary>
    ///  입력이 유효한지 검사하는 메서드 입니다.
    /// </summary>
    /// <param name="min">가장 작은 선택지 정수를 받는 파라미터입니다.</param>
    /// <param name="max">가장 큰 선택지 정수를 받는 파라미터입니다.</param>
    /// <returns></returns>
    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }
            Console.WriteLine("잘못된 입력입니다.");
        }
    }
    static void DisplayItemList(SelectMenuNumber select)
    {
        string selectedMenu = "";
        Console.Clear();

        if (select == SelectMenuNumber.Equip)
            selectedMenu = " - 장착 관리";
        else if (select == SelectMenuNumber.Arrange)
            selectedMenu = " - 아이템 정렬";
        else
            selectedMenu = " - 아이템 판매";
        if ((int)select <= (int)SelectMenuNumber.Arrange)
        {
            Console.WriteLine("인벤토리{0}", selectedMenu);
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        }
        else
        {
            Console.WriteLine("상점{0}\n필요한 아이템을 얻을 수 있는 상점입니다.\n", selectedMenu);
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"Gold : {player.Gold} G\n");
        }
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < player.inventory.Count(); i++)
        {
            int nameBlankLeft = 20;
            int effectBlankLeft = 20;
            int effectBlankRight;

            nameBlankLeft -= select == SelectMenuNumber.Equip ? 2 : 0;
            nameBlankLeft -= player.inventory[i].Using == true ? 3 : 0;
            nameBlankLeft -= (player.inventory[i].Name.Length + isCountHangul(player.inventory[i].Name));

            effectBlankLeft -= (player.inventory[i].Atk == 0 ? (player.inventory[i].Def == 0 ? ((int)Math.Log10(player.inventory[i].Hp) + 10) : (int)Math.Log10(player.inventory[i].Def) + 7) : (int)Math.Log10(player.inventory[i].Atk)+7);
            effectBlankRight = (effectBlankLeft + 1) /2;
            effectBlankLeft = effectBlankLeft / 2;

            Console.WriteLine("- "+ "{0}{1}{2}".PadRight(nameBlankLeft + 9) + "|".PadRight(effectBlankLeft+1) + "{3}{4}{5}".PadRight(effectBlankRight+9) + "| {6}",
                (select == SelectMenuNumber.Equip) || (select == SelectMenuNumber.SaleItem) ? i + 1 + " " : null,
                player.inventory[i].Using == true ? "[E]" : null,
                player.inventory[i].Name,
                player.inventory[i].Atk == 0 ? null : "공격력 " + (Math.Sign(player.inventory[i].Atk) == 1 ? "+" : "-") + player.inventory[i].Atk,
                player.inventory[i].Def == 0 ? null : "방어력 " + (Math.Sign(player.inventory[i].Def) == 1 ? "+" : "-") + player.inventory[i].Def,
                player.inventory[i].Hp == 0 ? null : "체력 회복 " + (Math.Sign(player.inventory[i].Hp) == 1 ? "+" : "-") + player.inventory[i].Hp,
                player.inventory[i].Explanation
                );
        }
        Console.WriteLine();
    }
    static void DisplayItemUsing()
    {
        while(true)
        {
            Console.Clear();

            DisplayItemList(SelectMenuNumber.Equip);
            DisplaySelectMenu(SelectMenuNumber.Equip);

            int input = CheckValidInput(0, player.inventory.Count());
            switch (input)
            {
                case 0:
                    return;
                default:
                    player.IsUsing(input-1);
                    break;
            }
        }
    }
    public static int isCountHangul(string str)
    {
        int count = 0;
        char[] charArr = str.ToCharArray();
        foreach (char c in charArr)
        {
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                count++;
        }
        return count;
    }
    public static void DisplaySelectMenu(SelectMenuNumber select)
    {
        //0 인트로, 1 상태창, 2 인벤토리,  3 장착 관리, 4 아이템 정렬, -1 정렬에서 내림차순 오름차순 선택 
        if (select == SelectMenuNumber.Intro)
        {
            Console.WriteLine("\n1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
        }
        else if (select == SelectMenuNumber.Status)
        {
            Console.WriteLine($"\nLv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 : {player.Atk} " +
                "{0}", player.addAtk == 0 ? null : ("(" + (Math.Sign(player.addAtk) == 1 ? "+" : "-") + player.addAtk + ")")
                );
            Console.WriteLine($"방어력 : {player.Def} " +
                "{0}", player.addDef == 0 ? null : ("(" + (Math.Sign(player.addDef) == 1 ? "+" : "-") + player.addDef + ")")
                );
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G\n");
        }
        else if(select == SelectMenuNumber.Inventory)
        {
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬");
        }
        else if (select == SelectMenuNumber.Arrange)
        {
            Console.WriteLine("1. 이름");
            Console.WriteLine("2. 장착순");
            Console.WriteLine("3. 공격력");
            Console.WriteLine("4. 방어력");
        }
        else if(select == SelectMenuNumber.ASCorDESC)
        {
            Console.WriteLine("\n1. 오름차순");
            Console.WriteLine("2. 내림차순");
        }
        else if (select == SelectMenuNumber.Shop)
        {
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
        }
        Console.WriteLine("0. 나가기\n");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
    }
    public static void DisplayInventoryArrange(SelectMenuNumber select)
    {
        while (true)
        {
            Console.Clear();

            DisplayItemList(select);
            DisplaySelectMenu(select);

            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 0:
                    return;
                default:
                    DisplaySelectMenu(SelectMenuNumber.ASCorDESC);
                    int arrSelect = CheckValidInput(0, 2);
                    orderByType(input, arrSelect);
                    break;
            }
        }
    }
    public static void orderByType(int selectType, int isASC)
    {
        if(selectType == 1)
        {
            if (isASC == 1)
                player.inventory.Sort((ItemA, ItemB) => ItemA.Name.CompareTo(ItemB.Name));
            else if (isASC == 2)
                player.inventory.Sort((ItemA, ItemB) => ItemB.Name.CompareTo(ItemA.Name));
            else return;
        }
        else if(selectType == 2)
        {
            if (isASC == 1)
            {
                player.inventory.Sort((ItemA, ItemB) => ItemA.UsingCount.CompareTo(ItemB.UsingCount));
                player.inventory.Sort((ItemA, ItemB) => ItemB.Using.CompareTo(ItemA.Using));
            }
            else if (isASC == 2)
            {
                player.inventory.Sort((ItemA, ItemB) => ItemB.Using.CompareTo(ItemA.Using));
                player.inventory.Sort((ItemA, ItemB) => ItemB.UsingCount.CompareTo(ItemA.UsingCount));
            }
            else return;
        }
        else if (selectType == 3)
        {
            if (isASC == 1)
                player.inventory.Sort((ItemA, ItemB) => ItemA.Atk.CompareTo(ItemB.Atk));
            else if (isASC == 2)
                player.inventory.Sort((ItemA, ItemB) => ItemB.Atk.CompareTo(ItemA.Atk));
            else return;
        }
        else
        {
            if (isASC == 1)
                player.inventory.Sort((ItemA, ItemB) => ItemA.Def.CompareTo(ItemB.Def));
            else if (isASC == 2)
                player.inventory.Sort((ItemA, ItemB) => ItemB.Def.CompareTo(ItemA.Def));
            else return;
        }

    }
    public static void DisplayShop(int[] itemArr)
    {
        while (true)
        {
            DisplayShopList(SelectMenuNumber.Shop, itemArr);
            DisplaySelectMenu(SelectMenuNumber.Shop);
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    return;
                case 1:
                    DisplayShopBuyItem(itemArr);
                    break;
                case 2:
                    DisplayShopSaleItem();
                    break;
            }
        }
    }
    public static void DisplayShopList(SelectMenuNumber isBuyIteom, int[] itemArr) 
    {
        int count = 0;
        Console.Clear();

        Console.WriteLine("상점{0}\n필요한 아이템을 얻을 수 있는 상점입니다.\n",
            isBuyIteom == SelectMenuNumber.BuyItem ? " - 아이템 구매" :  null
            );
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"Gold : {player.Gold} G\n");
        Console.WriteLine("[아이템 목록]");
        foreach (int i in itemArr)
        {
            int nameBlankLeft = 20;
            int effectBlankLeft = 20;
            int effectBlankRight;
            int explanationBlank = 60;

            nameBlankLeft -= isBuyIteom == SelectMenuNumber.BuyItem ? 2 : 0;
            nameBlankLeft -= (storage[i].Name.Length + isCountHangul(storage[i].Name));

            effectBlankLeft -= (storage[i].Atk == 0 ? (storage[i].Def == 0 ? ((int)Math.Log10(storage[i].Hp) + 10) : (int)Math.Log10(storage[i].Def) + 7) : (int)Math.Log10(storage[i].Atk) + 7);
            effectBlankRight = (effectBlankLeft + 1) / 2;
            effectBlankLeft = effectBlankLeft / 2;

            explanationBlank -= (storage[i].Explanation.Length + isCountHangul(storage[i].Explanation));
            count++;

            Console.WriteLine("- " + "{0}{1}".PadRight(nameBlankLeft + 9) + "|".PadRight(effectBlankLeft + 1) + "{2}{3}{4}".PadRight(effectBlankRight + 9) + "| {5}".PadRight(explanationBlank + 4) + "| {6}",
                isBuyIteom == SelectMenuNumber.BuyItem ? count + " " : null,
                storage[i].Name,
                storage[i].Atk == 0 ? null : "공격력 " + (Math.Sign(storage[i].Atk) == 1 ? "+" : "-") + storage[i].Atk,
                storage[i].Def == 0 ? null : "방어력 " + (Math.Sign(storage[i].Def) == 1 ? "+" : "-") + storage[i].Def,
                storage[i].Hp == 0 ? null : "체력 회복 " + (Math.Sign(storage[i].Hp) == 1 ? "+" : "-") + storage[i].Hp,
                storage[i].Explanation,
                player.inventory.Contains(storage[i]) ? "구매완료" : (storage[i].Price + "G")
                );
        }
        Console.WriteLine();
    }
    public static void DisplayShopBuyItem(int[] itemArr)
    {
        while (true)
        {
            
            DisplayShopList(SelectMenuNumber.BuyItem, itemArr);
            DisplaySelectMenu(SelectMenuNumber.BuyItem);
            int input = CheckValidInput(0, itemArr.Length);
            switch (input)
            {
                case 0:
                    return;
                default:
                    player.BuyItem(storage[itemArr[input - 1]]);
                    break;
            }
        }
    }
    public static void DisplayShopSaleItem()
    {
        while (true)
        {
            DisplayItemList(SelectMenuNumber.SaleItem);
            DisplaySelectMenu(SelectMenuNumber.SaleItem);
            int input = CheckValidInput(0, player.inventory.Count);
            switch (input)
            {
                case 0:
                    return;
                default:
                    player.SaleItem(input-1);
                    break;
            }
        }
    }
}


public class Character
{
    public string Name { get; private set; }
    public string Job { get; private set; }
    public int Level { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Hp { get; private set; }
    public int Gold { get; private set; }
    public List<Item> inventory { get; }
    public List<Item> usingItem { get; }
    public int addAtk { get; private set; }
    public int addDef { get; private set; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        inventory = new List<Item>();
        usingItem = new List<Item>();
    }
    public void IsUsing(int ItemIndex)
    {
        if (inventory[ItemIndex].Using == false)
        {
            if (inventory[ItemIndex].Type != ItemType.Potion)
            {
                inventory[ItemIndex].Using = !inventory[ItemIndex].Using;
                inventory[ItemIndex].UsingCount = usingItem.Count+1;
                usingItem.Add(inventory[ItemIndex]);
                addAtk += inventory[ItemIndex].Atk;
                addDef += inventory[ItemIndex].Def;
                Atk += inventory[ItemIndex].Atk;
                Def += inventory[ItemIndex].Def;
            }
            else
            {
                //포션 먹는 메서드 구현하여 적을것
            }
        }
        else
        {
            inventory[ItemIndex].Using = !inventory[ItemIndex].Using;
            inventory[ItemIndex].UsingCount = 0;
            usingItem.Remove(inventory[ItemIndex]);
            addAtk -= inventory[ItemIndex].Atk;
            addDef -= inventory[ItemIndex].Def;
            Atk -= inventory[ItemIndex].Atk;
            Def -= inventory[ItemIndex].Def;
        }
    }
    public void PickUpItem(Item pickUpItem)
    {
        inventory.Add(pickUpItem);
    }
    public void BuyItem(Item buyItem)
    {
        if(Gold >= buyItem.Price)
        {
            Gold -= buyItem.Price;
            inventory.Add(buyItem);
        }
        else if (inventory.Contains(buyItem))
        {
            Console.WriteLine("\n이미 구매한 아이템입니다.");
            Thread.Sleep(500);
        }
        else 
        {
            Console.WriteLine("\nGold가 부족합니다.");
            Thread.Sleep(500);
        }
    }
    public void SaleItem(int ItemIndex)
    {
        float salePrice = inventory[ItemIndex].Price * 0.85f;
        Gold += (int)salePrice;
        if(inventory[ItemIndex].Using == true)
        {
            addAtk -= inventory[ItemIndex].Atk;
            addDef -= inventory[ItemIndex].Def;
            Atk -= inventory[ItemIndex].Atk;
            Def -= inventory[ItemIndex].Def;
            inventory[ItemIndex].Using = false;
            inventory[ItemIndex].UsingCount = 0;
        }
        inventory.Remove(inventory[ItemIndex]);
    }
}
public enum ItemType
{
    Weapon, //0
    OneHandWeapon, 
    TwoHandWeapon,
    Shield,
    Armor = 10, //
    Head_Armor, //11
    Tops_Armor,
    Bottom_Armor,
    Hand_Armor,
    Foot_Armor, //15
    Potion = 20,
    HealthPotion, //21
    StrengthPotion  //22
}
public interface Item
{
    ItemType Type { get; }
    string Name { get; }
    int Atk { get; }
    int Def { get; }
    int Hp { get; }
    string Explanation { get; }
    int UsingCount { get; set; }
    bool Using { get; set; }
    int Price { get; }
}
class Weapon : Item
{
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public string Explanation { get; set; }
    public int UsingCount { get; set; }
    public bool Using { get; set; }
    public int Price { get; }
    public Weapon(ItemType type, string name, int atk, int def, int hp, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Hp = hp;
        Explanation = explanation;
        Price = price;
    }
    public Weapon(ItemType type, string name, int atk, int def, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Explanation = explanation;
        Price = price;
    }
    public Weapon(ItemType type, string name, int atk, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = 0;
        Explanation = explanation;
        Price = price;
    }
}
class Armor : Item
{
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public string Explanation { get; set; }
    public int UsingCount { get; set; }
    public bool Using { get; set; }
    public int Price { get; }
    public Armor(ItemType type, string name, int atk, int def, int hp, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Hp = hp;
        Explanation = explanation;
        Price= price;
    }
    public Armor(ItemType type, string name, int atk, int def, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Explanation = explanation;
        Price = price;
    }
    public Armor(ItemType type, string name, int def, string explanation, int price)
    {
        Type = type;
        Name = name;
        Def = def;
        Explanation = explanation;
        Price = price;
    }
}
class Potion : Item
{
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public string Explanation { get; set; }
    public int UsingCount { get; set; }
    public bool Using { get; set; }
    public int Price { get; }
    public Potion(ItemType type, string name, int atk, int def, int hp, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Hp = hp;
        Explanation = explanation;
        Price = price;
    }
    public Potion(ItemType type, string name, int atk, int def, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Explanation = explanation;
        Price = price;
    }
    public Potion(ItemType type, string name, int hp, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = 0;
        Def = 0;
        Hp = hp;
        Explanation = explanation;
        Price = price;
    }
    public Potion(ItemType type, int atk, string name, string explanation, int price)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = 0;
        Hp = 0;
        Explanation = explanation;
        Price = price;
    }
    public Potion(ItemType type, string name, string explanation, int def, int price)
    {
        Type = type;
        Name = name;
        Atk = 0;
        Def = def;
        Hp = 0;
        Explanation = explanation;
        Price = price;
    }
}
```
