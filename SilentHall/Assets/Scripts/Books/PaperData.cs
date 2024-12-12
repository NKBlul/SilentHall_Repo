using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPaperData", menuName = "ScritableObject/PaperData")]
public class PaperData : ScriptableObject
{
    [TextArea(15, 20)]
    public string paperText;
}