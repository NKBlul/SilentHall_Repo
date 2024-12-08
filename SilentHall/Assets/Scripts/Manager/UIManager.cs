using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI interactableText;
    public TextMeshProUGUI extraText;

    public GameObject stamina;
    public Image staminaBar;
    public GameObject book;

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

    public void ChangeText(float time, TextMeshProUGUI text, string newText)
    {
        text.text = newText;
        StartCoroutine(Timer(time, text));
    }

    public void ClearText(TextMeshProUGUI text)
    {
        text.text = "";
    }

    public void UpdateStaminaBar(float fill, float total)
    {
        ActivateStamina(true);
        staminaBar.fillAmount = fill / total;
    }

    public void ActivateStamina(bool active)
    {
        stamina.SetActive(active);
    }

    IEnumerator Timer(float time, TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(time);
        ClearText(text);
    }
}
