using dropShippingApp.Repositories;

namespace InvoiceRepoTest
{
    internal class InvoiceController
    {
        private FakeInvoiceRepo repo;

        public InvoiceController(FakeInvoiceRepo repo)
        {
            this.repo = repo;
        }
    }
}