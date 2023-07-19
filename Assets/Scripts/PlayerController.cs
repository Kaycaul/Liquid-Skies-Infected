using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rb;
    Weapon weapon;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        rb.AddForce(input * moveSpeed * Time.deltaTime);
    }

    public void KnockBack(Vector2 force) {
        rb.AddForce(force);
    }

}
