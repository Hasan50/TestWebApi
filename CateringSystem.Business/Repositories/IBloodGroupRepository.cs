using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IBloodGroupRepository
    {
        IEnumerable<BloodGroup> GetBloodGroupList(int count);
        ResponseModel SaveBloodGroup(BloodGroup model);
    }
}
