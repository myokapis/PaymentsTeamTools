using BatchMonitoring.Models;

namespace BatchMonitoring.Services
{

    public class DataService : IDataService
    {
        public SessionScope GetSessionScope()
        {

            return new SessionScope();
        }
    }

}
