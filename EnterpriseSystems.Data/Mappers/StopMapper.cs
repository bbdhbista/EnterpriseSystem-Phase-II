using EnterpriseSystems.Data.DAO;
using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Entities;
using System.Collections.Generic;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Mappers
{
    public class StopMapper
    {
        private IDatabase Database { get; set; }
        private IHydrater<StopVO> Hydrater { get; set; }

        public StopMapper(IDatabase database, IHydrater<StopVO> hydrater)
        {
            Database = database;
            Hydrater = hydrater;
        }

        public IEnumerable<StopVO> GetStopsByCustomerRequest(CustomerRequestVO customerRequest)
        {
            const string selectQueryStatement = "SELECT * FROM REQ_ETY_OGN WHERE ETY_NM = 'CUS_REQ' AND ETY_KEY_I = @CUS_REQ_I";

            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(customerRequest.Identity, CustomerRequestQueryParameters.Identity);
            var result = Database.RunSelect(query);

            var StopsByCustomerRequest = Hydrater.Hydrate(result);

            return StopsByCustomerRequest;
        }

    }
}
