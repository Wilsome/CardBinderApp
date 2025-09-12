using CardInfrastructure.DTO;
using CardInfrastructure.Models;
using CardInfrastructure.Services;
using CardLibrary.Enums;
using CardLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBinderApp.Tests.Services
{
    public class CardServiceTests
    {
        [Fact]
        public async Task UpdateCardAsync_DtoValid()
        {
            CardService _cardService = CreateIsolatedService();

            //arrange, setup the test
            Card card = new() { Name = "black lotus", Value = 100m, GradingId = null };

            UpdateCardDto updatedCardDto = new() { Name = "Black Lotus", Value = 250m, GradingId = "22" };

            //act, call the test method
            //since this method returns a CardBase we need to capture it
            CardBase updatedCard = await _cardService.UpdateCardAsync(card, updatedCardDto);


            //assert effects match expectations. 
            Assert.Equal(updatedCard.Name, updatedCardDto.Name);
            Assert.Equal(updatedCard.Value, updatedCardDto.Value);
            Assert.Equal(updatedCard.GradingId, updatedCardDto.GradingId);

        }

        [Fact]
        public async Task UpdateCardAsync_SingleField()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            //create a card and save it
            Card card = new()
            {
                Name = "Mox Pearl",
                Value = 200m,
                Condition = CardCondition.Excellent,
                BinderId = "2",
                Image = null,
                Grading = null,
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            Assert.Equal(200m, card.Value); // before mutation


            //create a cardDto
            UpdateCardDto cardDto = new()
            {
                Value = 250m
            };

            //Act
            await _cardService.UpdateCardAsync(card, cardDto);

            //Assert
            Assert.Equal("Mox Pearl", card.Name);
            Assert.Equal(CardCondition.Excellent, card.Condition);
            Assert.Equal(250m, card.Value); // post-mutation check
            Assert.Equal("2", card.BinderId);
            Assert.Null(card.Image);
            Assert.Null(card.Grading);

        }

        [Fact]
        public async Task UpateCardAsync_UpdateDtoBinderIdEmpty()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            //Seed new card object
            Card card = new()
            {
                Name = "Force Of Nature",
                Value = 120m,
                Condition = CardCondition.NearMint,
                BinderId = "2",
            };

            //Add & save
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //create an UpdateCardDto
            UpdateCardDto cardDto = new()
            {
                BinderId = " "
            };

            //Act
            await _cardService.UpdateCardAsync(card, cardDto);

            //pull card from context
            CardBase pulledCard = await _cardService.GetCardByIdAsync(card.Id);

            //Assert
            Assert.NotNull(pulledCard);
            Assert.Equal(card.Name, pulledCard.Name);
            // Confirm that whitespace BinderId does not mutate the card
            Assert.NotEqual(" ", pulledCard.BinderId);

        }

        [Fact]
        public async Task UpdateCardAsync_UpdateDtoBinderIdValid()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            //seed a card
            Card card = new()
            {
                Name = "Black Lotus",
                Value = 250m,
                Condition = CardCondition.Good,
                BinderId = "22"
            };

            //add & save
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //create a binder
            CardBinder binder = new()
            {
                Name = "Magic trade binder",
                CollectionId = "5"
            };

            //add & save
            _context.Binders.Add(binder);
            await _context.SaveChangesAsync();

            //create a cardDto
            UpdateCardDto cardDto = new()
            {
                BinderId = binder.Id,
            };

            //Act
            await _cardService.UpdateCardAsync(card, cardDto);

            //pullcard
            CardBase pulledCard = await _cardService.GetCardByIdAsync(card.Id);

            //Assert
            Assert.NotNull(pulledCard);
            Assert.NotEqual("22", pulledCard.BinderId);
            Assert.Equal(card.Name, pulledCard.Name);
            Assert.Equal(pulledCard.BinderId, binder.Id);

        }

        [Fact]
        public async Task UpdateCardAsync_UpdateDtoBinderIdInvalid()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            // Arrange
            Card card = new()
            {
                Name = "Time Twister",
                Value = 285m,
                Condition = CardCondition.None,
                BinderId = "22"
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            UpdateCardDto cardDto = new()
            {
                BinderId = "9999" // invalid
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _cardService.UpdateCardAsync(card, cardDto);
            });

            Assert.Contains("Binder ID '9999' does not exist", ex.Message);
        }

        [Fact]
        public async Task UpdateCardAsync_UpdateDtoGradingIdNull()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            //create a card, seed with a validate gradingId
            Card card = new()
            {
                Name = "Black Lotus",
                Value = 250m,
                Condition = CardCondition.Excellent,
                BinderId = "2",
                GradingId = "15"
            };

            //add and save card
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            CardBase savedCard = await _context.Cards.FindAsync(card.Id);
            Assert.Equal("15", savedCard.GradingId);

            //create cardUpdateDto
            UpdateCardDto updateCardDto = new()
            {
                GradingId = null
            };

            //Act
            await _cardService.UpdateCardAsync(card, updateCardDto);

            //Assert
            Assert.NotEmpty(_context.Cards);
            Assert.Null(card.GradingId);
        }

        [Fact]
        public async Task UpdateCardAsync_UpdateDtoAllNullValues()
        {
            (CardDbContext _context, CardService _cardServices) = CreateIsolatedTestHarness();

            //Arrange
            //Seed a card with null GradingID
            Card card = new()
            {
                Name = "Black Lotus",
                Value = 300m,
                Condition = CardCondition.Unknown,
                BinderId = "2",
                GradingId = null
            };

            //add card to context and save
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //Act
            await _cardServices.UpdateCardAsync(card, new UpdateCardDto() { });

            //Assert
            Assert.Equal("Black Lotus", card.Name);
            Assert.Equal(300m, card.Value);
            Assert.Equal(CardCondition.Unknown, card.Condition);
            Assert.Equal("2", card.BinderId);
            Assert.Null(card.GradingId);
        }

        [Fact]
        public async Task UpdateCardAsync_UpdateDtoNegativeValue()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            //seed a card
            Card card = new()
            {
                Name = "Black Lotus",
                Value = 200m,
                Condition = CardCondition.NearMint,
                BinderId = "2",
                GradingId = null
            };
            //add and save
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //Dto
            UpdateCardDto cardDto = new()
            {
                Value = -50m
            };

            //Act
            await _cardService.UpdateCardAsync(card, cardDto);

            //Assert
            Assert.NotEqual(cardDto.Value, card.Value);
        }

        [Fact]
        public async Task UpdateCardAsync_UpdateImageDtoValid()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            //create a card and populate with valid image object
            Card card = new()
            {
                Name = "Black Lotus",
                Value = 200m,
                Condition = CardCondition.Poor,
                BinderId = "2",
                GradingId = null,
                Image = new()
                {
                    ImageUrl = "www.image.com",
                    IsUserUploaded = false,
                    IsPlaceholder = true,
                }
            };
            //add and save to context
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //create a new imageDto
            UpdateCardImageDto imageDto = new()
            {
                ImageUrl = "www.newImage.com",
                IsUserUploaded = true,
                IsPlaceHolder = false
            };

            //create updateCardDto
            UpdateCardDto cardDto = new()
            {
                //update the image object
                Image = imageDto
            };


            //Act
            await _cardService.UpdateCardAsync(card, cardDto);

            //pull card from our temp db
            CardBase pulledCard = await _cardService.GetCardByIdAsync(card.Id);


            //Assert
            Assert.NotNull(pulledCard);
            Assert.Equal(pulledCard.Id, card.Id);
            Assert.Equal(pulledCard.Image.ImageUrl, card.Image.ImageUrl);
            Assert.False(pulledCard.Image.IsPlaceholder);
            Assert.True(pulledCard.Image.IsUserUploaded);

        }

        [Fact]
        public async Task UpdateCardAsync_UpdateImageDtoNull()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            //create a full card with image object
            Card card = new()
            {
                Name = "Time Walk",
                Value = 150.44m,
                Condition = CardCondition.None,
                BinderId = "22",
                Image = new()
                {
                    ImageUrl = "www.originalUrl.com",
                    IsUserUploaded = true,
                    IsPlaceholder = false
                }
            };

            //add and save
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //create updateCardDto
            UpdateCardDto cardDto = new()
            {
                Image = null
            };

            //Act
            await _cardService.UpdateCardAsync(card, cardDto);

            //pull card
            CardBase pulledCard = await _cardService.GetCardByIdAsync(card.Id);

            //Assert
            Assert.Equal(card.Id, pulledCard.Id);
            Assert.NotNull(pulledCard.Image);
            Assert.NotEmpty(_context.Cards);
            Assert.Equal("www.originalUrl.com", pulledCard.Image.ImageUrl);
            Assert.False(pulledCard.Image.IsPlaceholder);
            Assert.True(pulledCard.Image.IsUserUploaded);
            Assert.Equal(pulledCard.Name, card.Name);
        }

        [Fact]
        public async Task CreateCardAsync_DtoNullImage()
        {
            CardService _cardService = CreateIsolatedService();
            //arrange
            CreateCardDto cardDto = new() { Name = "Black Lotus", Value = 200m, Condition = CardLibrary.Enums.CardCondition.Excellent, BinderId = "22", Image = null };

            //act. Create a card from the cardDto
            CardBase card = await _cardService.CreateCardAsync(cardDto);

            //assert
            Assert.Equal(card.Name, cardDto.Name);
            Assert.Equal(card.Value, cardDto.Value);
            Assert.Equal(card.Condition, cardDto.Condition);
            Assert.Equal(card.BinderId, cardDto.BinderId);
            Assert.Null(card.Image);
        }

        [Fact]
        public async Task CreateCardAsync_DtoNullGrading()
        {
            CardService _cardService = CreateIsolatedService();

            //Arrange, create a CreateCardDto
            CreateCardDto cardDto = new()
            {
                Name = "Atog",
                Value = .20m,
                Condition = CardLibrary.Enums.CardCondition.NearMint,
                BinderId = "5",
                Grading = null
            };

            //Act, create a card from our dto
            CardBase card = await _cardService.CreateCardAsync(cardDto);

            //Assert, Compare field from card and cardDto
            Assert.Equal(card.Name, cardDto.Name);
            Assert.Equal(card.Value, cardDto.Value);
            Assert.Equal(card.Condition, cardDto.Condition);
            Assert.Equal(card.BinderId, cardDto.BinderId);
            Assert.Null(card.Grading);
        }

        [Fact]
        public async Task CreateCardAsync_DtoComplete()
        {
            CardService _cardService = CreateIsolatedService();

            //Arrange, need a full cardDto with nested image and grading object
            CreateCardDto cardDto = new()
            {
                Name = "Force of Nature",
                Value = 1.22m,
                Condition = CardLibrary.Enums.CardCondition.NearMint,
                BinderId = "5",
                Grading = new()
                {
                    CompanyName = "Beckett",
                    CertificationNumber = "12345",
                    Grade = 9,
                    GradedDate = DateOnly.FromDateTime(DateTime.UtcNow)

                },
                Image = new()
                {
                    ImageUrl = "www.image.com",
                    IsPlaceHolder = false,
                    IsUserUploaded = true
                }
            };

            //arrange, create the card from the dto
            CardBase card = await _cardService.CreateCardAsync(cardDto);

            //Assert. Compare the fields and the object created in the card object
            Assert.Equal(card.Name, cardDto.Name);
            Assert.Equal(card.Value, cardDto.Value);
            Assert.Equal(card.Condition, cardDto.Condition);
            Assert.Equal(card.BinderId, cardDto.BinderId);
            Assert.Equal(card.Image.ImageUrl, cardDto.Image.ImageUrl);
            Assert.Equal(card.Image.IsUserUploaded, cardDto.Image.IsUserUploaded);
            Assert.Equal(card.Grading.CompanyName, cardDto.Grading.CompanyName);
            Assert.Equal(card.Grading.Grade, cardDto.Grading.Grade);
            Assert.Equal(card.Grading.CertificationNumber, cardDto.Grading.CertificationNumber);
            Assert.Equal(card.Grading.GradedDate, cardDto.Grading.GradedDate);

        }

        [Fact]
        public async Task CreateCardAsync_DtoValueNull()
        {
            CardService _cardService = CreateIsolatedService();

            //Arrange, create our cardDto
            CreateCardDto cardDto = new()
            {
                Name = "Mox Pearl",
                Value = null,
                Condition = CardLibrary.Enums.CardCondition.Good,
                BinderId = "18"
            };

            //Act, create our card from the dto
            CardBase card = await _cardService.CreateCardAsync(cardDto);

            //Assert, that our logic set the cards default value to 0 when null is provided. 
            Assert.True(card.Value == 0);
        }

        [Fact]
        public async Task DeleteCardByIdAsync_ValidId()
        {
            // Setup: create isolated context and service
            (CardDbContext context, CardService service) = CreateIsolatedTestHarness();

            // Arrange: seed binder
            CardBinder binder = new CardBinder
            {
                Id = "22",
                Name = "Trade Binder",
                EstimatedValue = 250m,
                CollectionId = "5"
            };

            context.Binders.Add(binder);
            await context.SaveChangesAsync();

            // Arrange: create card DTO
            CreateCardDto cardDto = new CreateCardDto
            {
                Name = "Blacker Lotus",
                Value = 20m,
                Condition = CardLibrary.Enums.CardCondition.Excellent,
                BinderId = "22",
                Grading = null,
                Image = null
            };

            // Act: create card via service
            CardBase card = await service.CreateCardAsync(cardDto);

            // Act: delete card via service
            bool result = await service.DeleteCardByIdAsync(card.Id);

            // Assert: deletion result is true
            Assert.True(result);

            // Assert: card no longer exists
            Card deletedCard = await context.Cards.FindAsync(card.Id);
            Assert.Null(deletedCard);

            // Assert: binder no longer contains the card
            CardBinder updatedBinder = await context.Binders
                .Include(b => b.Cards)
                .SingleAsync(b => b.Id == binder.Id);

            Assert.DoesNotContain(updatedBinder.Cards, c => c.Id == card.Id);

            // Assert: total card count is zero
            int cardCount = await context.Cards.CountAsync();
            Assert.Equal(0, cardCount);
        }

        [Fact]
        public async Task GetCardByIdAsync_ValidId()
        {
            CardService _cardService = CreateIsolatedService();

            CreateCardDto cardDto = new()
            {
                Name = "Black Lotus",
                Value = 200m,
                Condition = CardLibrary.Enums.CardCondition.VeryGood,
                BinderId = "22",
                Grading = null,
                Image = null,
            };

            CardBase card = await _cardService.CreateCardAsync(cardDto);

            // Act: Retrieve the card
            CardBase selectedCard = await _cardService.GetCardByIdAsync(card.Id);

            // Assert: Validate fields
            Assert.NotNull(selectedCard);
            Assert.Equal(card.Id, selectedCard.Id);
            Assert.Equal(card.Name, selectedCard.Name);
            Assert.Equal(card.Value, selectedCard.Value);
            Assert.Equal(card.Condition, selectedCard.Condition);
            Assert.Equal(card.BinderId, selectedCard.BinderId);
        }

        [Fact]
        public async Task GetCardByIdAsync_InvalidId()
        {
            //need access to our temp Db
            CardService _cardService = CreateIsolatedService();

            //arrange
            string cardId = "1234";

            //act
            Card card = await _cardService.GetCardByIdAsync(cardId);

            //assert
            Assert.Null(card);

        }

        [Fact]
        public async Task DeleteCardByIdAsync_InvalidId()
        {
            //CardService _cardServie = CreateIsolatedService();
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange, sample input id
            string id = "1234";

            //Act
            bool result = await _cardService.DeleteCardByIdAsync(id);

            //Assert
            Assert.False(result);
            Assert.True(_context.Cards.Count() == 0);
        }

        [Fact]
        public async Task DeleteCardByIdAsync_NullId()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange
            string id = null;

            //Act
            bool result = await _cardService.DeleteCardByIdAsync(id);

            //Assert
            Assert.False(result);
            Assert.True(_context.Cards.Count() == 0);
        }

        [Fact]
        public async Task GetCardsByBinderIdAsync_ValidId()
        {
            //access to context and cardservice
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //arrange, create a binder, and a couple cards to add to that binder
            CardBinder binder = new()
            {
                Name = "Trade Binder",
                EstimatedValue = 300m,
                CollectionId = "22"
            };

            _context.Binders.Add(binder);
            await _context.SaveChangesAsync();

            Card cardOne = new()
            {
                Name = "Black Lotus",
                Value = 200m,
                Condition = CardLibrary.Enums.CardCondition.Good,
                BinderId = binder.Id,
                Grading = null,
                Image = null,
            };

            Card cardTwo = new()
            {
                Name = "Mox Jet",
                Value = 100m,
                Condition = CardLibrary.Enums.CardCondition.Poor,
                BinderId = binder.Id,
            };

            //add cards to binder
            binder.Cards.Add(cardOne);
            binder.Cards.Add(cardTwo);
            await _context.SaveChangesAsync();

            //act, call the test method
            List<Card> cardList = await _cardService.GetCardsByBinderIdAsync(binder.Id);

            // Assert: exact count match
            Assert.Equal(2, cardList.Count);

            // Assert: all cards belong to the correct binder
            Assert.All(cardList, card => Assert.Equal(binder.Id, card.BinderId));

            // Assert: field-level integrity for cardOne
            Card retrievedCardOne = cardList.Single(c => c.Name == "Black Lotus");
            Assert.Equal(200m, retrievedCardOne.Value);
            Assert.Equal(CardCondition.Good, retrievedCardOne.Condition);
            Assert.Null(retrievedCardOne.Grading);
            Assert.Null(retrievedCardOne.Image);

            // Assert: field-level integrity for cardTwo
            Card retrievedCardTwo = cardList.Single(c => c.Name == "Mox Jet");
            Assert.Equal(100m, retrievedCardTwo.Value);
            Assert.Equal(CardCondition.Poor, retrievedCardTwo.Condition);

            // Assert: no phantom cards exist in the database
            int totalCardCount = await _context.Cards.CountAsync();
            Assert.Equal(2, totalCardCount);



        }

        [Fact]
        public async Task GetCardsByBinderIdAsync_InvalidId()
        {
            //Arrange
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Act
            List<Card> cards = await _cardService.GetCardsByBinderIdAsync("1234");

            //Assert
            Assert.Empty(_context.Cards);
            Assert.Empty(cards);

        }

        [Fact]
        public async Task GetCardsByBinderIdAsync_BinderExistButHasNoCards()
        {
            (CardDbContext _context, CardService _cardService) = CreateIsolatedTestHarness();

            //Arrange, create a binder and save. No cards
            CardBinder binder = new()
            {
                Name = "Trade Binder",
                CollectionId = "5"
            };

            _context.Binders.Add(binder);
            await _context.SaveChangesAsync();

            //Act, Call our method
            List<Card> cards = await _cardService.GetCardsByBinderIdAsync(binder.Id);

            //Assert, binder has no cards
            Assert.Empty(_context.Cards);
            Assert.Empty(cards);
            CardBinder cardBinder = await _context.Binders.FindAsync(binder.Id);
            Assert.NotNull(cardBinder);
            Assert.Equal("Trade Binder", cardBinder.Name);
            Assert.Equal("5", cardBinder.CollectionId);
        }

        //helper method
        private CardService CreateIsolatedService()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            CardDbContext context = new CardDbContext(options);
            return new CardService(context);

        }

        //helper method
        private (CardDbContext context, CardService service) CreateIsolatedTestHarness()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            CardDbContext context = new CardDbContext(options);
            CardService service = new CardService(context);

            return (context, service);
        }
    }
}
