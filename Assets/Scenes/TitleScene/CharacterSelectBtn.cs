using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectBtn : MonoBehaviour
{
    [SerializeField] private GameObject CharacterImageHolder;
    [SerializeField] private Text TxtCharacterRace;

    private Image[] characterImages;


    // Start is called before the first frame update
    void Start()
    {
        Image[] tempImages = CharacterImageHolder.GetComponentsInChildren<Image>(true);
        characterImages = new Image[tempImages.Length - 1];
        for (int i = 0; i < tempImages.Length - 1; i++)
        {
            characterImages[i] = tempImages[i + 1];
        }
    }

    public void MoveRight()
    {
        int characterSelect = GameManager.Instance.CharacterSelect;
        characterImages[characterSelect].gameObject.SetActive(false);

        characterSelect++;
        if (characterSelect >= (int)PlayableCharacter.Max) characterSelect = 0;

        characterImages[characterSelect].gameObject.SetActive(true);
        GameManager.Instance.CharacterSelect = characterSelect;
        TxtCharacterRace.text = GameManager.Instance.CharacterRace[characterSelect];
    }
    public void MoveLeft()
    {
        int characterSelect = GameManager.Instance.CharacterSelect;
        characterImages[characterSelect].gameObject.SetActive(false);

        characterSelect--;
        if (characterSelect < 0) characterSelect = (int)PlayableCharacter.Max - 1;

        characterImages[characterSelect].gameObject.SetActive(true);
        GameManager.Instance.CharacterSelect = characterSelect;
        TxtCharacterRace.text = GameManager.Instance.CharacterRace[characterSelect];
    }
}
