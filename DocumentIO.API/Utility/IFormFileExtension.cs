namespace DocumentIO.API.Utility
{
    public static class IFormFileExtension
    {
        public async static Task<byte[]> ToArrayAsync(this IFormFile file)
        {
            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                data = memoryStream.ToArray();
            }
            return data;

        }
    }
}

