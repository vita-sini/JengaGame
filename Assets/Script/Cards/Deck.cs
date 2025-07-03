using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Deck : MonoBehaviour
{
    [SerializeField] private CardUI _cardUI;
    [SerializeField] private float _probability;

    private List<Card> _cards = new List<Card>();
    private Random _random = new Random();

    private void Awake()
    {
        InitializeDeck();
        ShuffleDeck();
    }

    private void InitializeDeck()
    {
        WindEffect windEffect = GetComponent<WindEffect>();
        EarthquakeEffect earthquakeEffect = GetComponent<EarthquakeEffect>();
        Glitch glitchEffect = GetComponent<Glitch>();
        RotatingPlatform rotatingPlatformEffect = GetComponent<RotatingPlatform>();
        Magnetic magneticEffect = GetComponent<Magnetic>();
        Ghost ghostEffect = GetComponent<Ghost>();
        Heavy heavyEffect = GetComponent<Heavy>();
        Slippery slipperyEffect = GetComponent<Slippery>();
        Explosive explosiveEffect = GetComponent<Explosive>();
        Smoke smokeEffect = GetComponent<Smoke>();

        _cards.Add(new WindCard(windEffect));
        _cards.Add(new EarthquakeCard(earthquakeEffect));
        _cards.Add(new GlitchCard(glitchEffect));
        _cards.Add(new RotatingPlatformCard(rotatingPlatformEffect));
        _cards.Add(new MagneticCard(magneticEffect));
        _cards.Add(new GhostCard(ghostEffect));
        _cards.Add(new HeavyCard(heavyEffect));
        _cards.Add(new SlipperyCard(slipperyEffect));
        _cards.Add(new ExplosiveCard(explosiveEffect));
        _cards.Add(new SmokeCard(smokeEffect));
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            int randomPosition = _random.Next(_cards.Count);
            Card temporary = _cards[randomPosition];
            _cards[randomPosition] = _cards[i];
            _cards[i] = temporary;
        }
    }

    public void OnTurnEnd()
    {
        float chance = UnityEngine.Random.Range(0f, 1f);

        if (chance <= _probability)
            DrawCard();
    }

    private void DrawCard()
    {
        if (_cards.Count > 0)
        {
            Card drawnCard = _cards[0];
            _cards.RemoveAt(0);

            //Показываем карту в UI
            _cardUI.ShowCard(drawnCard);

            // Выполняем эффект карты
            drawnCard.Execute();

            // Возвращаем карту в колоду
            _cards.Add(drawnCard);
            ShuffleDeck();
        }
    }
}
