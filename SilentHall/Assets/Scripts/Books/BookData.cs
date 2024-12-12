using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBookData", menuName = "ScritableObject/BookData")]
public class BookData : ScriptableObject
{
    public string bookTitle;
    public List<string> pages; // Each string represents one page of content
}