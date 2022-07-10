using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_Debug : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageBox;
    public string message => messageBox.text; 

    public void SetMessage(string message) => messageBox.text = message;

}
