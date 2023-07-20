using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float dashFullForce = 800f;
    [SerializeField] float dashChargeTime = 0.68f;
    [SerializeField] AnimationCurve dashForceCurve;

    float speedMultiplier = 1f;
    bool dashing = false;

    Rigidbody2D rb;
    Weapon weapon;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Dash();
        }
    }

    async void Dash() {
        if (dashing) return;
        dashing = true;
        // charge a dash
        speedMultiplier = 0.5f;
        float timeSpentCharging = 0;
        while (Input.GetKey(KeyCode.Space)) {
            timeSpentCharging += Time.deltaTime;
            await UniTask.Yield();
        }
        timeSpentCharging = Mathf.Min(timeSpentCharging, dashChargeTime);
        // dash the player
        speedMultiplier = 0f;
        Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float forcePercent = dashForceCurve.Evaluate(timeSpentCharging / dashChargeTime);
        rb.AddForce(mouseVector.normalized * dashFullForce * forcePercent);
        // reset control
        speedMultiplier = 1f;
        dashing = false;
    }

    void FixedUpdate() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        rb.AddForce(input * moveSpeed * speedMultiplier * Time.deltaTime);
    }

    public void KnockBack(Vector2 force) {
        rb.AddForce(force);
    }

}
