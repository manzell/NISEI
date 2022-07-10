using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.Events; 
using System.Linq;

[CreateAssetMenu(menuName = "Rig/rig")]
public class Rig : ScriptableObject
{
    public new string name; 
    public intRef memory;
    public intRef busWidth, clockSpeed, link, trace;
    public int AvailableMemory=> (int)memory.Value - Programs.Sum(program => program.MemoryCost);
    public int cycles { get; private set; } = 0; 
    
    public List<Executable> ExecutionStack { get; private set; } = new List<Executable>();     
    public List<Program> Programs { get; private set; } = new List<Program>();
    public List<Card> Hand { get; private set; } = new List<Card>(); 
    public List<Card> DrawDeck { get; private set; } = new List<Card>();
    public List<Card> Discards { get; private set; } = new List<Card>();
    public List<Card> Trash { get; private set; } = new List<Card>();

    [SerializeField] Scene runSummaryScene; 

    [SerializeField] List<Card> startingCards = new List<Card>();
    [SerializeField] List<Program> startingPrograms = new List<Program>(); 
    [SerializeField] List<PlayBehavior> startupBehaviors = new List<PlayBehavior>();

    public GameObject rigPrefab, programPrefab, executionPrefab, cardPrefab;

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
    [HideInInspector] public UnityEvent<Rig>
        updateRig = new UnityEvent<Rig>(); 

    public void Boot()
    {
        Debug.Log($"{name} Booting...");

        CreateStartingDrawDeck();
        InstallStartingPrograms();
        RunStartupBehaviors();

        ServerManager.turnStartEvent.AddListener(DrawUpOnTurnStart);
        ServerManager.turnStartEvent.AddListener(ResetAvailableCycles);
        ServerManager.turnEndEvent.AddListener(DiscardHandOnTurnEnd);
        ServerManager.failedRunEvent.AddListener(OnRunEnd);
        ServerManager.successfulRunEvent.AddListener(OnRunEnd);
        ServerManager.turnStartEvent.Invoke();
    }

    private void RunStartupBehaviors()
    {
        foreach (PlayBehavior behavior in startupBehaviors)
            behavior.Play(this, null);
    }

    /* RIG */
    void ResetAvailableCycles() => cycles = Mathf.Min(clockSpeed, cycles + clockSpeed); 
    public void ChargeCycles(int c) => cycles -= c;
    public void ChargeCycles(intRef c) => cycles -= c;

    /* STACK */
    public void Enqueue(Executable exe)
    {
        if (ExecutionStack.Sum(e => e.cycles.Value) + exe.cycles.Value <= clockSpeed)
        {
            ExecutionStack.Add(exe);
            enqueueEvent.Invoke(exe);
        }
    }
    public void Dequeue(Executable exe)
    {
        ExecutionStack.Remove(exe);
        dequeueEvent.Invoke(exe); 
    }

    public void ExecuteStack()
    {
        if (ExecutionStack.Count > 0)
        {
            Executable exe = ExecutionStack.First();
            ExecutionStack.Remove(exe);
            exe.SetCallback(ExecuteStack);
            exe.Execute();

            Dequeue(exe); 
        }
        else
        {
            ServerManager.turnEndEvent.Invoke();
        }
    }

    /* PROGRAMS */
    public virtual void InstallProgram(Program program)
    {
        program = Instantiate(program);

        Programs.Add(program);
        installEvent.Invoke(program);
    }
    void InstallStartingPrograms()
    {
        foreach (Program program in startingPrograms)
            InstallProgram(program); 
    }

    /* CARDS */
    public void AddCard(Card card) => Discards.Add(GameObject.Instantiate(card));
    public void RemoveCard(Card card)
    {
        Hand.Remove(card);
        Discards.Remove(card);
        DrawDeck.Remove(card);
        Trash.Add(card);

        cardTrashEvent.Invoke(card); 
    }
    
    public Card Draw()
    {
        if (DrawDeck.Count == 0)
        {
            DrawDeck.AddRange(Discards.OrderBy(card => Random.value));
            Discards.Clear();
        }
        if (DrawDeck.Count > 0)
        {
            Card card = DrawDeck.First();
            Draw(card);
            return card;
        }

        return null;
    }
    public void Draw(Card card)
    {
        Hand.Add(card);
        DrawDeck.Remove(card);
        cardDrawEvent.Invoke(card);
    }

    private void CreateStartingDrawDeck()
    {
        Debug.Log("Creating Starting Draw Deck");
        foreach (Card card in startingCards.OrderBy(card => Random.value))
            AddCard(card);
    }
    private void DrawUpOnTurnStart()
    {
        Debug.Log("Drawing Up on Turn Start"); 
        int drawSpace = (int)busWidth.Value + AvailableMemory;
        cycles = (int)clockSpeed.Value; 

        while(drawSpace > 0)
        {
            Card card = Draw();

            if (card != null)
                drawSpace -= card.GetDrawCost();
            else
                break; 
        }
    }
    private void DiscardHandOnTurnEnd()
    {
        Discards.AddRange(Hand);
        Hand.Clear(); 
    }

    public IEnumerable<Card> GetAllCards() => DrawDeck.Union(Hand).Union(Discards);
    public Card GetCardByOrder(ModifyCardDrawCost.Order order, IEnumerable<Card> cards)
    {
        switch (order)
        {
            case ModifyCardDrawCost.Order.First:
                return cards.First();
            case ModifyCardDrawCost.Order.Last:
                return cards.Last();
            default:
                return cards.OrderBy(card => Random.value).First();
        }
    }
    public string SourceDeck(Card card)
    {
        if (DrawDeck.Contains(card))
            return "Draw Deck";
        else if (Discards.Contains(card))
            return "Discard Pile";
        else if (Hand.Contains(card))
            return "Hand";
        else if (Trash.Contains(card))
            return "Trash";
        else
            return string.Empty; 
    }

    /* OTHER */

    void OnRunEnd()
    {
        SceneManager.LoadScene(runSummaryScene.ToString()); 
    }
}