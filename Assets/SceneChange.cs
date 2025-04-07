using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public List<string> possibleScenes; //A list of possible scenes to load
    private string sceneToLoad; // The name of the scene to load

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            while(sceneToLoad == null || sceneToLoad == SceneManager.GetActiveScene().name)
            {
                //choose a random scene from the list
                sceneToLoad = possibleScenes[Random.Range(0, possibleScenes.Count)];
            }
            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
