using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piano : MonoBehaviour
{
    [SerializeField] List<GameObject> notes = new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject note in notes) 
        {
            Button button = note.GetComponent<Button>();
            button.onClick.AddListener(delegate { PlayNote(note.name); }) ;
        }

        gameObject.SetActive(false);
    }

    void PlayNote(string note)
    {
        AudioManager.instance.PlaySFX(note);
        PuzzleManager.instance.AddChord(note);
    }

    public List<GameObject> GetNotes()
    {
        return notes;
    }
}
