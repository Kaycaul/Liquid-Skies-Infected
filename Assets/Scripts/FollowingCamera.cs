using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

    public bool followPlayer = true;
    [SerializeField] Transform player;

    Vector2 targetPosition;
    Vector2 velocity;

    private void FixedUpdate() {
        if (followPlayer) targetPosition = player.position;
        // smoothdamp towards the player
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, 0.25f);
        transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
    }

}
