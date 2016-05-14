using EnterpriseSystems.Data.DAO;
using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Constants;
using EnterpriseSystems.Data.Model.Entities;
using System.Collections.Generic;

namespace EnterpriseSystems.Data.Mappers
{
    public class ReferenceNumberMapper
    {
        private IDatabase Database { get; set; }
        private IHydrater<ReferenceNumberVO> Hydrater { get; set; }

        public ReferenceNumberMapper(IDatabase database, IHydrater<ReferenceNumberVO> hydrater)
        {
            Database = database;
            Hydrater = hydrater;
        }

        public IEnumerable<ReferenceNumberVO> GetReferenceNumbersByCustomerRequest(CustomerRequestVO customerRequest)
        {
            const string selectQueryStatement = "SELECT * FROM REQ_ETY_REF_NBR WHERE ETY_NM = 'CUS_REQ' AND ETY_KEY_I = @CUS_REQ_I";

            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(customerRequest.Identity, CustomerRequestQueryParameters.Identity);
            var result = Database.RunSelect(query);

            var referenceNumberByCustomerRequest = Hydrater.Hydrate(result);

            return referenceNumberByCustomerRequest;
            
        }
    }
}

