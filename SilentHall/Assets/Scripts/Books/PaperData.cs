using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPaperData", menuName = "ScritableObject/PaperData")]
public class PaperData : ScriptableObject
{
    [TextArea(15, 20)]
    public string paperText;
}

//[CustomEditor(typeof(PaperData))]
//public class PaperDataEditor : Editor
//{
//    SerializedProperty textBox;
//
//    private void OnEnable()
//    {
//        textBox = serializedObject.FindProperty("paperText");
//    }
//
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//
//
//    }
//}