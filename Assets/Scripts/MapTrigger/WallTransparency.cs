using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTransparency : MonoBehaviour
{
    [SerializeField] Tilemap GridWallTransparent;

    private float currentTransparency = 1.0f;
    private float gotoTransparency = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gotoTransparency = 0.6f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gotoTransparency = 1.0f;
    }

    private void Update()
    {
        if (currentTransparency != gotoTransparency)
        {
            if (currentTransparency < gotoTransparency)
            {
                currentTransparency += 0.001f;
                if (currentTransparency > gotoTransparency) currentTransparency = gotoTransparency;
            }
            else
            {
                currentTransparency -= 0.001f;
                if (currentTransparency < gotoTransparency) currentTransparency = gotoTransparency;
            }
            Color color = GridWallTransparent.color;
            color.a = currentTransparency;
            GridWallTransparent.color = color;
        }
    }
}
