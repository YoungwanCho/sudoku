
public class SquareCell
{
    public int OrderIndex { get { return this._orderIndex; } }
    
    private int _orderIndex = 0;
    private int _value = 0;
    private bool _isOpenValue = false;

    public SquareCell(int cellIndex, int value, bool isOpenValue)
    {
        this._orderIndex = cellIndex;
        this._value = _value;
        this._isOpenValue = isOpenValue;
    }
}
