using System;
using UnityEngine;

public class Clock : MonoBehaviour {

    [Header("Clock Properties")]
    [SerializeField] Transform hoursTransform;
    [SerializeField] Transform minutesTransform;
    [SerializeField] Transform secondsTransform;
    [SerializeField] bool continuous;

    const float degreesPerHour = 30f;
    const float degreesPerMinute = 6f;
    const float degreesPerSecond = 6f;

    private void Awake() {
        UpdateContinuous();
    }

    // Update is called once per frame
    private void Update () {
        if (continuous)
            UpdateContinuous();
        else
            UpdateDiscrete();
    }

    void UpdateContinuous() {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursTransform.localRotation    = Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
        minutesTransform.localRotation  = Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation  = Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
    }

    void UpdateDiscrete() {
        DateTime time = DateTime.Now;
        hoursTransform.localRotation    = Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation  = Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation  = Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
    }
}
