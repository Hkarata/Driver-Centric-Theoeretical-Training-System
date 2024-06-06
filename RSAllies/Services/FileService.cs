using RSAllies.Models;
using System.Text.Json;

namespace RSAllies.Services
{
    public static class FileService
    {
        public static IEnumerable<Data> GetDistricts()
        {
            var data = new List<Data>();

            using (var stream = new FileStream(@"Data\Districts.json", FileMode.Open))
            {
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                data = JsonSerializer.Deserialize<List<Data>>(json);
            }

            return data!;
        }

        public static IEnumerable<Data> GetRegions()
        {
            var data = new List<Data>();

            using (var stream = new FileStream(@"Data\Regions.json", FileMode.Open))
            {
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                data = JsonSerializer.Deserialize<List<Data>>(json);
            }

            return data!;
        }
    }
}
