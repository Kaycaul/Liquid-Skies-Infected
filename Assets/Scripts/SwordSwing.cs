using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SwordSwing : MonoBehaviour {

    [SerializeField] float duration = 0.6f;

    [HideInInspector] public float knockbackForce;
    [HideInInspector] public int damage;
    [HideInInspector] public Vector2 knockbackDirection;

    List<Collider2D> hitEnemies = new List<Collider2D>();

    private async void Start() {
        await UniTask.Delay((int)(duration * 1000));
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (hitEnemies.Contains(other)) return;
        hitEnemies.Add(other);
        other.GetComponent<Enemy>().Hit(damage, knockbackDirection * knockbackForce);
    }

}
