using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;

public class UIController : MonoBehaviour
{
    [Header("Floating Menu")]
    [SerializeField] private RectTransform FloatingMenu;
    [SerializeField] private CanvasGroup FloatingMenuCanvas;

    [Header("Change Character Menu")]
    [SerializeField] private RectTransform ChangeCharacterMenu;
    [SerializeField] private CanvasGroup ChangeCharacterCanvas;
    [SerializeField] private GameObject CharacterImageHolder;

    private Image[] characterImages;

    [Header("Change Player Name")]
    [SerializeField] private RectTransform ChangePlayerName;
    [SerializeField] private CanvasGroup ChangePlayerNameCanvas;

    [Header("Member Button Menu")]
    [SerializeField] private RectTransform MemberBtnMenu;
    [SerializeField] private CanvasGroup MemberBtnMenuCanvas;

    [Header("Member View Menu")]
    [SerializeField] private RectTransform MemberViewMenu;
    [SerializeField] private CanvasGroup MemberViewMenuCanvas;
    [SerializeField] private Text TextMember;

    [Header("Interact With Tutor")]
    [SerializeField] private RectTransform InteractWithTutor;
    [SerializeField] private CanvasGroup InteractWithTutorCanvas;


    [Header("ETC")]
    [SerializeField] private RectTransform TimeView;
    [SerializeField] private RectTransform FadeInOut;
    [SerializeField] private float FadeTime;


    private bool viewFloatingMenu, viewChangeCharacterMenu, viewChangePlayerName, viewMemberBtnMenu, viewMemberViewMenu, viewInteractWithTutor;
    private bool gotoFloatingMenu, gotoChangeCharacterMenu, gotoChangePlayerName, gotoMemberBtnMenu, gotoMemberViewMenu, gotoInteractWithTutor;
    private bool canInteractFloatingMenu, canInteractChangeCharacterMenu, canInteractChangePlayerName, canInteractMemberBtnMenu, canInteractMemberViewMenu, canInteractWithTutor;

    private readonly Vector3 FloatingMenuEnablePos = new Vector3(0, 0);
    private readonly Vector3 FloatingMenuDisablePos = new Vector3(0, -200);
    private readonly Vector3 ChangeCharacterMenuEnablePos = new Vector3(0, 0);
    private readonly Vector3 ChangeCharacterMenuDisablePos = new Vector3(0, -1000);
    private readonly Vector3 ChangePlayerNameEnablePos = new Vector3(0, 0);
    private readonly Vector3 ChangePlayerNameDisablePos = new Vector3(0, -1000);
    private readonly Vector3 MemberBtnMenuEnablePos = new Vector3(0, 0);
    private readonly Vector3 MemberBtnMenuDisablePos = new Vector3(300, 0);
    private readonly Vector3 MemberViewMenuEnablePos = new Vector3(-200, 0);
    private readonly Vector3 MemberViewMenuDisablePos = new Vector3(250, 0);
    private readonly Vector3 InteractWithTutorEnablePos = new Vector3(0, -300);
    private readonly Vector3 InteractWithTutorDisablePos = new Vector3(0, -750);

    private Vector3 FloatingMenuVelocity = Vector3.zero;
    private Vector3 ChangeCharacterMenuVelocity = Vector3.zero;
    private Vector3 ChangePlayerNameVelocity = Vector3.zero;
    private Vector3 MemberBtnMenuVelocity = Vector3.zero;
    private Vector3 MemberViewMenuVelocity = Vector3.zero;
    private Vector3 InteractWithTutorVelocity = Vector3.zero;

    private Image FadeImage;
    private bool IsFading;
    private float FadeTimeLapse;
    private int FadeProgress;
    private Action DeathSequenceHalfCallback;
    private Action DeathSequenceEndCallback;

    private void Start()
    {
        viewFloatingMenu = true;
        viewChangeCharacterMenu = false;
        viewChangePlayerName = false;
        viewMemberBtnMenu = true;
        viewMemberViewMenu = false;
        viewInteractWithTutor = false;

        gotoFloatingMenu = true;
        gotoChangeCharacterMenu = false;
        gotoChangePlayerName = false;
        gotoMemberBtnMenu = true;
        gotoMemberViewMenu = false;
        gotoInteractWithTutor = false;

        canInteractFloatingMenu = true;
        canInteractChangeCharacterMenu = true;
        canInteractChangePlayerName = true;
        canInteractMemberBtnMenu = true;
        canInteractMemberViewMenu = true;
        canInteractWithTutor = true;

        Image[] tempImages = CharacterImageHolder.GetComponentsInChildren<Image>(true);
        characterImages = new Image[tempImages.Length - 1];
        for (int i = 0; i < tempImages.Length - 1; i++)
        {
            characterImages[i] = tempImages[i + 1];
        }

        string initialMemberTxt = $"¼ÛÁö¿ø Unity Æ©ÅÍ\n{GameManager.Instance.PlayerName}";
        SetMemberViewText(initialMemberTxt);

        FadeImage = FadeInOut.GetComponent<Image>();
    }

