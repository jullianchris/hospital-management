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
    public class DoctorService : IDoctorService
    {
        private IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteTiming(int Timingid)
        {
            var model = _unitOfWork.GenericRepository<Timing>().GetById(Timingid);
            _unitOfWork.GenericRepository<Timing>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new TimingViewModel();
            int totalCount;
            List<TimingViewModel> vmList = new List<TimingViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modeList       = _unitOfWork.GenericRepository<Timing>()
                                          .GetAll()
                                          .Skip(ExcludeRecords)
                                          .Take(pageSize)
                                          .ToList();

                totalCount         = _unitOfWork.GenericRepository<Timing>()
                                        .GetAll()
                                        .ToList()
                                        .Count;

                vmList = ConvertModelToViewModelList(modeList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return new PagedResult<TimingViewModel>
            {
                Data       = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize   = pageSize
            };
        }

        private List<TimingViewModel> ConvertModelToViewModelList(List<Timing> modelList)
        {
            return modelList.Select(x => new TimingViewModel(x)).ToList();
        }

        public IEnumerable<TimingViewModel> GetAll()
        {
            var timingList = _unitOfWork.GenericRepository<Timing>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(timingList);
            return vmList;
        }

        public TimingViewModel GetTimingById(int TimingId)
        {
            var model = _unitOfWork.GenericRepository<Timing>().GetById(TimingId);
            return new TimingViewModel(model);
        }

        public void InsertTiming(TimingViewModel Timing)
        {
            var model = new TimingViewModel().ConvertViewModel(Timing);
            _unitOfWork.GenericRepository<Timing>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateTiming(TimingViewModel Timing)
        {
            var model                           = new TimingViewModel().ConvertViewModel(Timing);
            var modelById                       = _unitOfWork.GenericRepository<Timing>().GetById(model.Id);
            modelById.Id                        = Timing.Id;
            modelById.DoctorId                  = Timing.DoctorId;
            modelById.Status                    = Timing.Status;
            modelById.Duration                  = Timing.Duration;
            modelById.MorningShiftEndTime       = Timing.MorningShiftEndTime;
            modelById.MorningShiftStartTime     = Timing.MorningShiftStartTime;
            modelById.AfternoonShiftEndTime     = Timing.AfternoonShiftEndTime;
            modelById.AfternoonShiftStartTime   = Timing.AfternoonShiftStartTime;

            _unitOfWork.GenericRepository<Timing>().Update(modelById);
            _unitOfWork.Save();
        }
    }
}
