using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject crosshair;

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

    [Header("Flashlight")]
    public GameObject flashlight;

    [Header("NumLock")]
    public GameObject numlockUI2d;
    public GameObject numlockUI3d;
    public Vector3 offset;

    [Header("Piano")]
    public GameObject pianoUI;

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

    public void Close(GameObject toClose)
    {
        GameManager.instance.TogglePlayerMovement(true);
        GameManager.instance.HideCursor();
        toClose.SetActive(false);
        //ClearText(paperText);
    }

    public void CloseNumlock()
    {
        UINumLock uiNumLock = numlockUI3d.GetComponent<UINumLock>();
        NumLock numLock = uiNumLock.numLock.GetComponent<NumLock>();

        GameManager.instance.TogglePlayerMovement(true);
        GameManager.instance.HideCursor();
        crosshair.SetActive(true);
        numlockUI2d.SetActive(false);
        numlockUI3d.SetActive(false);
        
        numLock.isInteracting = false;
    }

    public void ActivateNumlockUI(NumLock numlock)
    {
        Transform playerPos = GameManager.instance.player.transform;
        UINumLock uiNumLock = numlockUI3d.GetComponent<UINumLock>();

        ClearText(interactableText);
        GameManager.instance.TogglePlayerMovement(false);
        GameManager.instance.ShowCursor();

        crosshair.SetActive(false);

        numlockUI2d.SetActive(true);
        numlockUI3d.SetActive(true);

        uiNumLock.numLock = numlock.gameObject;
        uiNumLock.password = numlock.password;
    }

    IEnumerator Timer(float time, TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(time);
        ClearText(text);
    }
}
