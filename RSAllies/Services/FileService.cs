using Newtonsoft.Json;
using RSAllies.Models;

namespace RSAllies.Services
{
    public static class FileService
    {
        public static List<Data> GetDistricts()
        {
            var data = new List<Data>();

            using (var stream = new FileStream(@"Data/Districts.json", FileMode.Open))
            {
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Data>>(json);
            }

            return data!.OrderBy(d => d.Name).ToList();
        }

        public static List<Data> GetRegions()
        {
            var data = new List<Data>();

            using (var stream = new FileStream(@"Data/Regions.json", FileMode.Open))
            {
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Data>>(json);
            }

            return data!.OrderBy(r => r.Name).ToList();
        }
    }
}