    private void Update()
    {
        if (viewFloatingMenu == false && gotoFloatingMenu == true)
        {
            FloatingMenu.anchoredPosition = Vector3.SmoothDamp(FloatingMenu.anchoredPosition, FloatingMenuEnablePos, ref FloatingMenuVelocity, 0.05f);
        }
        else if (viewFloatingMenu == true && gotoFloatingMenu == false)
        {
            FloatingMenu.anchoredPosition = Vector3.SmoothDamp(FloatingMenu.anchoredPosition, FloatingMenuDisablePos, ref FloatingMenuVelocity, 0.05f);
        }


        if (viewChangeCharacterMenu == false && gotoChangeCharacterMenu == true)
        {
            ChangeCharacterMenu.anchoredPosition = Vector3.SmoothDamp(ChangeCharacterMenu.anchoredPosition, ChangeCharacterMenuEnablePos, ref ChangeCharacterMenuVelocity, 0.05f);
        }
        else if (viewChangeCharacterMenu == true && gotoChangeCharacterMenu == false)
        {
            ChangeCharacterMenu.anchoredPosition = Vector3.SmoothDamp(ChangeCharacterMenu.anchoredPosition, ChangeCharacterMenuDisablePos, ref ChangeCharacterMenuVelocity, 0.05f);
        }


        if (viewChangePlayerName == false && gotoChangePlayerName == true)
        {
            ChangePlayerName.anchoredPosition = Vector3.SmoothDamp(ChangePlayerName.anchoredPosition, ChangePlayerNameEnablePos, ref ChangePlayerNameVelocity, 0.05f);
        }
        else if (viewChangePlayerName == true && gotoChangePlayerName == false)
        {
            ChangePlayerName.anchoredPosition = Vector3.SmoothDamp(ChangePlayerName.anchoredPosition, ChangePlayerNameDisablePos, ref ChangePlayerNameVelocity, 0.05f);
        }


        if (viewMemberBtnMenu == false && gotoMemberBtnMenu == true)
        {
            MemberBtnMenu.anchoredPosition = Vector3.SmoothDamp(MemberBtnMenu.anchoredPosition, MemberBtnMenuEnablePos, ref MemberBtnMenuVelocity, 0.05f);
        }
        else if (viewMemberBtnMenu == true && gotoMemberBtnMenu == false)
        {
            MemberBtnMenu.anchoredPosition = Vector3.SmoothDamp(MemberBtnMenu.anchoredPosition, MemberBtnMenuDisablePos, ref MemberBtnMenuVelocity, 0.05f);
        }


        if (viewMemberViewMenu == false && gotoMemberViewMenu == true)
        {
            MemberViewMenu.anchoredPosition = Vector3.SmoothDamp(MemberViewMenu.anchoredPosition, MemberViewMenuEnablePos, ref MemberViewMenuVelocity, 0.05f);
        }
        else if (viewMemberViewMenu == true && gotoMemberViewMenu == false)
        {
            MemberViewMenu.anchoredPosition = Vector3.SmoothDamp(MemberViewMenu.anchoredPosition, MemberViewMenuDisablePos, ref MemberViewMenuVelocity, 0.05f);
        }


        if (viewInteractWithTutor == false && gotoInteractWithTutor == true)
        {
            InteractWithTutor.anchoredPosition = Vector3.SmoothDamp(InteractWithTutor.anchoredPosition, InteractWithTutorEnablePos, ref InteractWithTutorVelocity, 0.05f);
        }
        else if (viewInteractWithTutor == true && gotoInteractWithTutor == false)
        {
            InteractWithTutor.anchoredPosition = Vector3.SmoothDamp(InteractWithTutor.anchoredPosition, InteractWithTutorDisablePos, ref InteractWithTutorVelocity, 0.05f);
        }


        if (IsFading)
        {
            FadeTimeLapse += Time.deltaTime;
            if (FadeProgress == 0)
            {
                float timeNext = FadeTime / 3;
                if (FadeTimeLapse < timeNext)
                {
                    float alpha = (FadeTimeLapse - timeNext) / timeNext + 1;
                    FadeImage.color = new Color(0, 0, 0, alpha);
                }
                else
                {
                    EnableFloatingMenu();
                    EnableMemberBtnMenu();
                    FadeProgress++;
                    DeathSequenceHalfCallback?.Invoke();
                }
            }
            else if(FadeProgress == 1)
            {
                float timeNext = FadeTime * 2 / 3;
                if (FadeTimeLapse > timeNext)
                {
                    FadeProgress++;
                }
            }
            else
            {
                if (FadeTimeLapse < FadeTime)
                {
                    float alpha = 3 - (FadeTimeLapse * 3 / FadeTime);
                    FadeImage.color = new Color(0, 0, 0, alpha);
                }
                else
                {
                    FadeInOut.gameObject.SetActive(false);
                    DeathSequenceEndCallback?.Invoke();
                    IsFading = false;
                }
            }
        }
    }


