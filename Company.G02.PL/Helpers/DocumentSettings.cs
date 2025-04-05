namespace Company.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }

        public static void Delete(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

    }
}
