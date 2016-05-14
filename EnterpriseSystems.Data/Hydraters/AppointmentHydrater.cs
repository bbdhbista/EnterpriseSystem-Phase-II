using EnterpriseSystems.Data.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Hydraters
{
    public class AppointmentHydrater : IHydrater<AppointmentVO>
    {
         public IEnumerable<AppointmentVO> Hydrate(DataTable dataTable){
            var Appointments = new List<AppointmentVO>();

            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var Appointment = HydrateEntity(dataRow);
                    Appointments.Add(Appointment);
                }
            }

            return Appointments;
        }

        private AppointmentVO HydrateEntity(DataRow dataRow)
        {
            var Appointment = new AppointmentVO
            {
                Identity = (int)dataRow[AppointmentColumnNames.Identity],
                EntityName = dataRow[AppointmentColumnNames.EntityName].ToString(),
                EntityIdentity = (int)dataRow[AppointmentColumnNames.EntityIdentity],
                SequenceNumber = (short?)dataRow[AppointmentColumnNames.SequenceNumber],
                FunctionType = dataRow[AppointmentColumnNames.FunctionType].ToString(),
                AppointmentBegin = (DateTime?)dataRow[AppointmentColumnNames.AppointmentBegin],
                AppointmentEnd = (DateTime?)dataRow[AppointmentColumnNames.AppointmentEnd],
                TimezoneDescription = dataRow[AppointmentColumnNames.TimezoneDescription].ToString(),
                Status = dataRow[AppointmentColumnNames.Status].ToString(),
                CreatedDate = (DateTime?)dataRow[AppointmentColumnNames.CreatedDate],
                CreatedUserId = dataRow[AppointmentColumnNames.CreatedUserId].ToString(),
                CreatedProgramCode = dataRow[AppointmentColumnNames.CreatedProgramCode].ToString(),
                LastUpdatedDate = (DateTime?)dataRow[AppointmentColumnNames.LastUpdatedDate],
                LastUpdatedUserId = dataRow[AppointmentColumnNames.LastUpdatedUserId].ToString(),
                LastUpdatedProgramCode = dataRow[AppointmentColumnNames.LastUpdatedProgramCode].ToString()
            };

            
            return Appointment;
        }
    }
}
