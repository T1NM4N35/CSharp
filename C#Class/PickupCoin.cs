using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
    {
        // Tell the manager to add a coin to the count
        GameManager.instance.AddCoin();
        
        Debug.Log("Coin collected! UI Updated.");
        Destroy(gameObject); 
    }
}
}