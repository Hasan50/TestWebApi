using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IPostOfficeRepository
    {
        IEnumerable<PostOffice> GetPostOfficeList(int count);
        ResponseModel SavePostOffice(PostOffice model);
    }
}
