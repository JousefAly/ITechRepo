using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public interface INotificationRespository
    {
        //Notify Single User
        //Return  Notification Id
        Task<string> Notify(string senderId, string receiverId, string message);

        //Notify List of Users
        //return array of created Notification Ids
        Task<string[]> Notify(string senderId, List<AppUser> receivers, string message);

    }
}
