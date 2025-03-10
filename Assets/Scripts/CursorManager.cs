using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D CursorTexture;

    private Vector2 cursorPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        cursorPosition = new Vector2(CursorTexture.width/2, CursorTexture.height/2);
        Cursor.SetCursor(CursorTexture, cursorPosition, CursorMode.Auto);
    }

}
