// Cell information, that wil be serialized by the LevelLoader
public class CellData
{
    public int PosX { get; }
    public int PosY { get; }
    public string PrefabName { get; }
    public string RotationY { get; }
    public EColor Color { get; }
    public EFurnitureType FurnitureType { get; }


    // Different colors for furniture
    public enum EColor
    {
        None = 0,
        Black,
        Blue,
        Red,
        Yellow
    }

    // Furniture type
    public enum EFurnitureType
    {
        Movable = 0,
        Decoration,
        Target
    }
}
