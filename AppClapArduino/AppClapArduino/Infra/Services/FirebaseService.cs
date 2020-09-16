using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Core
{
    public interface IFirebaseService
    {
        Task<LedStatus> ObterLedStatus();
        Task AtualizarLed(bool status);
    }
    public class FireBaseService : IFirebaseService
    {
        readonly FirebaseClient firebase = new FirebaseClient(AppConfiguration.FirebaseDatabaseUrl);

        public async Task AtualizarLed(bool status)
            => await firebase.Child("led_status").PutAsync(status);

        public async Task<LedStatus> ObterLedStatus()
           => (await firebase.Child("")
                 .OnceAsync<string>())
                 .Select(item => new LedStatus
                 {
                     led_status = item.Object.ToString()
                 }).FirstOrDefault();
    }
}