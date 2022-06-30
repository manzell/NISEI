using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

[CreateAssetMenu(menuName = "Rig/rig")]
public class Rig : ScriptableObject
{
    public new string name; 
    public intRef memory;
    public int AvailableMemory => (int)memory.Value - Programs.Sum(program => program.memoryCost);
    public intRef busWidth;
    public intRef clockSpeed;
    
    public List<Executable> ExecutionStack { get; private set; } = new List<Executable>();     
    public List<Program> Programs { get; private set; } = new List<Program>();

    public List<Card> Hand { get; private set; } = new List<Card>(); 
    public List<Card> DrawDeck { get; private set; } = new List<Card>();
    public List<Card> Discards { get; private set; } = new List<Card>();
    public List<Card> Trash { get; private set; } = new List<Card>();

    [SerializeField] List<Card> startingCards = new List<Card>();
    [SerializeField] GameObject rigPrefab, programPrefab, executionPrefab, cardPrefab; 

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
        ServerManager.turnStartEvent.AddListener(DrawUpOnTurnStart);
        cardPlayEvent.AddListener(DiscardCardOnPlay);
        CreateStartingDrawDeck();

        updateRig.Invoke(this); 
    } 

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
        List<Executable> dequeList = new List<Executable>(); 

        for(int i = 0; i < ExecutionStack.Count; i++)
        {
            Executable exe = ExecutionStack[i];

            //TODO: Charge cycles
            exe.Execute();
            executeEvent.Invoke(exe);
            dequeList.Add(exe); 
        }

        foreach (Executable exe in dequeList) 
            Dequeue(exe);

        ServerManager.turnEndEvent.Invoke();
    }

    /* PROGRAMS */
    public virtual void InstallProgram(Program program)
    {
        program = Instantiate(program);

        Programs.Add(program);
        installEvent.Invoke(program);
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
        Debug.Log("|DesperadOS> Creating Starting Draw Deck");
        foreach (Card card in startingCards.OrderBy(card => Random.value))
            AddCard(card);
    }
    private void DrawUpOnTurnStart()
    {
        Debug.Log($"Drawing up to {name} busWidth: {(int)busWidth.Value + AvailableMemory}");
        int drawSpace = (int)busWidth.Value + AvailableMemory;
        while(drawSpace > 0)
        {
            Card card = Draw();
            Debug.Log($"Drawing {card.name} ({card.GetDrawCost()})");

            if (card != null)
                drawSpace -= card.GetDrawCost();
            else
                break; 
        }
    }
    private void DiscardCardOnPlay(Card card)
    {
        if (Hand.Contains(card))
        {
            Hand.Remove(card);
            Discards.Add(card);
        }
    }

    public IEnumerable<Card> GetAllCards() => DrawDeck.Union(Hand).Union(Discards);
    public Card GetCardByOrder(ModifyCard.Order order, IEnumerable<Card> cards)
    {
        switch (order)
        {
            case ModifyCard.Order.First:
                return cards.First();
            case ModifyCard.Order.Last:
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
}