using System.Collections;

namespace Hotel.Api.Wrappers
{
    public class PagedResponse<T> : Response<T> where T : ICollection
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public int Records { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(T data, int total, PaggedRequest paggedRequest) : base(data)
        {
            Page = paggedRequest.Page;
            Size = paggedRequest.Size;
            Records = data.Count;
            TotalRecords = total;
            TotalPages = (total / Size) + 1;
        }
    }
}
