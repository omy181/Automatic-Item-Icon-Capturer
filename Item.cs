using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ID;
    public string Name;
    public Sprite Icon => IconCapturer.GetSprite(ID);
    public GameObject Model; // The model to be used for icon capture

    [Header("IconCaputre")]
    public Vector3 CaptureRotation;
    public Vector3 CaptureOffset;
    public float CaptureScale = 1;
    public override string ToString()
    {
        return Name;
    }
}