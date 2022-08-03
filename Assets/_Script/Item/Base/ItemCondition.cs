using System;

public enum CompareType
{
    Greater = 0,
    GreaterEqual = 1,
    Equal = 2,
    LessEqual = 3,
    Less = 4,
}

[Serializable]
public abstract class ItemCondition
{
    public abstract bool CheckCondition(object target);

    public bool CheckValue(float value, float compareValue, CompareType type)
    {
        switch (type)
        {
            case CompareType.Greater:
                return value > compareValue;
            case CompareType.GreaterEqual:
                return value >= compareValue;
            case CompareType.Equal:
                return Math.Abs(value - compareValue) < 1e-6;
            case CompareType.LessEqual:
                return value <= compareValue;
            case CompareType.Less:
                return value < compareValue;
        }

        return false;
    }
}