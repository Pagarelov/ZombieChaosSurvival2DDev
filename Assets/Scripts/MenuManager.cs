using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ChangeScene(string name) 
    {
        SceneManager.LoadScene(name);
    }
}
