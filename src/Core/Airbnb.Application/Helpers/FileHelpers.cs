using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Helpers
{
    public static class FileHelpers
    {
        public static async Task<string> FileCreate(this IFormFile file, string root, string folder)
        {

            string filename = $"{Guid.NewGuid()}.jpg";

            string path = Path.Combine(root, folder);
            string filePath = Path.Combine(path, filename);

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception)
            {
                throw new FileLoadException();
            }

            return filename;
        }

        public static void FileDelete(string root, string folder, string image)
        {
            string filePath = Path.Combine(root, folder, image);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static bool IsImageOkay(this IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 < mb && file.ContentType.Contains("image/");
        }
        public static string[] AllUserRelationIncludes()
        {
            string[] includes = new[] { "Gender","Host", "AppUserLanguages", "ReviewsByYou"
                , "ReviewsAboutYou", "ReservationsYouMade" };
            return includes;
        }
        public static string[] AllPropertyRelationIncludes()
        {
            string[] includes = new[] { "PropertyImages"
                , "PropertyAmenities", "PropertyAmenities.Amenity", "PropertyGroup", "PropertyType", "AirCover"
                , "CancellationPolicy", "PrivacyType","Reviews","Host","Reservations" };

            return includes;
        }


    }
}
