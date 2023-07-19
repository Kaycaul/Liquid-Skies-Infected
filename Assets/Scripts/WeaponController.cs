using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    [SerializeField] Weapon equippedWeapon;

    void Update() {
        // tell the weapon if the player is clicking
        if (Input.GetMouseButtonDown(0)) equippedWeapon.Attack();
        // todo: tell the weapon to throw/drop ball
    }
}
