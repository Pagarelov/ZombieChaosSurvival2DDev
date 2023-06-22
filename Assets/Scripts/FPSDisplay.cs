using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style;
    bool showFPS = true;

    private void Start()
    {
        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = 20;
        style.normal.textColor = Color.white;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Input.GetKeyDown(KeyCode.F))
        {
            showFPS = !showFPS;
        }
    }

    private void OnGUI()
    {
        if (!showFPS) return;

        float fps = 1.0f / deltaTime;

        GUI.Label(new Rect(10, 10, 200, 20), "FPS: " + Mathf.Round(fps).ToString(), style);
    }
}
