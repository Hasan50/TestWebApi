using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IThanaRepository
    {
        IEnumerable<Thana> GetThanaList(int count);
        ResponseModel SaveThana(Thana model);
    }
}
