using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public string userPlayFabId;
    public string userDisplayName;
    public bool isLoggedIn;
    public Color currentPlayerColor;

    public UserData()
    {
        userDisplayName = null;
        isLoggedIn = false;
        currentPlayerColor = ItemColor.ItemIDToColor(default);
    }
}
