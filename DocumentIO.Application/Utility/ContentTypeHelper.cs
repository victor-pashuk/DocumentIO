namespace DocumentIO.Application.Utility
{
    public static class ContentTypeHelper
    {
        public static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".txt" => "text/plain",
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                _ => "application/octet-stream",
            };
        }
    }
}

