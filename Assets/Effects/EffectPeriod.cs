using System;

/// <summary>
/// Period configuration for specific duration or infinite effects 
/// </summary>
[Serializable]
public struct EffectPeriod
{
    /// <summary>
    /// The period at which to tick this effect
    /// </summary>
    public float Period;

    /// <summary>
    /// True if effect is execture on first application or wait for the first internal tick
    /// </summary>
    public bool ExecuteOnApplication;
}
