using RentAGym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace RentAGym.FileAccess.Services
{
    public class LocalFileService : IFileService
    {
        

        public async Task<string> SaveFileAsync(Stream stream, string fileName, bool setRandomName = true)
        {

            string newFName,filePath;

            do  //чтобы исключить коллизию имен файлов
            {
                newFName = setRandomName ? Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(fileName)) : fileName;
                filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", newFName);
            }
            while (File.Exists(filePath));

            using(var fileStream = File.Create(filePath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);
            }   // сохранение файла

           // var host = "https://" + _contextAccessor.HttpContext.Request.Host;

            return $"Images/{newFName}"; ;

        }
    }
}
