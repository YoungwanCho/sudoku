
namespace model
{
    public class BoardCoordinate
    {
        public readonly int column;
        public readonly int row;

        public BoardCoordinate(int packIndex, int column, int row)
        {
            int packColumn = packIndex % DefineData.MAX_COLUMN_COUNT;
            int packRow = packIndex / DefineData.MAX_ROW_COUNT;

            this.column = (packColumn * DefineData.MAX_COLUMN_COUNT) + column;
            this.row = (packRow * DefineData.MAX_ROW_COUNT) + row;
        }
    }
}