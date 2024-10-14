using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectPool objectPool { get; private set; }

    public string PlayerName = "스파르타";
    public int CharacterSelect = 0;
    public string[] CharacterRace;

    public bool CanInteractWithTutor;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            objectPool = GetComponent<ObjectPool>();

            CharacterRace = new string[] { "Penguin", "Knight Female", "Knight Male", "Wizzard Female", "Wizzard Male", "Elf Female", "Elf Male",
                                           "Dwarf Female", "Dwarf Male", "Lizard Female", "Lizard Male", "Pumpkin Man" };
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsLayerMatchedWithPlayer(int layer)
    {
        return LayerMask.NameToLayer("Player") == layer;
    }
}

public enum PlayableCharacter
{
    Penguin, KnightF, KnightM, WizzardF, WizzardM, ElfF, ElfM, DwarfF, DwarfM, LizardF, LizardM, Pumpkin,
    Max
}
