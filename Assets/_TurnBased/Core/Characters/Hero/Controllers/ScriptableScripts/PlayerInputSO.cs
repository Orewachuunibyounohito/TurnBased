using UnityEngine;

[CreateAssetMenu(fileName = "New Input", menuName = "Turn-based/Custom Player Input")]
public class PlayerInputSO : ScriptableObject
{
    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputLeft;
    public KeyCode inputRight;
    public KeyCode inputBack;
    public KeyCode inputConfirm;
    public KeyCode altInputUp;
    public KeyCode altInputDown;
    public KeyCode altInputLeft;
    public KeyCode altInputRight;
    public KeyCode altInputBack;
    public KeyCode altInputConfirm;
}
