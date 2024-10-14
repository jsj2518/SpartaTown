using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemberViewChangeUI : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    public void OpenMemberViewMenu()
    {
        uiController.DisableMemberBtnMenu(true);
        uiController.EnableMemberViewMenu();
    }

    public void CloseMemberViewMenu()
    {
        uiController.EnableMemberBtnMenu();
        uiController.DisableMemberViewMenu(true);
    }
}
