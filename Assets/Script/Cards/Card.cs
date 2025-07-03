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
        Description = LocalizationManager.GetText(nameof(WindCard));
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
        Description = LocalizationManager.GetText(nameof(EarthquakeCard));
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
        Description = LocalizationManager.GetText(nameof(GlitchCard));
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
        Description = LocalizationManager.GetText(nameof(RotatingPlatformCard));
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
        Description = LocalizationManager.GetText(nameof(MagneticCard));
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
        Description = LocalizationManager.GetText(nameof(GhostCard));
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
        Description = LocalizationManager.GetText(nameof(HeavyCard));
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
        Description = LocalizationManager.GetText(nameof(SlipperyCard));
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
        Description = LocalizationManager.GetText(nameof(ExplosiveCard));
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
        Description = LocalizationManager.GetText(nameof(SmokeCard));
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}
