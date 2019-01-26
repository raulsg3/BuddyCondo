// Cell information, that will be serialized by the LevelLoader
[System.Serializable]
public class MKCellData
{
    public int PosX;
    public int PosY;
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
        Player
    }

    //Colors for characters and furniture
    public enum EMKColor
    {
        None = 0,
        Black,
        Blue,
        Red,
        Yellow
    }

    //Cell typedef
    public struct TMKCell
    {
        public uint row;
        public uint col;
        public EMKCellType type;
    }
}
