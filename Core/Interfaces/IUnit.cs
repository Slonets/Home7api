using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnit
    {
        Task<UnitDto> GetById(int id);
        Task<IEnumerable<UnitDto>> GetAllUnits();
        Task AddUnit(CreateUnitDto createUnitDto);
        Task DeleteUnit(int id);
    }
}
