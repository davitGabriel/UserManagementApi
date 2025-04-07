using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManagementApi.DTOs;

namespace UsersManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGiftCardsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserGiftCardsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetGiftCardsByUser(int userId)
        {
            try
            {
                var userGiftCards = _unitOfWork.GiftCards.GetGiftCardsByUserId(userId).ToList();

                FileLogger.Log($"User with ID: {userId} has {userGiftCards.Count} gift card(s).");

                return Ok(userGiftCards);
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error retrieving gift cards for user with ID: {userId}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}")]
        public IActionResult PurchaseGiftCard(int userId, [FromBody] GiftCardDTO giftCardDTO)
        {
            try
            {
                UserGiftCard giftCard = new UserGiftCard();

                giftCard.UserId = userId;
                giftCard.Code ??= Guid.NewGuid().ToString("N").ToUpper();
                giftCard.Amount = giftCardDTO.Amount;
                giftCard.ExpirationDate = DateTime.UtcNow.AddYears(1);
                giftCard.PurchaseDate = DateTime.UtcNow;
                giftCard.IsRedeemed = false;

                _unitOfWork.GiftCards.Add(giftCard);
                _unitOfWork.Complete();

                FileLogger.Log($"Gift card purchased for user with ID: {userId}. Amount: {giftCardDTO.Amount}");

                return Ok(giftCard);
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error purchasing gift card for user with ID: {userId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{giftCardId}/redeem")]
        public IActionResult RedeemGiftCard(int giftCardId)
        {
            try
            {
                var card = _unitOfWork.GiftCards.GetById(giftCardId);
                if (card == null)
                    return NotFound();

                if (card.IsRedeemed)
                    return BadRequest("Gift card already redeemed.");

                if (card.ExpirationDate < DateTime.UtcNow)
                    return BadRequest("Gift card has expired.");

                card.IsRedeemed = true;
                _unitOfWork.Complete();

                FileLogger.Log($"Gift card with ID: {giftCardId} redeemed.");

                return Ok($"Gift card with ID: {giftCardId} redeemed.");
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error redeeming gift card with ID: {giftCardId}.");
                return BadRequest(ex.Message);
            }
        }
    }
}
