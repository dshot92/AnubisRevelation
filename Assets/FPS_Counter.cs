using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Timers;

public class FPS_Counter : MonoBehaviour
{
    /*
    [SerializeField] public TextMeshProUGUI _fpsText;
    [SerializeField] float timer;
    [SerializeField] private float _hudRefreshRate = 1f;

    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            // Format float to 2 decimal places
            // https://answers.unity.com/questions/50391/how-to-round-a-float-to-2-dp.html
            _fpsText.SetText("FPS " + (1f / Time.unscaledTime).ToString("F0"));
            timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
    */

    [SerializeField] private TextMeshProUGUI _fpsText;
    [SerializeField] private float _hudRefreshRate = 1f;

    private float _timer;

    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            // https://forum.unity.com/threads/fps-counter.505495/
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.SetText("FPS " + fps.ToString());
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}
