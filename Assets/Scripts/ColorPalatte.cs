using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "WordSearch/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    public Color[] wordColors = new Color[10];
    public Color highlightColor = new Color(0.7f, 0.85f, 1f, 1f);
    public Color wrongColor = new Color(1f, 0.5f, 0.5f, 1f);
    public Color defaultCellColor = Color.white;
}