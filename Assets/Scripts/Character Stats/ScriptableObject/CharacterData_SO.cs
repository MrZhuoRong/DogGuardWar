using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Data",menuName ="Character Stats/CharacterData")]

public class CharacterData_SO : ScriptableObject//数值文件
{
    public int maxHealth;
    public int currentHealth;

}
