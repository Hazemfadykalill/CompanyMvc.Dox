﻿namespace CompanyMvc.Dox.PL.HelperLogic
{
    public static class DocumentSettings
    {
        public static string UploadingFile(IFormFile file,string FolderName)
        {

            //1. get Location Folder That Contains On All Files
            //string FilePath = $"D:\\FullStack.Net\\Route\\BackEnd\\Route\\Aliaa\\C#\\6_MVC\\CompanyMvc\\CompanyMvc.Dox.Solution\\CompanyMvc.Dox.PL\\wwwroot\\Files\\{FileName}";
            // in previous Line Is Static In Your PC So That We Need Make Dynamic
            //string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\Files\" + FileName;
            //or  + is Combine
           string FolderPath = Path.Combine(Directory.GetCurrentDirectory() ,@"\wwwroot\Files\" , FolderName);

            //2. Get File Name
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            //3 Get Location File (IMG,PDF ,video)
            string FilePath=Path.Combine(FolderPath,FileName);
            //4 save file as stream
            var FileStream = new FileStream(FilePath,FileMode.Create);
            file.CopyTo(FileStream);

            return FileName;
        }
    }
}
