using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IThanaRepository
    {
        IEnumerable<Thana> GetThanaList(int count);
        ResponseModel SaveThana(Thana model);
    }
}
