using EnterpriseSystems.Data.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Hydraters
{
    public class StopHydrater : IHydrater<StopVO>
    {
        public IEnumerable<StopVO> Hydrate(DataTable dataTable)
        {
            var Stops = new List<StopVO>();

            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var Stop = HydrateEntity(dataRow);
                    Stops.Add(Stop);
                }
            }

            return Stops;
        }

        private StopVO HydrateEntity(DataRow dataRow)
        {
            var Stop = new StopVO
            {
                Identity = (int)dataRow[StopColumnNames.Identity],
                EntityName = dataRow[StopColumnNames.EntityName].ToString(),
                EntityIdentity = (int)dataRow[StopColumnNames.EntityIdentity],
                RoleType = dataRow[StopColumnNames.RoleType].ToString(),
                StopNumber = (short)dataRow[StopColumnNames.StopNumber],
                CustomerAlias = dataRow[StopColumnNames.CustomerAlias].ToString(),
                OrganizationName = dataRow[StopColumnNames.OrganizationName].ToString(),
                AddressLine1 = dataRow[StopColumnNames.AddressLine1].ToString(),
                AddressLine2 = dataRow[StopColumnNames.AddressLine2].ToString(),
                AddressCityName = dataRow[StopColumnNames.AddressCityName].ToString(),
                AddressStateCode = dataRow[StopColumnNames.AddressStateCode].ToString(),
                AddressCountryCode = dataRow[StopColumnNames.AddressCountryCode].ToString(),
                AddressPostalCode = dataRow[StopColumnNames.AddressPostalCode].ToString(),
                CreatedDate = (DateTime?)dataRow[StopColumnNames.CreatedDate],
                CreatedUserId = dataRow[StopColumnNames.CreatedUserId].ToString(),
                CreatedProgramCode = dataRow[StopColumnNames.CreatedProgramCode].ToString(),
                LastUpdatedDate = (DateTime?)dataRow[StopColumnNames.LastUpdatedDate],
                LastUpdatedUserId = dataRow[StopColumnNames.LastUpdatedUserId].ToString(),
                LastUpdatedProgramCode = dataRow[StopColumnNames.LastUpdatedProgramCode].ToString()
            };

            //Stop.Appointments = GetAppointmentsByStop(Stop);
            //Stop.Comments = GetCommentsByStop(Stop);
            //Stop.ReferenceNumbers = GetReferenceNumbersByStop(Stop);
            //Stop.Stops = GetStopsByStop(Stop);

            return Stop;
        }
    }
}
