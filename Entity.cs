using System.ComponentModel;

namespace Test;
public class Entity(string name, int health, int damage, int givenXp)
{
    public string Name{get; private set; } = name;
    public int Health{get; private set; } = health;
    public int MaxHealth{get; private set; } = health;
    public int Damage{get; private set; } = damage;
    public int GivenXp{get;} = givenXp;
    public bool IsAlive{get; private set; } = true;
    public List<Effect> Effects{get; private set;} = [];
    public List<Property> Properties{get; private set;} = [];

    public void GiveEffects(bool unique, Effect[] effects)
    {
        if (unique)
        {
            foreach (Effect effect in effects)
            {
                if (!Effects.Contains(effect))
                {
                    Effects.Add(effect);
                }
            }
        }
        else
        {
            Effects.AddRange(effects);
        }
    }
    public void ClearEffects()
    {
        Effects = [];
    }
    public void Update(int count = 1)
    {
        for(int i = 0; i < count; i++)
        {
            foreach (Effect effect in Effects)
            {
                switch (effect)
                {
                    case Effect.Poison:
                        TakeDamage(10);
                        Console.WriteLine(Name + ": отравление, нанесено 10 урона");
                        break;
                    
                    case Effect.Regeneration:
                        if (IsAlive)
                        {
                            Heal();
                            Console.WriteLine(Name + ": исцеление, 10 здоровья");
                        }
                        break;
                    
                    default:
                        break;
                }
            }
            Heal();
        }
    }
    public void Attack(Entity target)
    {
        List<Effect> EnemyEffects = target.Effects;
        foreach (Effect effect in EnemyEffects)
        {
            if(effect == Effect.Boost)
            {
                SetDamage(Damage * 2);
            }
            if(effect == Effect.Weakness)
            {
                SetDamage(Damage / 2);
            }
        }
        

        bool isEnemyAlive = target.TakeDamage(Damage);
        if (!isEnemyAlive)
        {
            return;
        }

        List<Property> EnemyProperties = target.Properties;
        foreach (Property property in EnemyProperties)
        {
            if(property == Property.Venomous)
            {
                target.GiveEffects(true, [Effect.Poison]);
            }
            if(property == Property.Spikes)
            {
                TakeDamage(Damage / 10);
            }
        }
    }

    public void Heal(int bonus = 10)
    {   
        if (IsAlive)
        {
            SetHealth(Health + bonus);
        }
    }
    public void Kill()
    {
        SetHealth(0);
    }
    public bool TakeDamage(int value)
    {
        if(Health <= 0)
        {
            return false;
        }
        
        int resistanceCount = Effects.Count(e => e == Effect.Resistance);
        if(resistanceCount > 0)
        {
            value -= (5 * resistanceCount);
        }

        if(value < 0)
        {
            value = 0;
        }
        
        SetHealth(Health - value);
        return IsAlive;
    }
    private void SetHealth(int value)
    {
        LogMaster.Log($"{Name} здоровье {Health}/{MaxHealth} (до)");
        if (IsAlive)
        {
            
            Health = value;
            if(Health <= 0)
            {
                Health = 0;
                IsAlive = false;
            }
            else if(Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
        else
        {
            Health = 0;
        }
        LogMaster.Log($"{Name} здоровье {Health}/{MaxHealth} (после)");
    }

    private void SetDamage(int value)
    {
        LogMaster.Log($"{Name} урон {Damage} (до)");
        Damage = value;
        if(Damage < 0)
        {
            Damage = 0;
        }
        LogMaster.Log($"{Name} урон {Damage} (после)");
    }

}