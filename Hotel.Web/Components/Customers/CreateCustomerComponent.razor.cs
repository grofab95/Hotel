using Hotel.Web.Dtos;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Customers
{
    public partial class CreateCustomerComponent
    {
        private CustomerDto _newCustomer = new CustomerDto();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task Create(CustomerDto newCustomer)
        {

        }
    }
}
