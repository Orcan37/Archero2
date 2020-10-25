
using UnityEngine;

public class ColorOwner : MonoBehaviour
{

    public Color GetColorPlayer(Owner GetOwner)
    {
        Color getColor;
        switch (GetOwner)
        {
            case Owner.Player1:
                getColor = Color.blue;
                break;
            case Owner.Player2:
                getColor = Color.red;
                break;
            case Owner.Player3:
                getColor = Color.yellow;
                break;
            case Owner.Player4:
                getColor = Color.black;
                break;
            case Owner.Player5:
                getColor = Color.cyan;
                break;
            case Owner.Player6:
                getColor = Color.magenta;
                break;

            default:
                getColor = Color.white;
                break;
        }
        return getColor;

    }
}

public enum Owner : byte
{
    Player1,
    Player2,
    Player3,
    Player4,
    Player5,
    Player6,
    none,
}

public enum WhoControls : byte
{
    Player,
    AI,
    none,
}