    // Floating Menu ///////////////////////////////////////////////////////////////////
    public void EnableFloatingMenu()
    {
        if (canInteractFloatingMenu == false) return;
        canInteractFloatingMenu = false;

        float delay = 0.01f;

        gotoFloatingMenu = true;
        if (viewFloatingMenu == false) delay = 0.3f;

        Invoke("_EnableFloatingMenu", delay);
    }
    private void _EnableFloatingMenu()
    {
        FloatingMenuCanvas.interactable = true;

        viewFloatingMenu = gotoFloatingMenu;

        canInteractFloatingMenu = true;
    }
    public void DisableFloatingMenu(bool putAway)
    {
        if (canInteractFloatingMenu == false) return;
        canInteractFloatingMenu = false;

        FloatingMenuCanvas.interactable = false;

        float delay = 0.01f;

        if (putAway)
        {
            gotoFloatingMenu = false;
            if (viewFloatingMenu == true) delay = 0.3f;
        }

        Invoke("_DisableFloatingMenu", delay);
    }
    private void _DisableFloatingMenu()
    {
        viewFloatingMenu = gotoFloatingMenu;

        canInteractFloatingMenu = true;
    }

    // Change Character Menu ///////////////////////////////////////////////////////////////////
    public void EnableChangeCharacterMenu()
    {
        if (canInteractChangeCharacterMenu == false) return;
        canInteractChangeCharacterMenu = false;

        float delay = 0.01f;

        gotoChangeCharacterMenu = true;
        if (viewChangeCharacterMenu == false) delay = 0.3f;

        Invoke("_EnableChangeCharacterMenu", delay);
    }
    private void _EnableChangeCharacterMenu()
    {
        ChangeCharacterCanvas.interactable = true;

        viewChangeCharacterMenu = gotoChangeCharacterMenu;

        canInteractChangeCharacterMenu = true;
    }
    public void DisableChangeCharacterMenu(bool putAway)
    {
        if (canInteractChangeCharacterMenu == false) return;
        canInteractChangeCharacterMenu = false;

        ChangeCharacterCanvas.interactable = false;

        float delay = 0.01f;

        if (putAway)
        {
            gotoChangeCharacterMenu = false;
            if (viewChangeCharacterMenu == true) delay = 0.3f;
        }

        Invoke("_DisableChangeCharacterMenu", delay);
    }
    private void _DisableChangeCharacterMenu()
    {
        viewChangeCharacterMenu = gotoChangeCharacterMenu;

        canInteractChangeCharacterMenu = true;
    }

    // Change Player Name ///////////////////////////////////////////////////////////////////
    public void EnableChangePlayerName()
    {
        if (canInteractChangePlayerName == false) return;
        canInteractChangePlayerName = false;

        float delay = 0.01f;

        gotoChangePlayerName = true;
        if (viewChangePlayerName == false) delay = 0.3f;

        Invoke("_EnableChangePlayerName", delay);
    }
    private void _EnableChangePlayerName()
    {
        ChangePlayerNameCanvas.interactable = true;

        viewChangePlayerName = gotoChangePlayerName;

        canInteractChangePlayerName = true;
    }
    public void DisableChangePlayerName(bool putAway)
    {
        if (canInteractChangePlayerName == false) return;
        canInteractChangePlayerName = false;

        ChangePlayerNameCanvas.interactable = false;

        float delay = 0.01f;

        if (putAway)
        {
            gotoChangePlayerName = false;
            if (viewChangePlayerName == true) delay = 0.3f;
        }

        Invoke("_DisableChangePlayerName", delay);
    }
    private void _DisableChangePlayerName()
    {
        viewChangePlayerName = gotoChangePlayerName;

        canInteractChangePlayerName = true;
    }

