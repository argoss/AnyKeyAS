using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Servicing.Requests
{
    public enum RequestStatus
    {
        [Display(Name = "Назначена")] 
        Appointed = 0,
		[Display(Name = "Выполняется")] 
        Performed = 1,
		[Display(Name = "Выполнена")] 
        Сomplete = 2
    }
}
