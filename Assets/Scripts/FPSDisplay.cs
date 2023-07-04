using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f; // Значение времени между обновлениями кадров
    GUIStyle style; // Стиль текста для отображения FPS
    bool showFPS = true; // Флаг для определения, нужно ли отображать FPS

    private void Start()
    {
        style = new GUIStyle(); // Создание нового стиля текста
        style.alignment = TextAnchor.UpperLeft; // Выравнивание текста по левому верхнему углу
        style.fontSize = 20; // Размер шрифта
        style.normal.textColor = Color.white; // Цвет текста
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f; // Рассчитываем среднее время обновления кадров

        if (Input.GetKeyDown(KeyCode.F))
        {
            showFPS = !showFPS; // При нажатии клавиши F переключаем флаг отображения FPS
        }
    }

    private void OnGUI()
    {
        if (!showFPS) return; // Если флаг отображения FPS выключен, выходим

        float fps = 1.0f / deltaTime; // Вычисляем FPS

        GUI.Label(new Rect(10, 10, 200, 20), "FPS: " + Mathf.Round(fps).ToString(), style); // Отображаем FPS на экране
    }
}
