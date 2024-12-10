using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterData;
    public AttackData_SO attackData_SO;

    public int MaxHealth {
        get { if (characterData != null) { return characterData.maxHealth; } else return 0; }
        set { characterData.maxHealth = value; }
    }
    public int CurrentHealth
    {
        get { if (characterData != null) { return characterData.currentHealth; } else return 0; }
        set { characterData.currentHealth = value; }
    }
    public int GetAttackValue
    {
        get { if (attackData_SO != null) { return attackData_SO.GatattackValue; } else return 0; }
        set { attackData_SO.GatattackValue = value; }
    }

    public float AttackRange
    {
        get { if (attackData_SO != null) { return attackData_SO.attackRange; } else return 0; }
        set { attackData_SO.attackRange = value; }
    }

    public void GetDamage(CharacterStats attacker)
    {
        int damage = attacker.GetAttackValue;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        //TODO:Updata UI
    }

    public void GetDoubleDamage(CharacterStats attacker)
    {
        int damage = attacker.GetAttackValue * 2;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        //TODO:Updata UI
    }
    public void GetFourDamage(CharacterStats attacker)
    {
        int damage = attacker.GetAttackValue * 4;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        //TODO:Updata UI
    }
}
