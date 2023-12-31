﻿using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class HospitalInfoService : IHospitalInfo
    {
        private IUnitOfWork _unitOfWork;

        public HospitalInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteHospitalInfo(int id)
        {
           var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
           _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
           _unitOfWork.Save();
        }

        public List<HospitalInfoViewModel> GetAll()
        {
            var modeList = _unitOfWork.GenericRepository<HospitalInfo>()
                                       .GetAll()
                                       .ToList();

            return ConvertModelToViewModelList(modeList);
        }

        public PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new HospitalInfoViewModel();
            int totalCount;
            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modeList = _unitOfWork.GenericRepository<HospitalInfo>()
                                          .GetAll()
                                          .Skip(ExcludeRecords)
                                          .Take(pageSize)
                                          .ToList();

                totalCount = _unitOfWork.GenericRepository<HospitalInfo>()
                                        .GetAll()
                                        .ToList()
                                        .Count;

                vmList = ConvertModelToViewModelList(modeList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return new PagedResult<HospitalInfoViewModel>
            {
                Data       = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize   = pageSize
            };
        }

        public HospitalInfoViewModel GetHospitalById(int hospitalId)
        {
           var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(hospitalId);
            var vm = new HospitalInfoViewModel(model);
            return vm;
        }

        public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            var ModelById = _unitOfWork.GenericRepository<HospitalInfo>().GetById(model.Id);
            ModelById.Name = hospitalInfo.Name;
            ModelById.City = hospitalInfo.City;
            ModelById.PinCode = hospitalInfo.PinCode;
            ModelById.Country = hospitalInfo.Country;
            _unitOfWork.GenericRepository<HospitalInfo>().Update(ModelById);
            _unitOfWork.Save();

        }

        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList)
        {
            return modelList.Select(x=>new HospitalInfoViewModel(x)).ToList();
        }
    }
}
