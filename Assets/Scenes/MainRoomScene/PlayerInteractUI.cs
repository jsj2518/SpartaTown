using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    public void SetActivate(bool setVal)
    {
        if (setVal == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenInteractWithTutor()
    {
        uiController.DisableFloatingMenu(true);
        uiController.EnableInteractWithTutor();
    }
    public void DeathSequence(Action deathSequenceHalfCallback, Action deathSequenceEndCallback)
    {
        uiController.DeathSequence(deathSequenceHalfCallback, deathSequenceEndCallback);
    }

}
