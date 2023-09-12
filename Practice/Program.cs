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

        private ConsoleColor fullColor;

        public Arena()
        {
            _fighters.Add(new Arbalester());
            _fighters.Add(new Archmage());
            _fighters.Add(new Knight());
            _fighters.Add(new Hunter());
            _fighters.Add(new Magician());
        }

        public void ShowInfo()
        {
            foreach (Fighter fighter in _fighters)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Информация об игроках: ");
                Console.WriteLine($"{fighter.Name} -- {fighter.Health}.\n");
            }
        }

        public Fighter ChooseFighert()
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
                return ChooseFighert();
            }
        }

        public void StartBattle()
        {
            Console.WriteLine("Выберите первого бойца: ");
            Fighter firstFighter = ChooseFighert();
            Console.WriteLine("Выберите второго бойца: ");
            Fighter secondFighter = ChooseFighert();
            Console.ReadKey();
            Console.Clear();

            ConsoleColor firstFighterFullColor = ConsoleColor.Cyan;
            ConsoleColor secondFighterFullColor = ConsoleColor.DarkYellow;

            Console.WriteLine("Нажмите любую клавишу для начала боя.");

            while (firstFighter.isAlive && secondFighter.isAlive)
            {
                firstFighter.DisplayHealthBar(firstFighterFullColor);
                secondFighter.DisplayHealthBar(secondFighterFullColor);


                firstFighter.DisplayHealthBar(fullColor);
                secondFighter.DisplayHealthBar(fullColor);

                firstFighter.Attack(secondFighter);
                secondFighter.Attack(firstFighter);

                Console.ReadKey();
                Console.WriteLine("Бой завершён.");
            }

        }
    }

    class Fighter
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; } = 2000;
        public int Damage { get; protected set; }
        public bool isAlive => Health > 0;

        public void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);
        }

        public void DisplayHealthBar(ConsoleColor fullColor)
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

        private void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                Health -= damage;
            }
        }
    }

    class Arbalester : Fighter
    {
        public Arbalester()
        {
            TwinShot = 148;
            Health = 1000;
            Name = "Байбал";
            Damage = UsesSpecialAttack();
        }

        public int TwinShot { get; private set; }

        public int UsesSpecialAttack()
        {
            return TwinShot;
        }
    }

    class Archmage : Fighter
    {
        public Archmage()
        {
            AuraFlash = 187;
            Health = 900;
            Name = "ЫФВФЫВ";
            Damage = UsesSpecialAttack();
        }

        public int AuraFlash { get; private set; }

        public int UsesSpecialAttack()
        {
            return AuraFlash;
        }
    }

    class Knight : Fighter
    {
        public Knight()
        {
            ShieldFortress = 99;
            Health = 2000;
            Name = "АаААа";
            Damage = 99;
        }

        public int ShieldFortress { get; private set; }

        public int UsesSpecialAttack()
        {
            return ShieldFortress;
        }
    }
}

class Hunter : Fighter
{

    public Hunter()
    {
        HammerCrush = 214;
        Health = 1000;
        Name = "Мич";
        Damage = UsesSpecialAttack();
    }

    public int HammerCrush { get; private set; }

    public int UsesSpecialAttack()
    {
        return HammerCrush;
    }
}

class Magician : Fighter
{
    public Magician()
    {
        Health = 900;
        SwordAttack = 50;
        GreaterHeal = 50;
        Name = "БбБб";
        Damage = UsesSpecialAttack();
    }

    public int SwordAttack { get; set; }
    public int GreaterHeal { get; set; }

    public int UsesSpecialAttack()
    {
        GetHeal();
        return SwordAttack;
    }

    public void GetHeal()
    {
        Random random = new Random();
        int randomHeal = random.Next(0, GreaterHeal);
        randomHeal += Health;
        Console.WriteLine($"Восстановаление здоровья {randomHeal} - хп.");
    }
}