﻿using AutoMapper;
using EndpointsSystem.Data.Repository.Interface;
using EndpointsSystem.Domain.Entities;
using EndpointsSystem.Domain.Enums;
using EndpointSystem.Application.DTO;
using EndpointSystem.Application.Input.Model;
using EndpointSystem.Application.Services.Interfaces;
using System.Net.Http.Headers;

namespace EndpointSystem.Application.Services.Implementation
{
    public class EndpointService : IEndpointService
    {
        private readonly IEndpointRepository _endpointRepository;
        private readonly IMapper _mapper;

        public EndpointService(
            IEndpointRepository endpointRepository,
            IMapper mapper)
        {
            _endpointRepository = endpointRepository;
            _mapper = mapper;
        }

        public async Task CreateEndpoint(CreateEndpointInput createEndpointInput)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(createEndpointInput.EndpointSerialNumber!);

            if (existingEndpoint != null)
            {
                throw new ArgumentException("An endpoint already exists with this serial number.");
            }

            var endpoint = _mapper.Map<Endpoint>(createEndpointInput);

            await _endpointRepository.Create(endpoint);
            await _endpointRepository.SaveAsync();
        }

        public async Task EditEndpoint(string endpointSerialNumber, ESwitchState switchState)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(endpointSerialNumber!);

            if (existingEndpoint!.SwitchState == switchState)
            {
                throw new ArgumentException("The new switch state is the same as the current switch state. No changes made.");
            }

            existingEndpoint!.SwitchState = switchState;

            await _endpointRepository.Update(existingEndpoint);
            await _endpointRepository.SaveAsync();
        }

        public async Task DeleteEndpoint(string endpointSerialNumber)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(endpointSerialNumber!);

            await _endpointRepository.Delete(existingEndpoint!);
            await _endpointRepository.SaveAsync();
        }

        public async Task<EndpointDto> FindEndpoint(string endpointSerialNumber)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(endpointSerialNumber!) ?? throw new ArgumentException("The endpoint was not found.");

            var foundEndpoint = _mapper.Map<EndpointDto>(existingEndpoint);

            return foundEndpoint;
        }

        public async Task<List<EndpointDto>> ListAllEndpoints()
        {
            List<EndpointDto> endpointList = (await _endpointRepository.GetAllEndpoints())
                .Select(x => _mapper.Map<EndpointDto>(x)).ToList();

            return endpointList;
        }
    }
}
