using Practice;
using System;
using System.Collections.Generic;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();

            arena.StartBattle();

            Console.ReadKey();
        }
    }

    class Arena
    {
        private List<Fighter> _fighters = new List<Fighter>();

        public Arena()
        {
            _fighters.Add(new Arbalester("Байбал"));
            _fighters.Add(new Archmage("Урсууна"));
            _fighters.Add(new Knight("Мич"));
            _fighters.Add(new Hunter("Альберт"));
            _fighters.Add(new Magician("АаааАА"));
        }

        public void ShowInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Информация об игроках: ");

            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.WriteLine($"{i + 1}, {_fighters[i].Name} -- {_fighters[i].Health}.\n");
            }
        }


        public Fighter ChooseFighter()
        {
            ShowInfo();

            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.WriteLine($"Выберите бойца:{_fighters[i].Name}.");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= _fighters.Count)
            {
                Fighter selectedFighters = _fighters[choice - 1];
                Console.WriteLine($"{selectedFighters.Name}");
                return selectedFighters;
            }
            else
            {
                Console.WriteLine("Некорректный выбор.");
                return ChooseFighter();
            }
        }

        public void StartBattle()
        {
            Console.WriteLine("Выберите первого бойца: ");
            Fighter firstFighter = ChooseFighter();
            Console.WriteLine("Выберите второго бойца: ");
            Fighter secondFighter = ChooseFighter();

            Console.Clear();

            ConsoleColor firstColor = ConsoleColor.Cyan;
            ConsoleColor secondColor = ConsoleColor.DarkYellow;

            Console.WriteLine("Нажмите любую клавишу для начала боя.");
            Console.ReadKey();
            Console.Clear();

            while (firstFighter.IsAlive && secondFighter.IsAlive)
            {
                firstFighter.DisplayHealthBar(firstColor);
                secondFighter.DisplayHealthBar(secondColor);

                firstFighter.Attack(secondFighter);
                secondFighter.Attack(firstFighter);

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Бой завершён.");
        }
    }

    class Fighter
    {
        public Fighter(string name)
        {
            Name = name;
            Health = MaxHealth;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; } = 2000;
        public int Damage { get; protected set; }
        public bool IsAlive => Health > 0;

        public virtual void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);
        }

        public virtual void DisplayHealthBar(ConsoleColor fullColor)
        {
            Console.Write($"Имя: {Name}\nHealth: ");

            int maxHealth = MaxHealth;
            int currentHealth = Health;

            int healthBarLength = 50;
            int filledLength = (int)((float)currentHealth / maxHealth * healthBarLength);

            Console.ForegroundColor = fullColor;
            Console.Write("[");

            for (int i = 0; i < healthBarLength; i++)
            {
                if (i < filledLength)
                {
                    Console.Write("█");
                }
            }

            Console.WriteLine($"] {currentHealth}/{healthBarLength}");
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                Health -= damage;
                if (Health < 0)
                {
                    Health = 0;
                }
            }
        }
    }

    class Arbalester : Fighter
    {
        public Arbalester(string name) : base(name)
        {
            Health = 1000;
            TwinShot = 148;
        }

        public int TwinShot { get; private set; }

        public override void Attack(Fighter enemy)
        {
            Console.WriteLine($"{Name}, выпускает двойную стрелу.");
            enemy.TakeDamage(TwinShot);
        }
    }

    class Archmage : Fighter
    {
        public Archmage(string name) : base(name)
        {
            Health = 900;
            AuraFlash = 150;
        }

        public int AuraFlash { get; private set; }

        public override void Attack(Fighter enemy)
        {
            Console.WriteLine($"{Name} использует магическое заклинание.");
            enemy.TakeDamage(AuraFlash);
        }
    }

    class Knight : Fighter
    {
        public Knight(string name) : base(name)
        {
            Health = 2000;
            ShieldFortress = 170;
        }

        public int ShieldFortress { get; private set; }

        public override void Attack(Fighter enemy)
        {
            Console.WriteLine($"{Name} удар.");
            enemy.TakeDamage(ShieldFortress);
        }
    }
}

class Hunter : Fighter
{
    public Hunter(string name) : base(name)
    {
        Health = 1500;
        HammerCrush = 160;
    }

    public int HammerCrush { get; private set; }

    public override void Attack(Fighter enemy)
    {
        Console.WriteLine($"{Name} бьет молотом в землю.");
        enemy.TakeDamage(HammerCrush);
    }
}

class Magician : Fighter
{
    public Magician(string name) : base(name)
    {
        Health = 900;
        SwordAttack = 150;
    }

    public int SwordAttack { get; set; }

    public override void Attack(Fighter enemy)
    {
        Console.WriteLine($"{Name} Бьет мечом");
        enemy.TakeDamage(SwordAttack);
    }
}