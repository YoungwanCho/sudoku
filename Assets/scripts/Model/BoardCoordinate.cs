
namespace model
{
    public class BoardCoordinate
    {
        public readonly int row;
        public readonly int column;

        public BoardCoordinate(int packIndex, int row, int column)
        {
            int packRow = packIndex / DefineData.MAX_ROW_COUNT;
            int packColumn = packIndex % DefineData.MAX_COLUMN_COUNT;

            this.row = (packRow * DefineData.MAX_ROW_COUNT) + row;
            this.column = (packColumn * DefineData.MAX_COLUMN_COUNT) + column;
        }
    }
}