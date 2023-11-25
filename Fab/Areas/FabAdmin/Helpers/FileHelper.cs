namespace FabAdmin.Helpers
{
    public static class FileHelper
    {
        public static string GetFilePath(string root, string folder, string file)
        {
            return Path.Combine(root, folder, file);
        }

        public static async Task SaveFileAsync(string path, IFormFile file)
        {
            using (FileStream stream = new(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
