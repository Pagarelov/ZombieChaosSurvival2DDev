using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button exitButton;

    private void Start()
    {
        exitButton = GetComponent<Button>();

        exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {

        Application.Quit();
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveListener(ExitGame);
    }
}
