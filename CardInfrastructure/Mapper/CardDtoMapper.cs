using CardInfrastructure.DTO;
using CardInfrastructure.Models;
using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Mapper
{
    public static class CardDtoMapper
    {
        public static UpdateCardImageDto? MapToImageDto(CardImage? image)
        {
            if (image == null) return null;

            return new UpdateCardImageDto
            {
                ImageUrl = image.ImageUrl,
                IsUserUploaded = image.IsUserUploaded,
                IsPlaceHolder = image.IsPlaceholder,
                UploadedAt = image.UploadedAt.ToString("yyyy-MM-dd")
            };
        }

        public static UpdateCardGradingDto? MapToGradingDto(CardGrading? grading)
        {
            if (grading == null) return null;

            return new UpdateCardGradingDto
            {
                CompanyName = grading.CompanyName,
                Grade = grading.Grade,
                CertificationNumber = grading.CertificationNumber,
                GradedDate = grading.GradedDate
            };
        }

        public static ResponseCardDto MapToResponseDto(Card card)
        {
            return new ResponseCardDto
            {
                Id = card.Id,
                Name = card.Name,
                Condition = card.Condition.ToString(),
                Value = card.Value,
                BinderId = card.BinderId,
                GradingId = card.GradingId,
                CreatedAt = card.CreatedAt.ToString("yyyy-MM-dd"),
                Image = MapToImageDto(card.Image),
                Grading = MapToGradingDto(card.Grading)
            };
        }

    }
}
