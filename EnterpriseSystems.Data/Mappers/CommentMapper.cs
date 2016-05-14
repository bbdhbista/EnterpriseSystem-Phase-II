using EnterpriseSystems.Data.DAO;
using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Entities;
using System.Collections.Generic;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Mappers
{
    public class CommentMapper
    {
        private IDatabase Database { get; set; }
        private IHydrater<CommentVO> Hydrater { get; set; }

        public CommentMapper(IDatabase database, IHydrater<CommentVO> hydrater)
        {
            Database = database;
            Hydrater = hydrater;
        }

        public IEnumerable<CommentVO> GetCommentsByCustomerRequest(CustomerRequestVO customerRequest)
        {
            var cus_Request = customerRequest.Identity;
            const string selectQueryStatement = "SELECT * FROM REQ_ETY_CMM WHERE ETY_NM = 'CUS_REQ' AND ETY_KEY_I= @CUS_REQ_I";

            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(cus_Request, CustomerRequestQueryParameters.Identity);
            var result = Database.RunSelect(query);

            var CommentsByCustomerRequest = Hydrater.Hydrate(result);

            return CommentsByCustomerRequest;

        }
        public IEnumerable<CommentVO> GetCommentsByStop(StopVO stop)
        {
            const string selectQueryStatement = "SELECT * FROM REQ_ETY_CMM WHERE ETY_NM = 'REQ_ETY_OGN' AND ETY_KEY_I = @REQ_ETY_OGN_I";

            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(stop.Identity, StopRequestQueryParameters.Identity);
            var result = Database.RunSelect(query);

            var CommentsByStop = Hydrater.Hydrate(result);

            return CommentsByStop;
        }


    }
}
