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
        playerController.gameObject.SetActive(false);
        inputName_TMPInputField.GetComponent<TMP_InputField>().text = GameManager.Instance.PlayerName;

        uiController.DisableFloatingMenu(false);
        uiController.EnableChangePlayerName();
    }

    public void ApplyPlayerNameChange()
    {
        GameManager.Instance.PlayerName = inputName_TMPInputField.GetComponent<TMP_InputField>().text;
        playerController.ResetPlayerObject();

        uiController.EnableFloatingMenu();
        uiController.DisableChangePlayerName(true);

        playerController.gameObject.SetActive(true);
    }

    public void CancelPlayerNameChange()
    {
        uiController.EnableFloatingMenu();
        uiController.DisableChangePlayerName(true);

        playerController.gameObject.SetActive(true);
    }
}
