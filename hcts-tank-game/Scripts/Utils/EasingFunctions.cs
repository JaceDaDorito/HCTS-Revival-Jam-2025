using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HCTSTankGame.Utils;

public static partial class MathUtils
{
    public static float easeInSine(float x)
    {
        return 1 - Mathf.Cos((float)(x * Math.PI) / 2);
    }
}