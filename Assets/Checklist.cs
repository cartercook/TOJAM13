using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Checklist : MonoBehaviour
{
    public string title = "Checklist";

    public string bullet = " • ";

    public string[] tasks;

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        updateText();
    }

    void OnEnable()
    {
        updateText();
    }

    private void updateText()
    {
        string separator = $"\n{bullet}";
        string joinedTasks = string.Join(separator, tasks);
        text.text = $"{title}{separator}{joinedTasks}";
    }
}
