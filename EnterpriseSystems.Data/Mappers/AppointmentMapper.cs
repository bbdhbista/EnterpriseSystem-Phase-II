using EnterpriseSystems.Data.DAO;
using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Entities;
using System.Collections.Generic;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Mappers
{
    public class AppointmentMapper
    {
        private IDatabase Database { get; set; }
        private IHydrater<AppointmentVO> Hydrater { get; set; }

        public AppointmentMapper(IDatabase database, IHydrater<AppointmentVO> hydrater)
        {
            Database = database;
            Hydrater = hydrater;
        }

        public IEnumerable<AppointmentVO> GetAppointmentsByStop(StopVO stop)
        {
            const string selectQueryStatement = "SELECT * FROM REQ_ETY_SCH WHERE ETY_NM = 'REQ_ETY_OGN' AND ETY_KEY_I = @REQ_ETY_OGN_I";

            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(stop.Identity, StopRequestQueryParameters.Identity);
            var result = Database.RunSelect(query);

            var AppointmentsByStop = Hydrater.Hydrate(result);
        
            return AppointmentsByStop;
        }

        public IEnumerable<AppointmentVO> GetAppointmentsByCustomerRequest(CustomerRequestVO customerRequest)
        {
            const string selectQueryStatement = "SELECT * FROM REQ_ETY_SCH WHERE ETY_NM = 'CUS_REQ' AND ETY_KEY_I = @CUS_REQ_I";

            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(customerRequest.Identity, CustomerRequestQueryParameters.Identity);
            var result = Database.RunSelect(query);

            var AppointmentsByCustomerRequest = Hydrater.Hydrate(result);

            return AppointmentsByCustomerRequest;
        }
    }



    }

