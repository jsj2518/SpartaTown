using UnityEngine;
using UnityEngine.UI;

public class CharacterChangeUI : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    [SerializeField] private GameObject CharacterImageHolder;
    [SerializeField] private Text TxtCharacterRace;

    [SerializeField] private PlayerInputController playerController;

    private static int CharacterSelect;

    public void OpenCharacterSelectMenu()
    {
        playerController.BlockControl(true);

        CharacterSelect = GameManager.Instance.CharacterSelect;
        uiController.GetCharacterImages()[CharacterSelect].gameObject.SetActive(true);
        TxtCharacterRace.text = GameManager.Instance.CharacterRace[CharacterSelect];

        uiController.DisableFloatingMenu(false);
        uiController.EnableChangeCharacterMenu();
    }

    public void MoveRight()
    {
        uiController.GetCharacterImages()[CharacterSelect].gameObject.SetActive(false);

        CharacterSelect++;
        if (CharacterSelect >= (int)PlayableCharacter.Max) CharacterSelect = 0;

        uiController.GetCharacterImages()[CharacterSelect].gameObject.SetActive(true);
        TxtCharacterRace.text = GameManager.Instance.CharacterRace[CharacterSelect];
    }
    public void MoveLeft()
    {
        uiController.GetCharacterImages()[CharacterSelect].gameObject.SetActive(false);

        CharacterSelect--;
        if (CharacterSelect < 0) CharacterSelect = (int)PlayableCharacter.Max - 1;

        uiController.GetCharacterImages()[CharacterSelect].gameObject.SetActive(true);
        TxtCharacterRace.text = GameManager.Instance.CharacterRace[CharacterSelect];
    }

    public void CloseCharacterSelectMenu()
    {
        GameManager.Instance.CharacterSelect = CharacterSelect;
        playerController.ResetPlayerObject();

        uiController.EnableFloatingMenu();
        uiController.DisableChangeCharacterMenu(true);

        playerController.BlockControl(false);
    }
}
