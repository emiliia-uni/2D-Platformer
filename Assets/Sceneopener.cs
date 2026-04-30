using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneopener : MonoBehaviour
{
    public string sceneName;
   public void OpenScene() 
    {
        SceneManager.LoadScene(sceneName);
    
    }

    
}
