using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button exitButton;
    private void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveListener(ExitGame);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("Exit");
        ExitGame();
    }

    private void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
