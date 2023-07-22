using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitTarget {

    [SerializeField] int health = 5;

    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Hit(int damage, Vector2 knockback) {
        health -= damage;
        rb.AddForce(knockback);
    }

}
