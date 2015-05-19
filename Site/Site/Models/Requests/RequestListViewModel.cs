namespace Site.Models.Requests
{
    public class RequestListViewModel
    {
        public RequestListViewModel()
        {
            List = new RequestViewModel[0];
        }

        public RequestViewModel[] List { get; set; }
    }

    public class RequestViewModel
    {
    }
}