    // Member Button Menu ///////////////////////////////////////////////////////////////////
    public void EnableMemberBtnMenu()
    {
        if (canInteractMemberBtnMenu == false) return;
        canInteractMemberBtnMenu = false;

        float delay = 0.01f;

        gotoMemberBtnMenu = true;
        if (viewMemberBtnMenu == false) delay = 0.3f;

        Invoke("_EnableMemberBtnMenu", delay);
    }
    private void _EnableMemberBtnMenu()
    {
        MemberBtnMenuCanvas.interactable = true;

        viewMemberBtnMenu = gotoMemberBtnMenu;

        canInteractMemberBtnMenu = true;
    }
    public void DisableMemberBtnMenu(bool putAway)
    {
        if (canInteractMemberBtnMenu == false) return;
        canInteractMemberBtnMenu = false;

        MemberBtnMenuCanvas.interactable = false;

        float delay = 0.01f;

        if (putAway)
        {
            gotoMemberBtnMenu = false;
            if (viewMemberBtnMenu == true) delay = 0.3f;
        }

        Invoke("_DisableMemberBtnMenu", delay);
    }
    private void _DisableMemberBtnMenu()
    {
        viewMemberBtnMenu = gotoMemberBtnMenu;

        canInteractMemberBtnMenu = true;
    }

    // Member View Menu ///////////////////////////////////////////////////////////////////
    public void EnableMemberViewMenu()
    {
        if (canInteractMemberViewMenu == false) return;
        canInteractMemberViewMenu = false;

        float delay = 0.01f;

        gotoMemberViewMenu = true;
        if (viewMemberViewMenu == false) delay = 0.3f;

        Invoke("_EnableMemberViewMenu", delay);
    }
    private void _EnableMemberViewMenu()
    {
        MemberViewMenuCanvas.interactable = true;

        viewMemberViewMenu = gotoMemberViewMenu;

        canInteractMemberViewMenu = true;
    }
    public void DisableMemberViewMenu(bool putAway)
    {
        if (canInteractMemberViewMenu == false) return;
        canInteractMemberViewMenu = false;

        MemberViewMenuCanvas.interactable = false;

        float delay = 0.01f;

        if (putAway)
        {
            gotoMemberViewMenu = false;
            if (viewMemberViewMenu == true) delay = 0.3f;
        }

        Invoke("_DisableMemberViewMenu", delay);
    }
    private void _DisableMemberViewMenu()
    {
        viewMemberViewMenu = gotoMemberViewMenu;

        canInteractMemberViewMenu = true;
    }

    // Interact With Tutor ///////////////////////////////////////////////////////////////////
    public void EnableInteractWithTutor()
    {
        if (canInteractWithTutor == false) return;
        canInteractWithTutor = false;

        float delay = 0.01f;

        gotoInteractWithTutor = true;
        if (viewInteractWithTutor == false) delay = 0.3f;

        Invoke("_EnableInteractWithTutor", delay);
    }
    private void _EnableInteractWithTutor()
    {
        InteractWithTutorCanvas.interactable = true;

        viewInteractWithTutor = gotoInteractWithTutor;

        canInteractWithTutor = true;
    }
    public void DisableInteractWithTutor(bool putAway)
    {
        if (canInteractWithTutor == false) return;
        canInteractWithTutor = false;

        InteractWithTutorCanvas.interactable = false;

        float delay = 0.01f;

        if (putAway)
        {
            gotoInteractWithTutor = false;
            if (viewInteractWithTutor == true) delay = 0.3f;
        }

        Invoke("_DisableInteractWithTutor", delay);
    }
    private void _DisableInteractWithTutor()
    {
        viewInteractWithTutor = gotoInteractWithTutor;

        canInteractWithTutor = true;
    }





    public Image[] GetCharacterImages()
    {
        return characterImages;
    }
        
    public void SetMemberViewText(string text)
    {
        TextMember.text = text;
    }

    public void PutAwayAll()
    {
        DisableFloatingMenu(true);
        DisableChangeCharacterMenu(true);
        DisableChangePlayerName(true);
        DisableMemberBtnMenu(true);
        DisableMemberViewMenu(true);
        DisableInteractWithTutor(true);
        TimeView.gameObject.SetActive(false);
    }
    public void DeathSequence(Action deathSequenceHalfCallback, Action deathSequenceEndCallback)
    {
        DeathSequenceHalfCallback = deathSequenceHalfCallback;
        DeathSequenceEndCallback = deathSequenceEndCallback;
        DisableFloatingMenu(false);
        DisableChangeCharacterMenu(true);
        DisableChangePlayerName(true);
        DisableMemberBtnMenu(false);
        DisableMemberViewMenu(true);
        DisableInteractWithTutor(true);
        FadeInOut.gameObject.SetActive(true);
        FadeTimeLapse = 0;
        FadeProgress = 0;
        IsFading = true;
    }
}
