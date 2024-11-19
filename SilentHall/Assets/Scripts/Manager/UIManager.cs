using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI interactableText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeText(TextMeshProUGUI text, string newText)
    {
        text.text = newText;
    }

    public void ClearText(TextMeshProUGUI text)
    {
        text.text = "";
    }
}
