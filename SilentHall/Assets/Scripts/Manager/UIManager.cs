using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Texts")]
    public TextMeshProUGUI interactableText;
    public TextMeshProUGUI extraText;

    [Header("Stamina")]
    public GameObject stamina;
    public Image staminaBar;

    [Header("Book and paper")]
    public GameObject book;
    public GameObject paper;
    public TextMeshProUGUI paperText;

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

    public void ReadPaper(PaperData paperData)
    {
        GameManager.instance.TogglePlayerMovement(false);
        GameManager.instance.ShowCursor();
        paper.SetActive(true);
        ChangeText(paperText, paperData.paperText);     
    }

    public void Close()
    {
        GameManager.instance.TogglePlayerMovement(true);
        GameManager.instance.HideCursor();
        paper.SetActive(false);
        ClearText(paperText);
    }

    IEnumerator Timer(float time, TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(time);
        ClearText(text);
    }
}
