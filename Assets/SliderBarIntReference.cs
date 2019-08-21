using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarIntReference : MonoBehaviour
{
    public FloatVariable MazeSize;
    public GameEvent UpdatedSize;
    public Slider val;

    public void UpdateMazeSize()
    {
        MazeSize.SetValue(val.value);
        UpdatedSize.Raise();
    }
}
