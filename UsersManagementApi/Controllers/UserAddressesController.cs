using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UsersManagementApi.DTOs;

namespace UsersManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserAddressesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetAddressesByUser(int userId)
        {
            try
            {
                var userAddresses = _unitOfWork.Addresses.GetAddressByUserId(userId).ToList();

                if (userAddresses.Count > 0)
                {
                    FileLogger.Log($"User with ID: {userId} has {userAddresses.Count} address(es).");

                    return Ok(userAddresses);
                }
                else
                {
                    return Ok("There are no addresses for user");
                }
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error retrieving addresses for user with ID: {userId}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}")]
        public IActionResult CreateAddress(int userId, [FromBody] AddressDTO address)
        {
            try
            {
                if (address == null)
                    return BadRequest();

                UserAddress newAddress = new UserAddress();

                newAddress.UserId = userId;
                newAddress.Apartment = address.Apartment;
                newAddress.Street = address.Street;
                newAddress.ZipCode = address.ZipCode;
                newAddress.City = address.City;
                newAddress.Country = address.Country;


                _unitOfWork.Addresses.Add(newAddress);
                _unitOfWork.Complete();

                FileLogger.Log($"Address for user with ID: {newAddress.UserId} created.");

                return Ok(newAddress);
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error creating address for user.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{addressId}")]
        public IActionResult UpdateAddress(int addressId, [FromBody] AddressDTO updateAddress)
        {
            try
            {
                if (updateAddress == null)
                    return BadRequest();

                var address = _unitOfWork.Addresses.GetById(addressId);

                if (address == null)
                    return NotFound();

                _mapper.Map(updateAddress, address);

                address.Street = updateAddress.Street;
                address.City = updateAddress.City;
                address.ZipCode = updateAddress.ZipCode;
                address.Country = updateAddress.Country;

                _unitOfWork.Complete();

                FileLogger.Log($"Address with ID: {addressId} updated.");

                return Ok(updateAddress);
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error updating address with ID: {addressId}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{addressId}")]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                var address = _unitOfWork.Addresses.GetById(addressId);
                if (address == null)
                    return NotFound();

                _unitOfWork.Addresses.Remove(address);

                _unitOfWork.Complete();

                FileLogger.Log($"Address with ID: {addressId} deleted.");

                return Ok($"Address with ID: {addressId} deleted.");
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error deleting address with ID: {addressId}.");
                return BadRequest(ex.Message);
            }
        }
    }
}
