using System.Threading;
using AdminSuite.CustomerCreation;
using Framework;
using MbUnit.Framework;

namespace AdminSuite
{
    [TestFixture(ApartmentState = ApartmentState.STA, TimeOut = FrameGlobals.TestCaseTimeOut)]
    public class AdminTests : AdminBase
    {
        private readonly CreateCustomers _createNewCustomer = new CreateCustomers();

        [Test]
        public string CreateSelfExcludedCustomer()
        {
            return _createNewCustomer.SelfExcludedCustomer(MyBrowser);
        }

        [Test]
        public void CreateCustomerBannedForSomeCountry()
        {
            _createNewCustomer.CustomerFrmBannedCntry(MyBrowser);
        }

        [Test]
        public void CreateCompanyExcludedCustomer()
        {
            _createNewCustomer.CustomerCompanyExclusion(MyBrowser);
        }

        [Test]
        public string CreateSuspendedCustomer()
        {
           return _createNewCustomer.SuspCustomer(MyBrowser);
        }

        public string CreateSelfExcludedCustomer(string user)
        {
            return _createNewCustomer.SelfExcludedCustomer(MyBrowser,user);
        }

        public string CreateSuspendedCustomer(string user)
        {
            return _createNewCustomer.SuspCustomer(MyBrowser,user);
        }


    }
}