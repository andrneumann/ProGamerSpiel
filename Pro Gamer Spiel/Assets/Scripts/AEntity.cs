﻿using UnityEngine;
using System.Collections;

public abstract class AEntity : MonoBehaviour 
{
    protected AWeapon m_weapon;

    public int m_MaxHP;
    public int m_CurrentHP;

    protected virtual void Awake()
    {
        m_weapon = GetComponent<AWeapon>();
        m_CurrentHP = m_MaxHP;
    }

    protected virtual void TakeDamage(int _amount)
    {
        m_CurrentHP -= _amount;
        if (m_CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
