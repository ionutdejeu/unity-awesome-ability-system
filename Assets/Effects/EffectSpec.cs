using System;
public class EffectSpec
{
    /// <summary>
    /// Original effect object that is the base for this spec
    /// </summary>
    public EffectScriptableObject Effect { get; private set; }

    public float DurationRemaining { get; private set; }
    public float TotalDuration { get; private set; }


    public EffectSpec()
    {

    }
}
