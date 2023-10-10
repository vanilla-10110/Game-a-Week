using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isAttacking : MonoBehaviour
{
    AttackHandler attackHandler;

    private void Awake()
    {
        attackHandler = GameObject.Find("Platyer").GetComponent<AttackHandler>();
    }


    public void ResetAttack()
    {
        attackHandler.isAttacking = false;
    }
}

