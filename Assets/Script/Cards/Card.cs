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
        Description = "�������!!!";
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
        Description = "����� ������!";
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
        Description = "�������� ����� �� ������� �������";
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
        Description = "��������� ����� �������� � �� ��������� ����������";
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
        Description = "����� �������� ���������";
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
        Description = "����� ������������� ���� � �����";
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
        Description = "����� ���������� ����������";
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
        Description = "����� ���������� �������";
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
        Description = "����� ���������� ����������";
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
        Description = "���� ���� ���������";
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
        Description = "����� �����";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.Execute();
    }
}
