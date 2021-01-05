using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IBloodGroupRepository
    {
        IEnumerable<BloodGroup> GetBloodGroupList(int count);
        ResponseModel SaveBloodGroup(BloodGroup model);
    }
}
