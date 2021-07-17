using Infrastructure.Models;
using Infrastructure.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefenderServices.Interfaces
{
    public interface IMenuBuilderService
    {
        Task<IResultWithData<IList<MenuItem>>> GetAvailableMunuItems();

        Task<IResultWithData<MenuItem>> GetMunuItemById(int id);

        Task<IResult> AddMunuItems(MenuItem newMenuItem);

        Task<IResult> UpdateMunuItems(MenuItem newMenuItem);

        Task<IResult> RemoveMunuItems(int id);
    }
}
