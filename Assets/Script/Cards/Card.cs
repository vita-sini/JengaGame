public abstract class Card
{
    public string Description { get; protected set; }

    public abstract void Execute();
}

public class WindCard : Card
{
    private readonly IEffect _effects;

    public WindCard(IEffect effects)
    {
        Description = "Торнадо!!!";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class EarthquakeCard : Card
{
    private readonly IEffect _effects;

    public EarthquakeCard(IEffect effects)
    {
        Description = "Земля дрожит!";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class MagicShieldCard : Card
{
    private readonly IEffect _effects;

    public MagicShieldCard(IEffect effects)
    {
        Description = "Защищает башню от падения кубиков";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class GlitchCard : Card
{
    private readonly IEffect _effects;

    public GlitchCard(IEffect effects)
    {
        Description = "Случайные блоки зависают и не поддаются извлечению";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class RotatingPlatformCard : Card
{
    private readonly IEffect _effects;

    public RotatingPlatformCard(IEffect effects)
    {
        Description = "Башня начинает крутиться";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class MagneticCard : Card
{
    private readonly IEffect _effects;

    public MagneticCard(IEffect effects)
    {
        Description = "Блоки притягиваются друг к другу";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class GhostCard : Card
{
    private readonly IEffect _effects;

    public GhostCard(IEffect effects)
    {
        Description = "Блоки становятся невидимыми";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class HeavyCard : Card
{
    private readonly IEffect _effects;

    public HeavyCard(IEffect effects)
    {
        Description = "Блоки становятся тяжёлыми";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class SlipperyCard : Card
{
    private readonly IEffect _effects;

    public SlipperyCard(IEffect effects)
    {
        Description = "Блоки становятся скользкими";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class ExplosiveCard : Card
{
    private readonly IEffect _effects;

    public ExplosiveCard(IEffect effects)
    {
        Description = "Один блок взорвется";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}

public class SmokeCard : Card
{
    private readonly IEffect _effects;

    public SmokeCard(IEffect effects)
    {
        Description = "Стало дымно";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}
