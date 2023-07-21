using System;
using System.Security.Cryptography;

[Serializable]
public class SettingsData
{
    public float volume;

    public SettingsData()
    {
        volume = 1f;
    }
}
