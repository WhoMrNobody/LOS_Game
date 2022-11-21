using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healtPoints = 100f;
    bool isDead = false;

   public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        healtPoints = Mathf.Max(healtPoints - damage, 0);

        if(healtPoints == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        if (isDead) return;

        isDead = true;
        //GetComponent<ActionSchedule>().CancelCurrentAction();
        Destroy(gameObject);
        
    }
}
