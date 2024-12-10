using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateKillNumber : MonoBehaviour
{
    public TextMeshProUGUI killNumberText;

    // Update is called once per frame
    void Update()
    {
        killNumberText.text = "KILL NUMBER:" + AttackPlayer.KillNumber;
    }
}
