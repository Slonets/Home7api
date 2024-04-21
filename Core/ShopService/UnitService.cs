using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Core.ShopService
{
    public class UnitService(IFilesService filesService, IRepository<Unit> units, IRepository<ImagesUnit> images, IMapper _mapper) : IUnit
    {
        public async Task<IEnumerable<UnitDto>> GetAllUnits()
        {
            var unitOld = await units.GetAsync();

            var unitNew = _mapper.Map<List<UnitDto>>(unitOld);

            var imagesOld = await images.GetAsync();

            var imagesNew = _mapper.Map<List<ImagesUnitDto>>(imagesOld);            

            for (int i = 0; i < unitNew.Count(); i++)
            {
                unitNew[i].Image = imagesNew[i].Path;
            } 
            
            return (unitNew);
        }

        public async Task<UnitDto> GetById(int id)
        {
            var unitOld = await units.GetByIDAsync(id);

            if (unitOld != null) 
            { 
            var unitNew = _mapper.Map<UnitDto>(unitOld);

            var imagesOld = await images.GetAsync();           

            var imagesNew = _mapper.Map<List<ImagesUnitDto>>(imagesOld);

            var imageSeach = imagesNew.FirstOrDefault(x => x.UnitId == unitOld.Id);

            unitNew.Image = imageSeach.Path;

            return (unitNew);
            }
            else
            {
                throw new HttpRequestException("Введене id невірне");
            }
        }
        public async Task AddUnit(CreateUnitDto createUnitDto)
        {
            var unit = new Unit()
            {                
                Name = createUnitDto.Name
            };

            await units.InsertAsync(unit);
            await units.SaveAsync();

            if (createUnitDto.Image!=null)
            {
                images.InsertAsync(
                    new ImagesUnit
                    {
                        Path = filesService.ImageSave(createUnitDto.Image),
                        UnitId= unit.Id
                    }
                    );                
            }
            await units.SaveAsync();
        }

        public async Task DeleteUnit(int id)
        {
            var unit = await units.GetByIDAsync(id);            

            if (unit!=null)
            {
               var img = await images.GetAsync();

               var exisImg= img.FirstOrDefault(x=>x.UnitId==unit.Id);

                filesService.RemoveImage(exisImg.Path);

               await units.DeleteAsync(unit);
               await units.SaveAsync();
            }
        }        
    }
}
