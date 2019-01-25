// Cell information, that wil be serialized by the LevelLoader
[System.Serializable]
public class MKCellData
{
    public int PosX;
    public int PosY;
    public string PrefabName;
    public string RotationY;
    public EColor Color;
    public ECellType CellType;


    // Different colors for furniture
    public enum EColor
    {
        None = 0,
        Black,
        Blue,
        Red,
        Yellow
    }

    // Cell type
    public enum ECellType
    {
        Empty = 0,
        Movable,
        Decoration,
        Target,
        Player
    }
}
