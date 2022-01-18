namespace Hotel.Api.Wrappers;

public class PaggedRequest
{
    private int _size;
    private int _page;

    public int Page
    {
        get => _page <= 0 ? 1 : _page;
        set => _page = value;
    }

    public int Size
    {
        get => _size <= 0 ? 100 : _size;
        set => _size = value;
    }
}