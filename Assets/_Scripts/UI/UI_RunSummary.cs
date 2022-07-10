using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_RunSummary : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI attempts, successes, cycles, attemptRate, cycleRate;

    public void Setup()
    {
        attempts.text = ServerManager.attempts.ToString();
        successes.text = ServerManager.successes.ToString();
        cycles.text = ServerManager.cycles.ToString();

        attemptRate.text = Mathf.Round(100 * ServerManager.successes / (float)ServerManager.attempts).ToString();
        cycleRate.text = Mathf.Round(100 * ServerManager.successes / (float)ServerManager.cycles).ToString();
    }
}
