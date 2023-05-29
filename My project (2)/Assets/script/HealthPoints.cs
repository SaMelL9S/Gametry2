using UnityEngine;
using System.Collections;

public class HealthPoints: MonoBehaviour
{
    public float HP;
    public float MaxHP;

    public void AddDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;

            Destroy(gameObject); // удаляем объект
        }
    }

    public void SetHealth(float healHP)
    { 
    HP += healHP;
        if (HP > MaxHP) 
        {
        HP = MaxHP;
        }
    
    }
}

