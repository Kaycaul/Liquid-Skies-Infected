using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Weapon : MonoBehaviour {

    [SerializeField] AnimationCurve swingCurve;
    [SerializeField] float swingDuration = 1f;
    [SerializeField] float swingArc = 60f;

    bool attacking = false;
    int swingDirection = -1;
    Vector2 mousePos;

    private void Update() {
        // point towards the mouse
        if (!attacking) PointTowardsMouse();
    }

    private void PointTowardsMouse() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleToMouse = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleToMouse);
    }

    public async void Attack() {
        if (attacking) return;
        attacking = true;
        // knock the player back a bit
        Vector2 knockbackVector = (Vector2)transform.position - mousePos;
        FindObjectOfType<PlayerController>().KnockBack(25 * knockbackVector.normalized);
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
