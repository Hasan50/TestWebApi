using ToDoApp.Business.Models;
using System;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IDeliveryManPackageQuantityRepository
    {
        IEnumerable<object> GetDeliveryManPackageList(int count, string deliveryManId, DateTime date);
        ResponseModel SavePackageQuantity(List<DeliveryManPackageQuantity> model);
        ResponseModel Delete(DeliveryManPackageQuantity model);
        List<DeliveryManPackageDeliveryStatusViewModel> GetDeliveryStatusList(string deliveryManId, DateTime date);
        IEnumerable<object> GetExtraDeliveryStatusList(string deliveryManId, DateTime date);
    }
}
