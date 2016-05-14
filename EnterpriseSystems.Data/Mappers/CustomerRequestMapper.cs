using EnterpriseSystems.Data.DAO;
using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Mappers
{
    public class CustomerRequestMapper
    {
        private IDatabase Database { get; set; }
        private IHydrater<CustomerRequestVO> Hydrater { get; set; }

        public CustomerRequestMapper(IDatabase database, IHydrater<CustomerRequestVO> hydrater)
        {
            Database = database;
            Hydrater = hydrater;
        }

        public CustomerRequestVO GetCustomerRequestByIdentity(CustomerRequestVO customerRequest)
        {
            const string selectQueryStatement = "SELECT * FROM CUS_REQ WHERE CUS_REQ_I = @CUS_REQ_I";

            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(customerRequest.Identity, CustomerRequestQueryParameters.Identity);
            var result = Database.RunSelect(query);

            var customerRequestByIdentity = Hydrater.Hydrate(result).FirstOrDefault();

            return customerRequestByIdentity;
        }

        public IEnumerable<CustomerRequestVO> GetCustomerRequestsByReferenceNumber(CustomerRequestVO customerRequest)
        {
            var referenceNumber = customerRequest.ReferenceNumbers.First().ReferenceNumber;

            const string selectQueryStatement = "SELECT A.* FROM CUS_REQ A, REQ_ETY_REF_NBR B WHERE "
                                                +"B.ETY_NM = 'CUS_REQ' AND B.ETY_KEY_I = A.CUS_REQ_I AND "
                                                +"B.REF_NBR = @REF_NBR";
            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(referenceNumber, CustomerRequestQueryParameters.ReferenceNumber);
            var result = Database.RunSelect(query);

            var customerRequestsByReferenceNumber = Hydrater.Hydrate(result);

            return customerRequestsByReferenceNumber;

        }

        public IEnumerable<CustomerRequestVO> GetCustomerRequestsByReferenceNumberAndBusinessName(CustomerRequestVO customerRequest)
        {
            var referenceNumber = customerRequest.ReferenceNumbers.First().ReferenceNumber;
            var businessName = customerRequest.BusinessEntityKey;

            const string selectQueryStatement = "SELECT A.* FROM CUS_REQ A, REQ_ETY_REF_NBR B WHERE "
                                                +"A.BUS_UNT_KEY_CH = @BUS_UNT_KEY_CH AND B.ETY_NM = 'CUS_REQ' "
                                                +"AND B.ETY_KEY_I = A.CUS_REQ_I AND B.REF_NBR = @REF_NBR";
            var query = Database.CreateQuery(selectQueryStatement);
            query.AddParameter(referenceNumber, CustomerRequestQueryParameters.ReferenceNumber);
            query.AddParameter(businessName, CustomerRequestQueryParameters.BusinessName);
            var result = Database.RunSelect(query);

            var customerRequestsByReferenceNumberAndBusinessName = Hydrater.Hydrate(result);

            return customerRequestsByReferenceNumberAndBusinessName;
        }
    }
}
