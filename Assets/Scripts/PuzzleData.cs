using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewPuzzle", menuName = "WordSearch/Puzzle")]
public class PuzzleData : ScriptableObject
{
    public int gridSize = 10;
    public List<WordEntry> words = new List<WordEntry>();
}

[System.Serializable]
public class WordEntry
{
    public string word;
    public string meaning;
}