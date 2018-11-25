using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed = 1.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, -15, 15),
          transform.position.y,
          transform.position.z);
    }
}
