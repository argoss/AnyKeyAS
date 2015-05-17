namespace Site.Models.Clients
{
    public class ClientListViewModel
    {
        public ClientListViewModel()
        {
            List = new ClientViewModel[0];
        }

        public ClientViewModel[] List { get; set; }
    }

    public class ClientViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }


    }
}