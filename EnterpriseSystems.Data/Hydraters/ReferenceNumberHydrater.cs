using EnterpriseSystems.Data.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Hydraters
{
    public class ReferenceNumberHydrater : IHydrater<ReferenceNumberVO>
    {
        public IEnumerable<ReferenceNumberVO> Hydrate(DataTable dataTable)
        {
            var ReferenceNumbers = new List<ReferenceNumberVO>();

            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var ReferenceNumber = HydrateEntity(dataRow);
                    ReferenceNumbers.Add(ReferenceNumber);
                }
            }

            return ReferenceNumbers;
        }

        private ReferenceNumberVO HydrateEntity(DataRow dataRow)
        {
            var ReferenceNumber = new ReferenceNumberVO
            {
                Identity = (int)dataRow[ReferenceNumberColumnNames.Identity],
                EntityName = dataRow[ReferenceNumberColumnNames.EntityName].ToString(),
                EntityIdentity = (int)dataRow[ReferenceNumberColumnNames.EntityIdentity],
                //SoutheasternReferenceNumberType = dataRow["SLU_REF_NBR_TYP"].ToString(),       not included in the entity
                ReferenceNumber = dataRow[ReferenceNumberColumnNames.ReferenceNumber].ToString(),
                CreatedDate = (DateTime?)dataRow[ReferenceNumberColumnNames.CreatedDate],
                CreatedUserId = dataRow[ReferenceNumberColumnNames.CreatedUserId].ToString(),
                CreatedProgramCode = dataRow[ReferenceNumberColumnNames.CreatedProgramCode].ToString(),
                LastUpdatedDate = (DateTime?)dataRow[ReferenceNumberColumnNames.LastUpdatedDate],
                LastUpdatedUserId = dataRow[ReferenceNumberColumnNames.LastUpdatedUserId].ToString(),
                LastUpdatedProgramCode = dataRow[ReferenceNumberColumnNames.LastUpdatedProgramCode].ToString()
            };
            return ReferenceNumber;
        }
    }
}
