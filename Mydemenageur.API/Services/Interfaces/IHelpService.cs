using System;
using System.Collections.Generic;
using Mydemenageur.API.Models.Demands;
using Mydemenageur.API.Entities;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IHelpService
    {
        public Task<IList<Help>> GetAllHelpAnnounces(string type, string title, Nullable<DateTime> createdAt, string personNumber, Nullable<DateTime> timeNeeded, string start, string destination, bool isFlexible, bool isEmergency, Nullable<DateTime> plannifiedDate, string volume, bool askStart, bool askEnd, int size);
        public Task<Help> GetHelpAnnounceById(string id);
        public Task<string> CreateHelpAnnounce(string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, bool isFlexible, bool isEmergency, DateTime plannifiedDate, string volume, bool askStart, bool askEnd);
        public Task UpdateHelpAnnounce(string id, string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, bool isFlexible, bool isEmergency, DateTime plannifiedDate, string volume, bool askStart, bool askEnd);
        public Task DeleteHelpAnnounce(string id);
    }
}
