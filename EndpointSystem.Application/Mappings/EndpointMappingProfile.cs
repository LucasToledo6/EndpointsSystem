﻿using AutoMapper;
using EndpointsSystem.Domain.Entities;
using EndpointSystem.Application.DTO;
using EndpointSystem.Application.Input.Model;

namespace EndpointSystem.Application.Mappings
{
    public class EndpointMappingProfile : Profile
    {
        public EndpointMappingProfile()
        {
            CreateMap<CreateEndpointInput, Endpoint>();
            CreateMap<Endpoint, EndpointDto>();
        }
    }
}
