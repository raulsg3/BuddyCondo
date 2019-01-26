// Cell information, that will be serialized by the LevelLoader
[System.Serializable]
public class MKCellData
{
    public uint PosX;
    public uint PosY;
    public float RotationY;
    public string PrefabName;
    public EMKColor Color;
    public EMKCellType Type;

    //Cell type
    public enum EMKCellType
    {
        Empty = 0,
        Movable,
        Decoration,
        Target,
        Player1,
        Player2,
    }

    //Colors for characters and furniture
    public enum EMKColor
    {
        None = 0,
        Black,
        Blue,
        Red,
        Yellow,
        Green
    }

    //Cell typedef
    public struct TMKCell
    {
        public uint row;
        public uint col;
        public EMKCellType type;
    }
}
