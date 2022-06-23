using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class Rig : MonoBehaviour
{
    public static UnityEvent<ICE> TraceEvent = new UnityEvent<ICE>();

    public new string name; 
    public int memory; // Determines number of installed programs
    public int busWidth; // Determines size of draw
    public int clockSpeed; // Determines number of cycles per turn, energy
    public int availableMemory => memory - installedPrograms.Sum(program => program.data.memoryCost);

    public List<Program> installedPrograms = new List<Program>();
    public List<Card> hand, draw, discard, trash;

    [SerializeField] List<CardData> startingHand = new List<CardData>();
    public List<Executable> programExecutionStack { get; private set; } = new List<Executable>();

    [HideInInspector] public UnityEvent<Executable> 
        enqueueEvent = new UnityEvent<Executable>(),
        executeEvent = new UnityEvent<Executable>(),
        dequeueEvent = new UnityEvent<Executable>();
    [HideInInspector] public UnityEvent<Program> 
        installEvent = new UnityEvent<Program>(),
        uninstallEvent = new UnityEvent<Program>();
    [HideInInspector] public UnityEvent<Card> 
        cardDrawEvent = new UnityEvent<Card>(),
        cardPlayEvent = new UnityEvent<Card>(),
        cardDiscardEvent = new UnityEvent<Card>(),
        cardTrashEvent = new UnityEvent<Card>();

    public void Awake()
    {
        CreateStartingDrawDeck();
        CombatManager.turnStartEvent.AddListener(DrawHand);
        cardPlayEvent.AddListener(OnPlayCard); 
    }

    [ContextMenu("Start Turn")]
    public void StartTurn() => CombatManager.turnStartEvent.Invoke(); 

    private void CreateStartingDrawDeck()
    {
        foreach (CardData cardData in startingHand)
            draw.Add(new Card(cardData));

        draw = draw.OrderBy(card => Random.value).ToList();
    }

    private void DrawHand()
    {
        int drawNum = 5; // TODO make this dynamic

        for(int i = 0; i < drawNum; i++)
        {
            if (draw.Count > 0)
            {
                Card card = draw[0];
                draw.Remove(card);
                hand.Add(card);
                cardDrawEvent.Invoke(card); 
            }
            else break; 
        }
    }

    void OnPlayCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            discard.Add(card); 
        }
    }

    public virtual void Install(ProgramData programData)
    {
        Program program = new Program(programData); 
        program.rig = this; 
        installedPrograms.Add(program);
        installEvent.Invoke(program);        
    }

    public void Enqueue(Executable exe)
    {
        if (programExecutionStack.Sum(e => e.cycles.Value) + exe.cycles.Value <= clockSpeed)
        {
            programExecutionStack.Add(exe);
            enqueueEvent.Invoke(exe);
        }
    }

    public void Dequeue(Executable exe)
    {
        programExecutionStack.Remove(exe);
        dequeueEvent.Invoke(exe); 
    }

    public void Execute()
    {
        List<Executable> dequeList = new List<Executable>(); 

        for(int i = 0; i < programExecutionStack.Count; i++)
        {
            Executable exe = programExecutionStack[i];

            //TODO: Charge cycles
            exe.Execute();
            executeEvent.Invoke(exe);

            if (exe.flushable)
                dequeList.Add(exe); 
        }

        foreach (Executable exe in dequeList)
            Dequeue(exe);

        CombatManager.turnEndEvent.Invoke(); 
    }
}
