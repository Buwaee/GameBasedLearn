using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class WordSearchManager : MonoBehaviour
{
    public PuzzleData puzzleData;
    public ColorPalette palette;
    
    public GameObject cellPrefab;
    public Transform gridParent;
    
    public GameObject cluePrefab;
    public Transform cluesParent;
    
    public GameObject winPanel;
    public Button restartButton;
    
    private WordSearchCell[,] cells;
    private List<PlacedWord> placedWords = new List<PlacedWord>();
    private List<TextMeshProUGUI> clueTexts = new List<TextMeshProUGUI>();
    private List<bool> wordFound = new List<bool>();
    
    private bool isDragging = false;
    private WordSearchCell startCell;
    private WordSearchCell currentEndCell;
    private List<WordSearchCell> currentSelection = new List<WordSearchCell>();
    
    private static readonly Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, -1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1)
    };
    
    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        
        BuildGrid();
        PlaceWords();
        FillEmptyCells();
        DisplayClues();
    }
    
    void Update()
    {
        HandleMouseInput();
    }
    
    void BuildGrid()
    {
        int size = puzzleData.gridSize;
        cells = new WordSearchCell[size, size];
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                GameObject obj = Instantiate(cellPrefab, gridParent);
                WordSearchCell cell = obj.GetComponent<WordSearchCell>();
                cell.SetPosition(x, y);
                cells[x, y] = cell;
            }
        }
    }
    
    void PlaceWords()
    {
        List<int> sortedIndices = new List<int>();
        for (int i = 0; i < puzzleData.words.Count; i++)
            sortedIndices.Add(i);
        
        sortedIndices.Sort((a, b) => puzzleData.words[b].word.Length.CompareTo(puzzleData.words[a].word.Length));
        
        int maxRetries = 50;
        for (int retry = 0; retry < maxRetries; retry++)
        {
            ResetGrid();
            bool allPlaced = true;
            
            foreach (int wordIdx in sortedIndices)
            {
                string word = puzzleData.words[wordIdx].word.ToUpper();
                if (!TryPlaceWord(word, wordIdx))
                {
                    allPlaced = false;
                    break;
                }
            }
            
            if (allPlaced) return;
        }
        
        Debug.LogError("Could not place all words after " + maxRetries + " retries");
    }
    
    void ResetGrid()
    {
        placedWords.Clear();
        wordFound.Clear();
        int size = puzzleData.gridSize;
        for (int y = 0; y < size; y++)
            for (int x = 0; x < size; x++)
                cells[x, y].SetLetter('\0');
    }
    
    bool TryPlaceWord(string word, int wordIdx)
    {
        int size = puzzleData.gridSize;
        int attempts = 0;
        
        while (attempts < 1000)
        {
            attempts++;
            Vector2Int dir = directions[Random.Range(0, directions.Length)];
            int startX = Random.Range(0, size);
            int startY = Random.Range(0, size);
            
            if (CanPlaceWord(word, startX, startY, dir))
            {
                PlaceWord(word, startX, startY, dir, wordIdx);
                return true;
            }
        }
        return false;
    }
    
    bool CanPlaceWord(string word, int startX, int startY, Vector2Int dir)
    {
        int size = puzzleData.gridSize;
        
        for (int i = 0; i < word.Length; i++)
        {
            int x = startX + dir.x * i;
            int y = startY + dir.y * i;
            
            if (x < 0 || x >= size || y < 0 || y >= size)
                return false;
            
            char existing = cells[x, y].letter;
            if (existing != '\0' && existing != word[i])
                return false;
        }
        return true;
    }
    
    void PlaceWord(string word, int startX, int startY, Vector2Int dir, int wordIndex)
    {
        List<WordSearchCell> wordCells = new List<WordSearchCell>();
        
        for (int i = 0; i < word.Length; i++)
        {
            int x = startX + dir.x * i;
            int y = startY + dir.y * i;
            cells[x, y].SetLetter(word[i]);
            wordCells.Add(cells[x, y]);
        }
        
        placedWords.Add(new PlacedWord
        {
            word = word,
            wordIndex = wordIndex,
            cells = wordCells,
            startPos = new Vector2Int(startX, startY),
            direction = dir
        });
        
        wordFound.Add(false);
    }
    
    void FillEmptyCells()
    {
        int size = puzzleData.gridSize;
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (cells[x, y].letter == '\0')
                    cells[x, y].SetLetter(letters[Random.Range(0, letters.Length)]);
            }
        }
    }
    
    void DisplayClues()
    {
        for (int i = 0; i < puzzleData.words.Count; i++)
        {
            GameObject obj = Instantiate(cluePrefab, cluesParent);
            TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
            text.text = (i + 1) + ". " + puzzleData.words[i].meaning;
            clueTexts.Add(text);
        }
    }
    
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WordSearchCell cell = GetCellUnderMouse();
            if (cell != null)
            {
                isDragging = true;
                startCell = cell;
                currentEndCell = cell;
                UpdateSelection();
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            WordSearchCell cell = GetCellUnderMouse();
            if (cell != null && cell != currentEndCell)
            {
                currentEndCell = cell;
                UpdateSelection();
            }
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            bool found = CheckWord();
            if (!found)
                ClearHighlights();
            ClearSelection();
        }
    }
    
    WordSearchCell GetCellUnderMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Camera cam = null;
        Canvas canvas = gridParent.GetComponentInParent<Canvas>();
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera;
        
        int size = puzzleData.gridSize;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                RectTransform rt = cells[x, y].GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(rt, mousePos, cam))
                    return cells[x, y];
            }
        }
        return null;
    }
    
    void UpdateSelection()
    {
        ClearHighlights();
        currentSelection.Clear();
        
        if (startCell == null || currentEndCell == null) return;
        
        int dx = currentEndCell.gridX - startCell.gridX;
        int dy = currentEndCell.gridY - startCell.gridY;
        
        Vector2Int dir = Vector2Int.zero;
        int steps = 0;
        
        if (dx == 0 && dy == 0)
        {
            currentSelection.Add(startCell);
            startCell.SetHighlight(palette.highlightColor);
            return;
        }
        else if (dx == 0)
        {
            dir = new Vector2Int(0, dy > 0 ? 1 : -1);
            steps = Mathf.Abs(dy);
        }
        else if (dy == 0)
        {
            dir = new Vector2Int(dx > 0 ? 1 : -1, 0);
            steps = Mathf.Abs(dx);
        }
        else if (Mathf.Abs(dx) == Mathf.Abs(dy))
        {
            dir = new Vector2Int(dx > 0 ? 1 : -1, dy > 0 ? 1 : -1);
            steps = Mathf.Abs(dx);
        }
        else
        {
            currentSelection.Add(startCell);
            startCell.SetHighlight(palette.highlightColor);
            return;
        }
        
        for (int i = 0; i <= steps; i++)
        {
            int x = startCell.gridX + dir.x * i;
            int y = startCell.gridY + dir.y * i;
            if (x >= 0 && x < puzzleData.gridSize && y >= 0 && y < puzzleData.gridSize)
            {
                currentSelection.Add(cells[x, y]);
                cells[x, y].SetHighlight(palette.highlightColor);
            }
        }
    }
    
    void ClearHighlights()
    {
        foreach (var cell in currentSelection)
            cell.ClearHighlight();
    }
    
    void ClearSelection()
    {
        currentSelection.Clear();
        startCell = null;
        currentEndCell = null;
    }
    
    bool CheckWord()
    {
        if (currentSelection.Count < 2) return false;
        
        StringBuilder sb = new StringBuilder();
        foreach (var cell in currentSelection)
            sb.Append(cell.letter);
        
        string selected = sb.ToString();
        string reversed = ReverseString(selected);
        
        for (int i = 0; i < placedWords.Count; i++)
        {
            if (wordFound[i]) continue;
            
            if (placedWords[i].word == selected || placedWords[i].word == reversed)
            {
                MarkWordFound(i);
                return true;
            }
        }
        return false;
    }
    
    string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        System.Array.Reverse(arr);
        return new string(arr);
    }
    
    void MarkWordFound(int index)
    {
        wordFound[index] = true;
        Color c = palette.wordColors[placedWords[index].wordIndex];
        
        foreach (var cell in placedWords[index].cells)
            cell.MarkAsFound(c);
        
        clueTexts[placedWords[index].wordIndex].color = c;
        clueTexts[placedWords[index].wordIndex].text += "  (" + placedWords[index].word + ")";
        
        CheckWinCondition();
    }
    
    void CheckWinCondition()
    {
        foreach (bool found in wordFound)
            if (!found) return;
        
        if (winPanel != null) winPanel.SetActive(true);
    }
    
    void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

[System.Serializable]
public class PlacedWord
{
    public string word;
    public int wordIndex;
    public List<WordSearchCell> cells;
    public Vector2Int startPos;
    public Vector2Int direction;
}