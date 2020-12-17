using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Profiling;

public class Stats_Display : MonoBehaviour
{
    public Temp_MC_Stats player;
    public IsoMovementController playerspeed;
    public TextMeshProUGUI attackstat, speedstat, healthstat, goldcount;

    void Start()
    {
        player = FindObjectOfType<Temp_MC_Stats>();
        playerspeed = FindObjectOfType<IsoMovementController>();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        attackstat.text = player.attackPwr.ToString();
        speedstat.text = playerspeed.movementSpeed.ToString();
        if(player.curHearts < 0)
        {
            healthstat.text = "0";
        }
        else
        {
            healthstat.text = player.curHearts.ToString("F0");
        }
        goldcount.text = player.curGold.ToString();
    }
}
