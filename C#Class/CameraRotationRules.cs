using UnityEngine;

public class Rotatation : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 0.5f;
  void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        turn.y += Input.GetAxis("Mouse X") * sensitivity;
        turn.x += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(-turn.x, turn.y, 0);
        
    }
}
