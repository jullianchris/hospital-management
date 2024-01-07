using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomService : IRoomService
    {

        private IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            _unitOfWork.GenericRepository<Room>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<RoomViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new RoomViewModel();
            int totalCount;
            List<RoomViewModel> vmList = new List<RoomViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modeList = _unitOfWork.GenericRepository<Room>()
                                          .GetAll(includeProperties:"Hospital")
                                          .Skip(ExcludeRecords)
                                          .Take(pageSize)
                                          .ToList();

                totalCount = _unitOfWork.GenericRepository<Room>()
                                        .GetAll()
                                        .ToList()
                                        .Count;

                vmList = ConvertModelToViewModelList(modeList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return new PagedResult<RoomViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public RoomViewModel GetRoomById(int RoomId)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(RoomId);
            var vm = new RoomViewModel(model);
            return vm;
        }

        public void InsertRoom(RoomViewModel room)
        {
            var model = new RoomViewModel().ConvertViewModel(room);
            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModel room)
        {
            var model = new RoomViewModel().ConvertViewModel(room);
            var ModelById = _unitOfWork.GenericRepository<Room>().GetById(model.Id);
            ModelById.Type = room.Type;
            ModelById.RoomNumber = room.RoomNumber;
            ModelById.Status = room.Status;
            model.HospitalId = room.HospitalInfoId;

            _unitOfWork.GenericRepository<Room>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<RoomViewModel> ConvertModelToViewModelList(List<Room> modelList)
        {
            return modelList.Select(x=> new RoomViewModel(x)).ToList();
        }
    }
}
