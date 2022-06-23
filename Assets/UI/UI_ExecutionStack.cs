using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq; 

public class UI_ExecutionStack : MonoBehaviour
{
    [SerializeField] Rig rig; 
    [SerializeField] GameObject executionPrefab, stackArea;
    [SerializeField] Button executeButton;
    [SerializeField] TextMeshProUGUI heading;

    UnityAction<Executable> updateHeading;

    private void Start()
    {
        updateHeading = exe => UpdateHeading();

        if (rig != null)
            Setup(rig); 
    }

    void UpdateHeading()
    {
        heading.text = $"Execution Stack > {stackArea.GetComponentsInChildren<UI_Executable>().Sum(_e => _e.exe.cycles.Value)}/{rig.clockSpeed}";
    }

    public void Setup(Rig rig)
    {
        foreach (Transform t in stackArea.transform)
            Destroy(t.gameObject);

        RemoveListeners(this.rig);
        RegisterListeners(rig);

        this.rig = rig;
        UpdateHeading();
    }

    public void Execute() => rig?.Execute();

    private void RegisterListeners(Rig rig)
    {
        rig.enqueueEvent.AddListener(Enqueue);
        rig.enqueueEvent.AddListener(updateHeading);
        rig.dequeueEvent.AddListener(Dequeue);
        rig.dequeueEvent.AddListener(updateHeading);
        CombatManager.turnStartEvent.AddListener(UpdateHeading);
        CombatManager.turnStartEvent.AddListener(SetExecuteButtonStatus);
    }

    private void RemoveListeners(Rig rig)
    {
        rig.enqueueEvent.RemoveListener(Enqueue);
        rig.enqueueEvent.RemoveListener(updateHeading);
        rig.dequeueEvent.RemoveListener(Dequeue);
        rig.dequeueEvent.RemoveListener(updateHeading);
        CombatManager.turnStartEvent.RemoveListener(UpdateHeading);
        CombatManager.turnStartEvent.RemoveListener(SetExecuteButtonStatus);
    }

    public void Enqueue(Executable exe)
    {
        Instantiate(executionPrefab, stackArea.transform)
            .GetComponent<UI_Executable>().Setup(exe); // TODO - styling, custom prefabs. 

        exe.updateExe.AddListener(UpdateHeading); 
        SetExecuteButtonStatus();
    }

    public void Dequeue(Executable exe)
    {
        exe.updateExe.RemoveListener(UpdateHeading);

        foreach (Transform t in stackArea.transform)
            if (t.GetComponent<UI_Executable>()?.exe == exe)
                Destroy(t.gameObject);

        SetExecuteButtonStatus(); 
    }

    void SetExecuteButtonStatus() => executeButton.interactable = stackArea.transform.childCount > 0;
}