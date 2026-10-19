using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 8f;
    public KeyCode upKey;
    public KeyCode downKey;

    void Update()
    {
        float movement = 0f;

        if (Input.GetKey(upKey))
            movement = 1f;

        if (Input.GetKey(downKey))
            movement = -1f;

        transform.position +=
            Vector3.up * movement * speed * Time.deltaTime;

        float y = Mathf.Clamp(transform.position.y, -3.5f, 3.5f);
        transform.position = new Vector3(
            transform.position.x, y, transform.position.z
        );
    }
}