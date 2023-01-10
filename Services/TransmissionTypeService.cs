using System;
using System.Linq;
using CarRentalRestApi.Data;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Services
{
    public class TransmissionTypeService
    {
        private readonly DataContext _dataContext;

        public TransmissionTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public TransmissionType getTransmissionTypeByName(String name)
        {
            return _dataContext.TransmissionTypes.FirstOrDefault(tr => tr.name.Equals(name));
        }
        
    }
}