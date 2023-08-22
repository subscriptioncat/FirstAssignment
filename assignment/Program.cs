using System.ComponentModel;

internal class Program
{
    private static Character player;

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
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("0. 게임종료");
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                break;

            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보르 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < player.Inventory.Count(); i++)
        {
            Console.WriteLine("- {0}{1}| {2}{3}{4} | {5}", 
                null,
                player.Inventory[i].Name.PadRight(10),
                player.Inventory[i].Atk == 0 ? null : "공격력 " + (Math.Sign(player.Inventory[i].Atk) == 1 ? "+" : "-") + player.Inventory[i].Atk,
                player.Inventory[i].Def == 0 ? null : "방어력 "+(Math.Sign(player.Inventory[i].Def) == 1 ? "+" : "-") + player.Inventory[i].Def,
                player.Inventory[i].Hp == 0 ? null : "체력 회복 " + (Math.Sign(player.Inventory[i].Hp) == 1 ? "+" : "-") + player.Inventory[i].Hp,
                player.Inventory[i].Explanation
                );
        }
        Console.WriteLine("\n0. 나가기\n1. 장착 관리\n");
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
            case 1:
                //DisplayGameIntro(); 장착관리 출력화면으로
                break;
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
}


public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; }
    public List<Item> Inventory { get; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        Armor defaultArmor = new Armor(ItemType.Armor,"무쇠갑옷",5,"무쇠로 만들어져 튼튼한 갑옷입니다.");
        Weapon defaultWeapon = new Weapon(ItemType.Weapon,"낡은 검",2,"쉽게 볼 수 있는 낡은 검입니다.");
        Inventory = new List<Item>();
        Inventory.Add(defaultArmor);
        Inventory.Add(defaultWeapon);
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
    ItemType Type { get; set; }
    string Name { get; set; }
    int Atk { get; set; }
    int Def { get; set; }
    int Hp { get; set; }
    string Explanation { get; set; }
}
class Weapon : Item
{
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public string Explanation { get; set; }

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
class Potion
{
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public string Explanation { get; set; }

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