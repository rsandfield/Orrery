using UnityEngine;

public static class FloatHelper
{
    public static float ClampLooping(float number, float min, float max)
    {
        if(min > max)
        {
            float temp = min;
            min = max;
            max = temp;
        }

        float range = max - min;
        while(number < min) number += range;
        while(number > max) number -= range;
        return number;
    }
}