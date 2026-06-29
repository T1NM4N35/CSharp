using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Trigger Entered by: " + other.name);
        
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player")) 
        {
            Debug.Log("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }