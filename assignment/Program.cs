﻿using System.ComponentModel;
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
        jeans
    }
    //0 인트로, 1 상태창, 2 인벤토리,  3 장착 관리, 4 아이템 정렬, -1 정렬에서 내림차순 오름차순 선택
    public enum SelectMenuNumber
    {
        ASCorDESC = -1,
        Intro = 0,
        Status,
        Inventory,
        Equip,
        Arrange
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
        Armor defaultArmor = new Armor(ItemType.Armor, "무쇠갑옷", 5, "무쇠로 만들어져 튼튼한 갑옷입니다.");
        Weapon defaultWeapon = new Weapon(ItemType.Weapon, "낡은 검", 2, "쉽게 볼 수 있는 낡은 검입니다.");
        Armor workGlovesr = new Armor(ItemType.Hand_Armor, "노가다장갑", 1, "무 코팅의 흰색 장갑입니다.");
        Armor jeans = new Armor(ItemType.Bottom_Armor, "청바지", 1, "일반 청바지입니다.");
        storage.Add(defaultArmor);
        storage.Add(defaultWeapon);
        storage.Add(workGlovesr);
        storage.Add(jeans);

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
            DisplayInventorySelectMenu(SelectMenuNumber.Intro);

            int input = CheckValidInput(0, 2);
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
            }
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보르 표시합니다.");
        DisplayInventorySelectMenu(SelectMenuNumber.Status);

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
            DisplayInventorySelectMenu(SelectMenuNumber.Inventory);

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

        if(select == SelectMenuNumber.Equip)
            selectedMenu = " - 장착 관리";
        else if(select == SelectMenuNumber.Arrange)
            selectedMenu = " - 아이템 정렬";

        Console.WriteLine("인벤토리{0}", selectedMenu);
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
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
                select == SelectMenuNumber.Equip ? i + 1 + " " : null,
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
            DisplayInventorySelectMenu(SelectMenuNumber.Equip);

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
            {
                count++;
            }
        }

        return count;
    }
    public static void DisplayInventorySelectMenu(SelectMenuNumber select)
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
        Console.WriteLine("0. 나가기\n");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
    }
    public static void DisplayInventoryArrange(SelectMenuNumber select)
    {
        while (true)
        {
            Console.Clear();

            DisplayItemList(select);
            DisplayInventorySelectMenu(select);

            int input = CheckValidInput(0, 4);

            

            switch (input)
            {
                case 0:
                    return;
                default:
                    DisplayInventorySelectMenu(SelectMenuNumber.ASCorDESC);
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
    public Weapon(ItemType type, string name, int atk, int def, int hp, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Hp = hp;
        Explanation = explanation;
    }
    public Weapon(ItemType type, string name, int atk, int def, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Explanation = explanation;
    }
    public Weapon(ItemType type, string name, int atk, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = 0;
        Explanation = explanation;
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

    public Armor(ItemType type, string name, int atk, int def, int hp, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Hp = hp;
        Explanation = explanation;
    }
    public Armor(ItemType type, string name, int atk, int def, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Explanation = explanation;
    }
    public Armor(ItemType type, string name, int def, string explanation)
    {
        Type = type;
        Name = name;
        Def = def;
        Explanation = explanation;
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

    public Potion(ItemType type, string name, int atk, int def, int hp, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Hp = hp;
        Explanation = explanation;
    }
    public Potion(ItemType type, string name, int atk, int def, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = def;
        Explanation = explanation;
    }
    public Potion(ItemType type, string name, int hp, string explanation)
    {
        Type = type;
        Name = name;
        Atk = 0;
        Def = 0;
        Hp = hp;
        Explanation = explanation;
    }
    public Potion(ItemType type, int atk, string name, string explanation)
    {
        Type = type;
        Name = name;
        Atk = atk;
        Def = 0;
        Hp = 0;
        Explanation = explanation;
    }
    public Potion(ItemType type, string name, string explanation, int def)
    {
        Type = type;
        Name = name;
        Atk = 0;
        Def = def;
        Hp = 0;
        Explanation = explanation;
    }
}