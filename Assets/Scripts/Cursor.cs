using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    void Update() {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPosition;
        transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * 360);
    }

}
