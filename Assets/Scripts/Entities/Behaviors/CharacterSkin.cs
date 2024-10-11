using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkin : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] skins;
    private SpriteRenderer currentSkin;
    private Animator currentSkinAnimator;

    public void SetSkin()
    {
        int characterSelect = GameManager.Instance.CharacterSelect;

        if (transform.childCount > 0)
        {
            Transform oldSkin = transform.GetChild(0);
            Destroy(oldSkin.gameObject);
        }
        
        currentSkin = Instantiate(skins[characterSelect]);
        currentSkin.transform.parent = transform;
        if (characterSelect == 0) currentSkin.transform.localPosition = new Vector3(0, 0.2f, 0);
        else currentSkin.transform.localPosition = Vector3.zero;
        currentSkinAnimator = currentSkin.GetComponent<Animator>();
    }

    public void SetFlipX(bool flipX)
    {
        if (currentSkin) currentSkin.flipX = flipX;
    }

    public Animator GetAnimator()
    {
        return currentSkinAnimator;
    }
}
