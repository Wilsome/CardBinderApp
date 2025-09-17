using CardInfrastructure.DTO;
using CardInfrastructure.Interfaces;
using CardInfrastructure.Services;
using CardLibrary.Enums;
using CardLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardBinderApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController(ICollectionService collectionService) : ControllerBase
    {
        //will need Collection interface
        private readonly ICollectionService _collectionService = collectionService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollectionById(string id)
        {
            //pull collection from db
            Collection collection = await _collectionService.GetCollectionById(id);
            //validate
            if (collection == null)
            {
                return NotFound($"Collection with {id} not found.");
            }

            //response
            return Ok(new CollectionResponseDTO
            {
                Id = collection.Id,
                Name = collection.Name,
                Theme = collection.Theme.ToString(),
                EstimatedValue = collection.EstimatedValue,
                CreatedAt = collection.CreatedAt,
            });
        }

        [HttpGet]
        //get all
        public async Task<IActionResult> GetAllCollections()
        {
            List<Collection> collections = await _collectionService.GetAllCollectionsAsyn();

            if (collections == null)
            {
                return NotFound($"No collections found.");
            }

            return Ok(collections);
        }

        [HttpDelete]
        //delete
        public async Task<IActionResult> DeleteCollectionByIdAsync(string id)
        {
            bool result = await _collectionService.DeleteCollectionByIdAsync(id);

            if (!result)
            {
                return NotFound($"No collection {id} found");
            }

            return Ok($"Collection {id} has been deleted.");
        }


        //create
        [HttpPost]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CreateCollectionDto dto)
        {
            //validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //create collection object
            Collection collection = new()
            {
                Name = dto.Name,
                Theme = dto.Theme,
                EstimatedValue = dto.EstimatedValue,
                UserId = dto.UserId
            };

            //pass this object to our service method to do the backend work
            await _collectionService.CreateCollectionAsync(collection);

            return Ok(new CollectionResponseDTO
            {
                Name = collection.Name,
                Theme = collection.Theme.ToString(),
                EstimatedValue = collection.EstimatedValue,
                Id = collection.Id,
                CreatedAt = collection.CreatedAt,
            });
        }

        //get all by userId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllCollectionsByUserId(string userId)
        {
            List<Collection> collections = await _collectionService.GetAllCollectionsByUserId(userId);
            //validate
            if (collections == null || collections.Count == 0)
            {
                return NotFound($"No collections found for user {userId}.");

            }

            List<CollectionResponseDTO> responseList = new();

            //convert each collection into ResponseDTO
            foreach (Collection collection in collections)
            {
                //create responseDTO
                CollectionResponseDTO responseDTO = new()
                {
                    Id = collection.Id,
                    Name = collection.Name,
                    Theme = collection.Theme.ToString(),
                    EstimatedValue = collection.EstimatedValue,
                    CreatedAt = collection.CreatedAt,

                };

                //add to list
                responseList.Add(responseDTO);

            }

            //return list DTO
            return Ok(responseList);
        }


        //patch
        [HttpPatch("{collectionId}")]
        public async Task<IActionResult> PatchCollectionAsync([FromBody] PatchCollectionDto dto, string collectionId) 
        {
            //validation
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            //pull collection from db
            Collection collection = await _collectionService.GetCollectionById(collectionId);

            if (collection == null) 
            {
                return NotFound($"Collction {collectionId} not found.");
            }

            //update properties
            if (!string.IsNullOrEmpty(dto.Name)) 
            {
                collection.Name = dto.Name;
            }
            if (dto.Theme.HasValue) 
            {
                collection.Theme = dto.Theme.Value;
            }
            if (dto.EstimatedValue.HasValue) 
            {
                collection.EstimatedValue = dto.EstimatedValue.Value;
            }

            //call save
            await _collectionService.UpdateCollectionAsync(collection);

            return Ok();
        }
    }
}
