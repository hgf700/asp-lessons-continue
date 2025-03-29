//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace aspapp.Services.Services
//{
//    public class GuideService : IGuideService
//    {
//        private readonly IGuideRepository _guideRepository;

//        public GuideService(IGuideRepository guideRepository)
//        {
//            _guideRepository = guideRepository;
//        }

//        public async Task<IEnumerable<Guide>> GetAllGuides()
//        {
//            return await _guideRepository.GetAllGuides();
//        }

//        public async Task<Guide> GetGuideDetails(int guideId)
//        {
//            // Tu można dodać logikę biznesową, np. sprawdzanie uprawnień, walidację itd.
//            var guide = await _guideRepository.GetGuideById(guideId);
//            if (guide == null)
//            {
//                throw new Exception("Guide not found");
//            }
//            return guide;
//        }

//        public async Task AddGuide(Guide guide)
//        {
//            // Można dodać walidację, przetwarzanie danych, itp.
//            if (string.IsNullOrEmpty(guide.Firstname))
//            {
//                throw new Exception("Firstname is required");
//            }

//            await _guideRepository.AddGuide(guide);
//        }
//    }

//}
