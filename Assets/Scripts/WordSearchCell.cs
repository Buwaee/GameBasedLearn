using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordSearchCell : MonoBehaviour
{
    public TextMeshProUGUI letterText;
    public Image background;
    
    public int gridX;
    public int gridY;
    public char letter;
    
    private Color baseColor = Color.white;
    private bool isFound = false;
    
    public void SetLetter(char c)
    {
        letter = c;
        letterText.text = c.ToString();
    }
    
    public void SetPosition(int x, int y)
    {
        gridX = x;
        gridY = y;
    }
    
    public void SetHighlight(Color c)
    {
        if (!isFound)
            background.color = c;
    }
    
    public void ClearHighlight()
    {
        if (!isFound)
            background.color = baseColor;
    }
    
    public void MarkAsFound(Color c)
    {
        isFound = true;
        background.color = c;
    }
    
    public bool IsFound()
    {
        return isFound;
    }
}