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
            Fighter selectedFighters = null;

            while (selectedFighters == null)
            {
                ShowInfo();

                Console.WriteLine("Выберите бойца по номеру: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= _fighters.Count)
                {
                    selectedFighters = _fighters[choice - 1];
                    Console.WriteLine($"{selectedFighters.Name}");
                }
                else
                {
                    Console.WriteLine("Некорректный выбор.");
                }
            }

            return selectedFighters;
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
            if (IsAlive)
            {
               Health -= damage;
            }
        }
    }

    class Arbalester : Fighter
    {
        public Arbalester(string name) : base(name)
        {
            Health = 1500;
            TwinShotDamage = 148;
        }

        public int TwinShotDamage { get; private set; }

        public override void Attack(Fighter enemy)
        {
            Console.WriteLine($"{Name}, выпускает двойную стрелу.");
            enemy.TakeDamage(TwinShotDamage);
        }
    }

    class Archmage : Fighter
    {
        public Archmage(string name) : base(name)
        {
            Health = 1500;
            AuraFlashDamage = 150;
        }

        public int AuraFlashDamage { get; private set; }

        public override void Attack(Fighter enemy)
        {
            Console.WriteLine($"{Name} использует магическое заклинание.");
            enemy.TakeDamage(AuraFlashDamage);
        }
    }

    class Knight : Fighter
    {
        public Knight(string name) : base(name)
        {
            Health = 2000;
            ShieldFortressDamage = 170;
        }

        public int ShieldFortressDamage { get; private set; }

        public override void Attack(Fighter enemy)
        {
            Console.WriteLine($"{Name} удар.");
            enemy.TakeDamage(ShieldFortressDamage);
        }
    }
}

class Hunter : Fighter
{
    public Hunter(string name) : base(name)
    {
        Health = 1500;
        WindBladeDamage = 160;
    }

    public int WindBladeDamage { get; private set; }

    public override void Attack(Fighter enemy)
    {
        Console.WriteLine($"{Name} атакует мечом.");
        enemy.TakeDamage(WindBladeDamage);
    }
}

class Magician : Fighter
{
    public Magician(string name) : base(name)
    {
        Health = 1500;
        FireDamage = 150;
    }

    public int FireDamage { get; private set; }

    public override void Attack(Fighter enemy)
    {
        Console.WriteLine($"{Name} атакует огнем.");
        enemy.TakeDamage(FireDamage);
    }
}