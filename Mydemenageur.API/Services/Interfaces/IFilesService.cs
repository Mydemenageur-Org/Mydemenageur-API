using MongoDB.Driver.GridFS;
using Mydemenageur.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IFilesService
    {
        Task<string> UploadFile(string filename, byte[] fileBytes, bool shouldOverride = true);

        Task<FileModel> GetFile(string fileId);
        Task<FileModel> GetFileByName(string filename);
    }
}
