using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarReference : MonoBehaviour
{
    public FloatVariable Ref;
    public GameEvent Updated;
    public Slider val;

    public void UpdateValue()
    {
        Ref.SetValue(val.value);
        Updated.Raise();
    }
}
