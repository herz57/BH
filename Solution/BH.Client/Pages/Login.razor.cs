using System.Threading.Tasks;
using BH.Common.Dtos;

namespace BH.Client.Pages
{
    public partial class Login
    {
        public LoginDto LoginData { get; set; }

        public Task LoginAsync()
        {
            throw new System.Exception();
        }
    }
}
