using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro; 

public class UI_Program : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Program program { get; private set; }
    [SerializeField] TextMeshProUGUI programName, quickAccess;
    [SerializeField] Button runProgramButton;
    [SerializeField] KeyCode AccessKey;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(AccessKey))
            runProgramButton.onClick.Invoke(); 
    }

    public void Setup(Program program)
    {
        this.program = program;

        programName.text = $"{program.name}()";
        quickAccess.text = ServerManager.currentRig.Programs.Count.ToString(); 
        AccessKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), quickAccess.text);
        gameObject.name = program.name; 
        
        runProgramButton.onClick.RemoveAllListeners();
        runProgramButton.onClick.AddListener(program.Enqueue); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<UI_Debug>().SetMessage(program.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<UI_Debug>().SetMessage(string.Empty);
    }
}
