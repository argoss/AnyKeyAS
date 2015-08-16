using System;
using System.Web.Mvc;

namespace Site.Models.MainMenu
{
    public class MainMenuApiConfig
    {
        public string ServicingUrl { get; set; }

        public string UserUrl { get; set; }

        public string ClientsUrl { get; set; }

        public string RequestUrl { get; set; }

        public ViewPermissions[] Views { get; set; }
    }

    public class ViewPermissions
    {
        public ViewItems View { get; set; }

        public Permissions Permissions { get; set; }
    }

    public enum ViewItems
    {
        Clients,
        Users,
        Requests,
        Service
    }

    [Flags]
    public enum Permissions
    {
        Non,
        View,
        Edit,
        Create,
        Delete,
        FullControl = View | Create | Edit | Delete
    }
}