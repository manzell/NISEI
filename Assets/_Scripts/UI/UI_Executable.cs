using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; 

public class UI_Executable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI executionName, executionDescription;
    [SerializeField] RawImage image;
    [SerializeField] Color hoverBGColor, hoverTextColor, headerTextColor, descTextColor; 

    public Executable Exe { get; private set; }

    private void Start()
    {
        if (Exe != null)
            Setup(Exe); 
    }

    public void Setup(Executable exe)
    {
        this.Exe = exe;
        exe.updateExe.AddListener(UpdateText);
        UpdateText(exe); 
    }

    public void UpdateText(Executable exe)
    {
        executionName.text = exe.Name;
        executionDescription.text = exe.Desc;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverBGColor;
        executionDescription.color = hoverTextColor;
        executionName.color = hoverTextColor; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.clear;
        executionName.color = headerTextColor;
        executionDescription.color = descTextColor; 
    }
}
