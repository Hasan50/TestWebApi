using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IPostOfficeRepository
    {
        IEnumerable<PostOffice> GetPostOfficeList(int count);
        ResponseModel SavePostOffice(PostOffice model);
    }
}
