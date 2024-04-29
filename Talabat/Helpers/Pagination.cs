using Talabat.Apis.DTOS;

namespace Talabat.Apis.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageSize, int pageIndex, IReadOnlyList<T> data , int Count )
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            count = Count;

        }

        public int PageSize {  get; set; }

        public int PageIndex { get; set; }

        public int count {  get; set; }

        public IReadOnlyList<T> Data { get; set; } 
    }
}
