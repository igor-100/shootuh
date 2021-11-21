﻿namespace CharacterStats
{
    public class CharacterStatModifier
    {
        public readonly float Value;
        public readonly StatModType Type;
        public readonly int Order;
        public readonly object Source;

        public CharacterStatModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public CharacterStatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

        public CharacterStatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

        public CharacterStatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }

    public enum StatModType
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMult = 300,
    }
}
