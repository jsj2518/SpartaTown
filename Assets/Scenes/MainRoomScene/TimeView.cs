using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeView : MonoBehaviour
{
    private Text timeTxt;

    private void Awake()
    {
        timeTxt = GetComponent<Text>();
    }

    private void Update()
    {
        timeTxt.text = DateTime.Now.ToString("HH:mm");
    }
}
