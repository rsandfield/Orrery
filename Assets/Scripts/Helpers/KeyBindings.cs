using UnityEngine;

public static class KeyBindings
{
    static string KEYCODE_BASE = "KEYCODE_";
    public static void SaveKeyBinding(string key, KeyCode code)
    {
        PlayerPrefs.SetInt(KEYCODE_BASE + key, (int) code);
        PlayerPrefs.Save();
    }

    public static KeyCode GetKeyBinding(string key, KeyCode defaultBinding)
    {
        return (KeyCode) PlayerPrefs.GetInt(KEYCODE_BASE + key, (int) defaultBinding);
    }

    public static KeyCode Up()
    {
        return GetKeyBinding("UP", KeyCode.UpArrow);
    }

    public static KeyCode Up_Alt()
    {
        return GetKeyBinding("UP_ALT", KeyCode.None);
    }

    public static KeyCode Down()
    {
        return GetKeyBinding("DOWN", KeyCode.DownArrow);
    }

    public static KeyCode Down_Alt()
    {
        return GetKeyBinding("DOWN_ALT", KeyCode.None);
    }

    public static KeyCode Left()
    {
        return GetKeyBinding("LEFT", KeyCode.LeftArrow);
    }

    public static KeyCode Left_Alt()
    {
        return GetKeyBinding("LEFT_ALT", KeyCode.None);
    }

    public static KeyCode Right()
    {
        return GetKeyBinding("RIGHT", KeyCode.RightArrow);
    }

    public static KeyCode Right_Alt()
    {
        return GetKeyBinding("RIGHT_ALT", KeyCode.None);
    }
}