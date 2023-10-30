using System;
using System.Threading;
using System.Xml.Linq;

Tamagocha tamagocha = new Tamagocha { Name = "Николя" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;
tamagocha.DirtyChanged += Tamagocha_DirtyChanged;
tamagocha.ThirstyChanged += Tamagocha_ThirstyChanged;
tamagocha.HealthChanged += Tamagocha_HealthChanged;

ConsoleKeyInfo command;
do
{
    command = Console.ReadKey();
    if (command.Key == ConsoleKey.F)
        tamagocha.Feed();
    else if (command.Key == ConsoleKey.I)
        tamagocha.PrintInfo();
    else if (command.Key == ConsoleKey.W)
        tamagocha.WashTheTamagocha();
    else if (command.Key == ConsoleKey.D)
        tamagocha.Drink();
    else if (command.Key == ConsoleKey.P)
        tamagocha.GeneratePresent();

}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}");
}

void Tamagocha_DirtyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 1);
    Console.Write($"{tamagocha.Name} грязный! Показатель грязноты растет: {tamagocha.Dirty}");
}

void Tamagocha_ThirstyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 2);
    Console.Write($"{tamagocha.Name} хочет пить!!!! Показатель жажды растет: {tamagocha.Thirsty}");
}
void Tamagocha_HealthChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 3);
    Console.Write($"{tamagocha.Name} Показатель здоровья падает: {tamagocha.Health}");
}

class Tamagocha
{
    public string Name { get; set; }
    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
                IsDead = true;
            HealthChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Hungry
    {
        get => hungry;
        set
        {
            hungry = value;
            if (hungry > 100)
                Health -= 10;
            HungryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Dirty
    {
        get => dirty;
        set
        {
            dirty = value;
            DirtyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Thirsty
    {
        get => thirsty;
        set
        {
            thirsty = value;
            ThirstyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsDead { get; set; } = false;

    public event EventHandler HungryChanged;
    public event EventHandler DirtyChanged;
    public event EventHandler ThirstyChanged;
    public event EventHandler HealthChanged;

    public Tamagocha()
    {
        Thread thread = new Thread(LifeCircle);
        thread.Start();
    }
    Random random = new Random();
    private int hungry = 0;
    private int dirty = 0;
    private int thirsty = 0;
    private int health = 100;

    private void LifeCircle(object? obj)
    {
        while (!IsDead)
        {
            Thread.Sleep(500);
            int rnd = random.Next(0, 9);
            switch (rnd)
            {
                case 0: JumpMinute(); break;
                case 1: FallSleep(); break;
                case 2: Talk(); break;
                case 3: Wash(); break;
                case 4: Play(); break;
                case 5: Mess(); break;
                case 6: Dance(); break;
                case 7: BringsPrey(); break;
                case 8: Angry(); break;
                case 9: SharpenClaws(); break;

                default: break;
            }
        }
    }




    private void FallSleep()
    {
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void JumpMinute()
    {
        WriteMessageToConsole($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void Talk()
    {
        WriteMessageToConsole($"{Name} по желанию начинает что-то говорить. Это продолжается целую минуту. Показатели жажды повышены!");
        Thirsty += random.Next(5, 10);
    }

    private void Wash()
    {
        if (Dirty > 60)
            WriteMessageToConsole($"{Name} по желанию начинает вылизываться. Это продолжается целую минуту. Показатели чистоты повышены!");
        Thirsty -= random.Next(5, 10);
    }

    private void Mess()
    {
        WriteMessageToConsole($"{Name} по желанию начинает  пакостить. Это продолжается целую минуту. Показатели жажды, голода, чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void Play()
    {
        WriteMessageToConsole($"{Name} по желанию начинает играться. Это продолжается целую минуту. Показатели жажды, голода, чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void Dance()
    {
        WriteMessageToConsole($"{Name} по желанию начинает танцевать. Это продолжается целую минуту. Показатели жажды, голода, чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void BringsPrey()
    {
        WriteMessageToConsole($"{Name} приносит добычу. Показатели жажды, голода, чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void Angry()
    {
        WriteMessageToConsole($"{Name} по желанию начинает злиться. Это продолжается целую минуту. Показатели чистоты повышены!");
        Dirty += random.Next(5, 10);
    }

    private void SharpenClaws()
    {
        WriteMessageToConsole($"{Name} по желанию начинает точить когти. Это продолжается целую минуту. Показатели жажды, голода, чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }


    private void WriteMessageToConsole(string message)
    {
        Console.SetCursorPosition(0, 10);
        Console.Write(message);
        Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
    }



    public void PrintInfo()
    {
        Console.SetCursorPosition(0, 5);
        Console.WriteLine($"{Name}: Health: {Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead}");
    }

    public void Stop()
    {
        IsDead = true;
    }

    internal void Feed()
    {
        WriteMessageToConsole($"{Name} внезапно начинает ЖРАТЬ как угорелый. Это продолжается целую минуту. ");

        Hungry -= random.Next(5, 10);
    }

    internal void Drink()
    {
        WriteMessageToConsole($"{Name} внезапно начинает ПИТЬ как угорелый. Это продолжается целую минуту. ");

        Thirsty -= random.Next(5, 10);
    }

    internal void WashTheTamagocha()
    {
        WriteMessageToConsole($"{Name} внезапно начинает МЫТЬСЯ как угорелый. Это продолжается целую минуту. ");

        Dirty -= random.Next(5, 10);
    }

    public void GeneratePresent()
    {
        Console.SetCursorPosition(0, 4);
        IPresent present;
        int rnd1 = random.Next(0, 3);
        switch (rnd1)
        {
            case 0: present = new Sweet(); break;
            case 1: present = new Ball(); break;
            default: present = new Phone(); break;
        }
        int rnd2 = random.Next(0, 3);
        switch (rnd2)
        {
            case 0: present.Open(); break;
            case 1: present.Gnaw(); break;
            default: present.Smash(); break;
        }

    }

}
public abstract class Present
{

}
public interface IPresent
{
    void Open();
    void Gnaw();
    void Smash();
}
public class Sweet : Present, IPresent
{
    public void Open()
    {
        Console.WriteLine("открыл коробку конфет!!! жопа моя точно не слипнется? ");
    }

    public void Gnaw()
    {
        Console.WriteLine("грызет коробку с под конфет!!! а зубы мои не выпадут?");
    }

    public void Smash()
    {
        Console.WriteLine("раздавил коробку конфет!!! оййй, я наступил на свой подарок");
    }
}

public class Ball : Present, IPresent
{
    public void Open()
    {
        Console.WriteLine("открыл подарок с мячиком!!! Блин, ну можно что-то по оригинальней подарить?? ");
    }

    public void Gnaw()
    {
        Console.WriteLine("грызет мячик!!! конфеты были по-вкуснее");
    }

    public void Smash()
    {
        Console.WriteLine("лопнул мячик!!! АААА, (упал) мячик лопнул");
    }
}

public class Phone : Present, IPresent
{
    public void Open()
    {
        Console.WriteLine("открыл коробку с телефоном!!! УРААА, достойный подарок!");
    }

    public void Gnaw()
    {
        Console.WriteLine("грызет коробку из под телефона !!! от счастья я разгрыз коробку от телефона");
    }

    public void Smash()
    {
        Console.WriteLine("сломал телефон!!! ** ТВОЮ МАТЬ !!!!!");
    }
}
