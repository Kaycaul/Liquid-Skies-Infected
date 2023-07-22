using UnityEngine;

public interface IHitTarget {
    public void Hit(int damage, Vector2 knockback) { }
}
