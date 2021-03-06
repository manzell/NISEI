using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro;
using System.Linq; 

public class UI_Ice : MonoBehaviour
{
    public ICE ice { get; private set; }
    [SerializeField] TextMeshProUGUI iceName;
    [SerializeField] Image image; 
    [SerializeField] GameObject subArea, subRoutinePrefab;

    private void Start()
    {
        ServerManager.iceRezEvent.AddListener(Setup); 
    }

    public void Setup(ICE ice)
    {
        this.ice = ice;
        iceName.text = ice.name;
        gameObject.name = ice.name;
        image.sprite = ice.sprite;

        foreach (Transform t in subArea.transform)
            Destroy(t.gameObject);

        foreach(Subroutine sub in ice.Subroutines)
            Instantiate(subRoutinePrefab, subArea.transform).GetComponent<UI_Subroutine>().Setup(sub); 
    }
}
