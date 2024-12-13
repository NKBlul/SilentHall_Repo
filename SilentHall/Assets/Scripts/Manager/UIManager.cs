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

    [Header("Flashlight")]
    public GameObject flashlight;

    [Header("NumLock")]
    public GameObject numlockUI2d;
    public GameObject numlockUI3d;

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
        GameManager.instance.TogglePlayerMovement(true);
        GameManager.instance.HideCursor();
        numlockUI2d.SetActive(false);
        numlockUI3d.SetActive(false);
    }

    public void ActivateNumlockUI(NumLock numlock)
    {
        Transform playerPos = GameManager.instance.player.transform;
        UINumLock uiNumLock = numlockUI3d.GetComponent<UINumLock>();

        GameManager.instance.TogglePlayerMovement(false);
        GameManager.instance.ShowCursor();
        numlockUI2d.SetActive(true);
        numlockUI3d.SetActive(true);

        uiNumLock.password = numlock.password;

        // Position the 3D UI in front of the player
        Vector3 forwardPosition = playerPos.position + playerPos.forward * offset.z
                                  + playerPos.up * offset.y
                                  + playerPos.right * offset.x;
        numlockUI3d.transform.position = forwardPosition;

        // Rotate the UI to face the player
        //numlockUI3d.transform.rotation = Quaternion.LookRotation(numlockUI3d.transform.position - playerPos.position);
    }

    IEnumerator Timer(float time, TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(time);
        ClearText(text);
    }
}
