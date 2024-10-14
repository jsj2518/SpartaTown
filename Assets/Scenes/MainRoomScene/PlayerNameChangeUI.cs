using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameChangeUI : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    [SerializeField] private GameObject inputName_TMPInputField;

    [SerializeField] private PlayerInputController playerController;

    public void OpenPlayerNameChangeMenu()
    {
        playerController.BlockControl(true);
        inputName_TMPInputField.GetComponent<TMP_InputField>().text = GameManager.Instance.PlayerName;

        uiController.DisableFloatingMenu(false);
        uiController.EnableChangePlayerName();
    }

    public void ApplyPlayerNameChange()
    {
        GameManager.Instance.PlayerName = inputName_TMPInputField.GetComponent<TMP_InputField>().text;
        playerController.ResetPlayerObject();
        string newMemberTxt = $"¼ÛÁö¿ø Unity Æ©ÅÍ\n{GameManager.Instance.PlayerName}";
        uiController.SetMemberViewText(newMemberTxt);

        uiController.EnableFloatingMenu();
        uiController.DisableChangePlayerName(true);

        playerController.BlockControl(false);
    }

    public void CancelPlayerNameChange()
    {
        uiController.EnableFloatingMenu();
        uiController.DisableChangePlayerName(true);

        playerController.BlockControl(false);
    }
}
