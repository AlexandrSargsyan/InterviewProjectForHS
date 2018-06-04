namespace Forum.Common.Base
{
    public interface IPager
    {
       int CurrentPage { get; set; }
       int TotalCount { get; set; }

       int ShowPerPage { get; set; }    

       int Count { get; }
        
    }
}
