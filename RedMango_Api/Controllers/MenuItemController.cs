using AutoMapper;
using Common;
using DataAccess.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Models;
using Models.DTO;
using RedMango_Api.Services.Contracts;
using Repositories.Contracts;
using System;

namespace RedMango_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;
        private ApiResponse _response;

        public MenuItemController(IMenuItemRepository menuItemRepository, IMapper mapper, IFileUpload fileUpload)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
            _fileUpload = fileUpload;
            _response = new ApiResponse();
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<ApiResponse>> GetMenuItems() 
        {
            _response.Result =  _mapper.Map<List<MenuItemDTO>>(await _menuItemRepository.GetAllAsync());
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok( _response);
        }

        [HttpGet("{id:int}", Name="GetMenuItem")]
        public async Task<ActionResult<ApiResponse>> GetMenuItem(int id)
        {
            if (id == 0)
            {
                _response.Result = "Invalid Data";
                _response.StatusCode=System.Net.HttpStatusCode.BadRequest;
                return BadRequest( _response);
            }
            MenuItem menuItem = await _menuItemRepository.GetAsync(id);

            if (menuItem == null)
            {
                _response.Result = "No Data Found";
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound( _response);
            }

            _response.Result = _mapper.Map<MenuItemDTO>(menuItem);
            _response.StatusCode=System.Net.HttpStatusCode.OK;
            return Ok( _response);
            
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromForm] MenuItemCreateDTO menuItemCreateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemCreateDTO.File == null || menuItemCreateDTO.File.Length == 0) 
                    { 
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>() { "No File or Empty file! - File is required" };
                        return BadRequest(_response);
                    }
                    
                    var uploadedImagePath = await _fileUpload.UploadFile(menuItemCreateDTO.File);

                    MenuItem menuItemToCreate = new()
                    {
                        Name = menuItemCreateDTO.Name,
                        Price = menuItemCreateDTO.Price,
                        Category = menuItemCreateDTO.Category,
                        SpecialTag = menuItemCreateDTO.SpecialTag,
                        Description = menuItemCreateDTO.Description,
                        Image = uploadedImagePath
                    };

                    await _menuItemRepository.AddAsync(menuItemToCreate);
                    _response.Result = menuItemToCreate;
                    _response.StatusCode = System.Net.HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItemToCreate.Id }, _response);

                } else
                {
                    _response.IsSuccess = false;

                }

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<ApiResponse>> UpdateMenuItem(int id, [FromForm] MenuItemUpdateDTO menuItemUpdateDTO )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemUpdateDTO == null || id != menuItemUpdateDTO.Id)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>() { "Bad Request - Item does not match"};
                        return BadRequest(_response);
                    }

                    MenuItem menuItemFromDb = await _menuItemRepository.GetAsync(id);
                    if (menuItemFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>() { "Bad Request - Item does not exist" };
                        return BadRequest(_response);
                    }

                    
                    menuItemFromDb.Price = menuItemUpdateDTO.Price;
                    menuItemFromDb.Name = menuItemUpdateDTO.Name;
                    menuItemFromDb.Category = menuItemUpdateDTO.Category;
                    menuItemFromDb.SpecialTag = menuItemUpdateDTO.SpecialTag;
                    menuItemFromDb.Description = menuItemUpdateDTO.Description;

                    if (menuItemUpdateDTO.File != null && menuItemUpdateDTO.File.Length>0)
                    {
                        // update the file - so first remove old file
                        _fileUpload.DeleteFile(menuItemFromDb!.Image!.Split('/').Last());
                        var uploadedImagePath = await _fileUpload.UploadFile(menuItemUpdateDTO.File);

                        menuItemFromDb.Image = uploadedImagePath;

                    }

                    await _menuItemRepository.UpdateAsync(menuItemFromDb);
                    _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    return Ok(_response);

                }
                else
                {
                    _response.IsSuccess = false;

                }

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
        {
            try
            {
                
                    if (id == 0)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>() { "Bad Request - Item does not match" };
                        return BadRequest(_response);
                    }

                    MenuItem menuItemFromDb = await _menuItemRepository.GetAsync(id);
                    if (menuItemFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>() { "Bad Request - Item does not exist" };
                        return BadRequest(_response);
                    }

                    _fileUpload.DeleteFile(menuItemFromDb!.Image!.Split('/').Last());

                    await _menuItemRepository.DeleteAsync(id);
                    _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    return Ok(_response);
 

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
