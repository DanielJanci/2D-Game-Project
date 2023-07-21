using UnityEngine;

public static class ItemColor
{
    public static Color ItemIDToColor(string itemID)
    {
        return itemID switch
        {
            "blue" => new Color(0f, 0.3729431f, 0.9254902f, 1f),
            "purple" => new Color(0.5844213f, 0f, 0.9254902f, 1f),
            "pink" => new Color(0.9254902f, 0f, 0.730417f, 1f),
            _ => new Color(0.1704033f, 0.9245283f, 0f, 1f)
        };
    }
}
