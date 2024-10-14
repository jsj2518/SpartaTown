using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithTutorUI : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    [SerializeField] private PlayerInputController playerController;

    public void CloseInteractWithTutor()
    {
        uiController.EnableFloatingMenu();
        uiController.DisableInteractWithTutor(true);

        playerController.BlockControl(false);
    }
}
