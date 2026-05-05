using UnityEngine;
using UnityEngine.SceneManagement;

public class killVolume : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
