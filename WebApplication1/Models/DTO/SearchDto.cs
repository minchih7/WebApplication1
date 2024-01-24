namespace WebApplication1.Models.DTO
{
    public class SearchDto
    {
        //搜尋相關
        public string? Keyword {  get; set; }
        public int? CategoryId { get; set; } = 0; //
        //排序相關
        public string? SortBy {  get; set; }
        public string? SoryType { get; set; } = "asc"; //desc
        //分頁相關
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 9;
    }
}
