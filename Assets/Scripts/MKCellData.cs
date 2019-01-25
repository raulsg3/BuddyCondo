// Cell information, that wil be serialized by the LevelLoader
[System.Serializable]
public class MKCellData
{
    public int PosX { get; }
    public int PosY { get; }
    public string PrefabName { get; }
    public string RotationY { get; }
    public EColor Color { get; }
    public ECellType CellType { get; }


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
