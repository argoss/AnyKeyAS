using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Servicing.Requests
{
    public enum RequestStatus
    {
        [Display(Name = "П"), Description("Принята")]
        Accept = 0,
        [Display(Name = "Д-СЦ"), Description("Доставка в СЦ")]
        DeliverySC = 1,
        [Display(Name = "В-СЦ"), Description("Доставлена в СЦ")]
        DeliveredSC = 2,
        [Display(Name = "Из-СЦ"), Description("Обработана")]
        ProcessedSC = 3,
        [Display(Name = "ДК"), Description("Доставка клиенту")]
        DeliveryC = 4,
        [Display(Name = "И"), Description("Исполнена")]
        Performed = 5,
        [Display(Name = "В"), Description("Возврат")]
        Return = 6
    }
}
