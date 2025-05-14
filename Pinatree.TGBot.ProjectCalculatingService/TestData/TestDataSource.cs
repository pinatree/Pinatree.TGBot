using Pinatree.TGBot.ProjectCalculatingService.Domain.Entities;
using System.Net.Http.Headers;

namespace Pinatree.TGBot.ProjectCalculatingService.MoqService.TestData
{
    public sealed class TestDataSource
    {
        List<ServiceType> _serviceTypes = new List<ServiceType>();
        List<ServiceSuboption> _serviceSuboptiions = new List<ServiceSuboption>();

        public TestDataSource()
        {
            InitTestData();
        }

        public IEnumerable<ServiceType> GetAvailableServiceTypes()
        {
            return _serviceTypes;
        }

        public IEnumerable<ServiceSuboption> GetAvailableOptions(long serviceTypeId)
        {
            var neededServiceType = _serviceTypes.FirstOrDefault(x => x.Id == serviceTypeId);

            if(neededServiceType != null)
            {
                return neededServiceType.AvailableSuboptions;
            }
            else
            {
                throw new Exception();
            }
        }

        public IEnumerable<ServiceSuboption> GetObligatoryOptions(long serviceTypeId)
        {
            var neededServiceType = _serviceTypes.FirstOrDefault(x => x.Id == serviceTypeId);

            if (neededServiceType != null)
            {
                return neededServiceType.ObligatorySuboptions;
            }
            else
            {
                throw new Exception();
            }
        }

        void InitTestData()
        {
            _serviceSuboptiions = new List<ServiceSuboption>()
            {
                new ServiceSuboption()
                {
                    Id = 0,
                    Name = "Разработка дизайна",
                    Days = 1,
                    Price = 5000
                },
                new ServiceSuboption()
                {
                    Id = 1,
                    Name = "SEO",
                    Days = 1,
                    Price = 15000
                },
                new ServiceSuboption()
                {
                    Id = 2,
                    Name = "Годовая поддержка",
                    Days = 0,
                    Price = 90000
                }
            };

            _serviceTypes = new List<ServiceType>()
            {
                new ServiceType()
                {
                    Id = 0,
                    Name = "Разработка сайта",
                    AvailableSuboptions = new List<ServiceSuboption>()
                    {
                        _serviceSuboptiions.First(x => x.Id == 0),
                        _serviceSuboptiions.First(x => x.Id == 1)
                    },
                },
                new ServiceType()
                {
                    Id = 1,
                    Name = "Лендинг",
                    AvailableSuboptions = new List<ServiceSuboption>()
                    {
                        _serviceSuboptiions.First(x => x.Id == 1)
                    },
                },
                new ServiceType()
                {
                    Id = 1,
                    Name = "AI решение",
                    AvailableSuboptions = new List<ServiceSuboption>()
                    {
                        _serviceSuboptiions.First(x => x.Id == 2)
                    },
                }
            };
        }
    }
}
