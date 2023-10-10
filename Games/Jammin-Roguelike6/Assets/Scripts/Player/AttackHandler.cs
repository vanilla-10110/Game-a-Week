using UnityEngine;
public class AttackHandler : MonoBehaviour
{
    [Header("Objects")]
    Animator animator;
    public GameObject weaponHolder;
    int selectedWeapon = 0;
    public bool isAttacking;


    [Header("Stats")]



    //[HideInInspector]
    public float[] Stats =
    {
        5f,
        1f,
        30f,
        0f,
        0f,
        0f,
        0f,
        0f
    };


    void SwitchWeapon()
    {
        int i = 0;

        foreach (Transform weapon in weaponHolder.transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
    }

    string GetActiveWeaponName()
    {
        foreach (Transform child in weaponHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                
                return child.name;

            }
        }
        return "None";
    }       
    Animator GetActiveWeaponAnimator()
    {
        foreach (Transform child in weaponHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                animator = child.GetComponent<Animator>();
                
                return animator;
            }
        }
        return null;
    }


    void Update()
    {
        AnimatorStateInfo currentState = GetActiveWeaponAnimator().GetCurrentAnimatorStateInfo(0);
        //while (currentState.IsName("swordIdle1") || currentState.IsName("swordIdle2") || currentState.IsName("swordIdle3") || currentState.IsName("swordIdle4"))
        //{
        //    isAttacking = false;
        //}
            if (Input.GetMouseButton(0))
        {
            Attack();
        }
        


        int previousWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= weaponHolder.transform.childCount - 1)
            {
                selectedWeapon = 0;
            }   else 
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weaponHolder.transform.childCount - 1;
            }   else
                selectedWeapon--;
            
        }

        if (previousWeapon != selectedWeapon)
            SwitchWeapon();

    }




    public void Attack()
    {

        if (GetActiveWeaponName() == "staff")
        {

            AnimatorStateInfo currentState = GetActiveWeaponAnimator().GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("staffIdle1"))
            {
                GetActiveWeaponAnimator().Play($"staffAttack{Random.Range(1, 4)}");
            }           
        }


        if (GetActiveWeaponName() == "sword")
        {
            

            int attackIndex = -1;
            AnimatorStateInfo currentState = GetActiveWeaponAnimator().GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("swordIdle1"))
            {
                attackIndex = 0;
            }
            else if (currentState.IsName("swordIdle2"))
            {
                attackIndex = 1;
            }
            else if (currentState.IsName("swordIdle3"))
            {
                attackIndex = 2;
            }
            else if (currentState.IsName("swordIdle4"))
            {
                attackIndex = 3;
            }
            if (attackIndex != -1)
            {

                GetActiveWeaponAnimator().speed = Stats[1];
                isAttacking = true;
                GetActiveWeaponAnimator().Play($"swordAttack{attackIndex + 1}");
                
            }
        }
    }
}
