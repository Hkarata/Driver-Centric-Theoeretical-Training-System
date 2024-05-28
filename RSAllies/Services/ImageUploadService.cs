namespace RSAllies.Services
{
    public static class ImageUploadService
    {
        public static string UploadImage(IFormFile image)
        {
            if (image == null)
            {
                return string.Empty;
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "questions", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return fileName;
        }
    }
}
