using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{      
    private Color _mixedColor;

    public Color MixedColor { get => _mixedColor; set => _mixedColor = value; }

    protected void MixColors(List<Color> colors)
    {
        Color currentColor = new Color(0, 0, 0, 0);

        foreach (Color color in colors)
        {
            currentColor += color;
        }

        currentColor /= colors.Count;

        _mixedColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a * colors.Count);
    }

}
