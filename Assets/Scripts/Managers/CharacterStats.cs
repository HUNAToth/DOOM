using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    public int healthLevel = 10;

    protected int maxHealth;

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    protected int currentHealth;

    public void SetCurrentHealth(int newCurrent)
    {
        currentHealth = newCurrent;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    [SerializeField]
    public int armorLevel = 10;

    protected int maxArmor;

    public int GetMaxArmor()
    {
        return maxArmor;
    }

    protected int currentArmor;

    public void SetCurrentArmor(int newCurrent)
    {
        currentArmor = newCurrent;
    }

    public int GetCurrentArmor()
    {
        return currentArmor;
    }
}
