using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    PlayerAnimatorManager animatorManager;
    WeaponSlotManager weaponSlotManager;
    public string lastAttack;

    private void Awake()
    {
        animatorManager = GetComponent<PlayerAnimatorManager>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }    

    public void HandleLightAttack(WeaponItem weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
        lastAttack = weapon.OH_Light_Attack_1;
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        lastAttack = weapon.OH_Heavy_Attack_1;
    }

    public void OpenRightDamageCollider()
    {
        weaponSlotManager.OpenRightDamageCollider();
    }

    public void CloseRightDamageCollider()
    {
        weaponSlotManager.CloseRightDamageCollider();
    }

}
