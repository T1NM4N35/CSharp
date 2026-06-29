using UnityEngine;

public class Camera : MonoBehaviour
{
        public Transform Player;
        public float pLerp = 0.2F;
        public float rLerp = 0.2F;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, Player.rotation, rLerp);
    }
}
