using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Weapon : MonoBehaviour {

    [SerializeField] AnimationCurve swingCurve;
    [SerializeField] float swingDuration = 1f;
    [SerializeField] float swingArc = 60f;
    [SerializeField] SwordSwing swordSwing;
    [SerializeField] float knockbackForce = 2000f;
    [SerializeField] int damage = 1;
    [SerializeField] float cooldownAfterThirdAttack = 1.8f;

    bool attacking = false;
    int swingDirection = -1;
    Vector2 mousePos;
    int recentAttackCount = 0;
    float timeOfLastAttack = 0;

    private void Update() {
        // point towards the mouse
        if (!attacking) PointTowardsMouse();
        // reset attack cooldown
        if (Time.time - timeOfLastAttack > cooldownAfterThirdAttack) recentAttackCount = 0;
    }

    private void PointTowardsMouse() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleToMouse = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleToMouse);
    }

    public async void Attack() {
        if (attacking) return;
        if (recentAttackCount > 2) return;
        recentAttackCount++;
        timeOfLastAttack = Time.time;
        attacking = true;
        // knock the player back a bit
        Vector2 knockbackVector = (Vector2)transform.position - mousePos;
        knockbackVector.Normalize();
        int knockbackAmount = 60 - (recentAttackCount * 40);
        FindObjectOfType<PlayerController>().KnockBack(knockbackAmount * knockbackVector);
        // spawn the sword swing and assign its values
        SwordSwing newSwordSwing = Instantiate(swordSwing, transform.position, transform.rotation);
        newSwordSwing.transform.position += (Vector3)(-knockbackVector);
        newSwordSwing.knockback = -knockbackVector * knockbackForce;
        newSwordSwing.damage = damage;
        // play the attack animation
        float t = 0;
        float startingAngle = transform.rotation.eulerAngles.z - swingArc / 2 * swingDirection;
        float rotation = 0;
        while (t < 1) {
            // wait until the weaponcontroller sets rotation, then add to it
            await UniTask.DelayFrame(1);
            rotation = swingCurve.Evaluate(t) * swingArc;
            transform.rotation = Quaternion.Euler(0, 0, startingAngle + rotation * swingDirection);
            t += Time.deltaTime / swingDuration;
        }
        swingDirection = -swingDirection;
        attacking = false;
    }

}
