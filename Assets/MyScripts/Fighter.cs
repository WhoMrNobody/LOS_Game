using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour, IAction
{
    [SerializeField] float attackRange = 2f;
    [SerializeField] float timeBetweenAttack = 1f;
    [SerializeField] float attackDamage = 5f;

    Health _target;
    float _timeSinceLastAttack = Mathf.Infinity;
    
    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;

        if (_target == null) return; // Oyuncu Health componenti yok ya da ölmüþ ise geri dön
        if (_target.IsDead()) return;

        if (!GetIsInRange())
        {
            GetComponent<Mover>().MoveTo(_target.transform.position, 1f);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            AttackBehaviour();
        }
        
    }

    void AttackBehaviour()
    {
        transform.LookAt(_target.transform);

        if(_timeSinceLastAttack > timeBetweenAttack)
        {
            TriggerAttack();
            _timeSinceLastAttack = 0f;
        }
    }

    void TriggerAttack()
    {
        //Saldýrý animasyonlarý
    }

    public void Attack(GameObject combatTarget)
    {
        GetComponent<ActionSchedule>().StartAction(this);
        _target = combatTarget.GetComponent<Health>();
    }

    public bool CanAttack(GameObject combatTarget)
    {
        if(combatTarget == null) return false;

        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.IsDead();
    }

    bool GetIsInRange()
    {
        return Vector3.Distance(transform.position, _target.transform.position) < attackRange;
    }

    public void Cancel()
    {
        StopAttack();
        _target = null;
       
    }

    void StopAttack()
    {
        //Saldýrý bitirme animasyonlarý
    }

}
