using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStat;
    public CharacterStat currentStat { get; private set; }

    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        if (baseStat.attackSO != null)
        {
            attackSO = Instantiate(baseStat.attackSO);
        }

        currentStat = new CharacterStat { attackSO = attackSO };
        currentStat.statsChangeType = baseStat.statsChangeType;
        currentStat.maxHealth = baseStat.maxHealth;
        currentStat.speed = baseStat.speed;
    }
}
