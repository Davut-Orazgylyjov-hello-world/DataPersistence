[System.Serializable]
public class Options 
{
    public SerializableDictionary<string, float> soundOptions = new();
    public SerializableDictionary<string, bool> postEffects = new();
    public SerializableDictionary<string, string> keyBindings = new();
    public int screenResolutionIndex;
    public bool verticalSync;
    public bool fullscreen;
    public bool invertMouseX;
    public bool invertMouseY;
    public int antiAliasing;
    public float mouseSensitivity;
    public float fov;
    public bool isInitialized;
}
