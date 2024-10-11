using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    private TextMesh titleMesh;
    private int color = 0;

    private void Start()
    {
        titleMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        color++;
        if (color >= 7650) color = 0;
        int slowColor = color / 10;
        float r = 0, g = 0, b = 0;
        if (slowColor < 255)
        {
            r = 255 - slowColor;
            g = slowColor;
        }
        else if (slowColor < 510)
        {
            g = 510 - slowColor;
            b = slowColor - 255;
        }
        else if (slowColor < 765)
        {
            b = 765 - slowColor;
            r = slowColor - 510;
        }

        Color newColor = new Color(r / 255f, g / 255f, b / 255f);

        titleMesh.color = newColor;
    }
}
