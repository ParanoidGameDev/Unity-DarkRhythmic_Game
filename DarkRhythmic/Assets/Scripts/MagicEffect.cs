[System.Serializable]
public class MagicEffect {
    // ! Attribute
    public string effectName;
    public float damageThreshold;
    public DamageType damageType;

    // ! Constructor
    public MagicEffect(string name, float damageModifier, DamageType type)
    {
        this.effectName = name;
        this.damageThreshold = damageModifier;
        this.damageType = type;
    }
}

public enum DamageType {
    Radiance, // Against shadows
    Glory, // Against dead
    Purity, // Against possession
    Clarity, // Against arcane
    Harmony, // Against caos
}

// USAGE
// AoE light - spawn        // dark tiles
// Streghtness              // heavy enemies
// Sustained - loop         // first enemy
// Chained                  // many enemies
// Crit on sync             // any enemy

// SAVAGE
// Emission
// Invulnerability
// Regen
// Stun enemies
// Cure