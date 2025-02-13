using AutoMapper;
using PetShop.DomainEntities;
using PetShop.WebApi.Responses;

namespace PetShop.WebApi.Mappers {
    public class OrderMapperProfile :Profile {
        public OrderMapperProfile() {
            CreateMap<Order, OrderResponseModel>();
        }
    }
}
