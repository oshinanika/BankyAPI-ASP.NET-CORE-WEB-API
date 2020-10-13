using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankyAPI.Models;
using BankyAPI.Models.DTOs;
using BankyAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] //We can see these StatusCodes in SwaggerUI
    public class BankController : ControllerBase //API will always have ControllerBase
    {
    
        private IBankRepository _IbankRepo; //private variable name will start with _ .
        private readonly IMapper _mapper;

        public BankController(IBankRepository IbankRepo, IMapper mapper)
        {
            _IbankRepo = IbankRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get List of Accounts
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BankDTO>))]
         public IActionResult GetAccounts()
        {
            try
            {
                var objList = _IbankRepo.GetAccounts();

                //--------Rather than using the Repository directly, We will use DTOs to access the data
                
                var objDTO = new List<BankDTO>();

                foreach (var obj in objList){
                    objDTO.Add(_mapper.Map<BankDTO>(obj)); //Mapping each obj from repo to the DTO
                }

                return Ok(objDTO); //we are returning the DTO
            }
            catch (Exception e)
            {

                throw new Exception("Get Accounts Error :", e);
            }

        }

        /// <summary>
        /// Get Individual Account
        /// </summary>
        /// <param name="Id">Get Id of the Account</param>
        /// <returns></returns>
        [HttpGet("{Id:int}", Name = "GetAccountById")]  //We have to define Id otherwise HTTPGET gets confused with the previous one. We can name this HTTPGET too.
        [ProducesResponseType(200, Type = typeof(BankDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetAccount(int Id)
        {
            try
            {
                var obj = _IbankRepo.GetAccount(Id);

                if(obj == null)
                {
                    return NotFound();
                }

                //--------Rather than using the Repository directly, We will use DTOs to access the data

                var objDTO = _mapper.Map<BankDTO>(obj); //Converting the db obj to DTO

                //--------------------If we didn't use AutoMapper we had to map data manually like this
                //var objDTO = new BankDTO()
                //{
                //    Id = obj.Id,
                //    AccountHolderName = obj.AccountHolderName,
                //    AccountNumber = obj.AccountNumber,
                //    Accountbalance = obj.AccountBalance,
                //    AccountCreated = obj.AccountCreated
                //};

                return Ok(objDTO); //we are returning the DTO
            }
            catch (Exception e)
            {

                throw new Exception("Get Account Error :", e);
            }

        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BankDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateAccount([FromBody] BankDTO bankDTO)
        {
            try
            {
                if(bankDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (_IbankRepo.AccountExists(bankDTO.AccountNumber))
                {
                    ModelState.AddModelError("", "Bank Account Exixts");
                    return StatusCode(404, ModelState);
                }

                //We have [required] field in the model 
                //so don't need ModelState.IsValid to check

                var bankObj = _mapper.Map<Bank>(bankDTO); //Converting the received DTO to db obj

                if (!_IbankRepo.CreateAccount(bankObj))
                {
                    ModelState.AddModelError("", $"Something happened while saving the record {bankObj.AccountNumber}");
                    return StatusCode(500, ModelState);               
                }

                //return Ok(); 

                //CreatedAtrouted gives 201 statuscode and displays the new created obj
                //It uses the HHTPGET byt Id method from previous and passes the new ID
                return CreatedAtRoute("GetAccountById", new {Id = bankObj.Id}, bankObj);

            }
            catch (Exception e)
            {

                throw new Exception("Created Account Error :", e);
            }
        }


        [HttpPatch("{Id:int}", Name = "UpdateAccount")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateAccount(int Id, [FromBody] BankDTO bankDTO)
        {

            try
            {
                if (bankDTO == null || Id!=bankDTO.Id)
                {
                    return BadRequest(ModelState);
                }

                var bankObj = _mapper.Map<Bank>(bankDTO); //Converting the received DTO to db obj

                if (!_IbankRepo.UpdateAccount(bankObj)) //IBankRepository's Method
                {
                    ModelState.AddModelError("", $"Something happened while updating the record {bankObj.AccountNumber}");
                    return StatusCode(500, ModelState);
                }


                return NoContent(); //204 status-code, With Path we don't retun any obj
            }
            catch (Exception e)
            {

                throw new Exception("Updated Account Error :", e);
            }
            
        }


        [HttpDelete("{Id:int}", Name = "DeleteAccount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteAccount(int Id)
        {

            try
            {
                if (!_IbankRepo.AccountExists(Id))
                {
                    return NotFound();
                }

                var bankObj = _IbankRepo.GetAccount(Id);

                if (!_IbankRepo.DeleteAccount(bankObj))
                {
                    ModelState.AddModelError("", $"Something happened while Deleting the record {bankObj.AccountNumber}");
                    return StatusCode(500, ModelState);
                }


                return NoContent(); //204 status-code, With Delete we don't retun any obj
            }
            catch (Exception e)
            {

                throw new Exception("Delete Account Error :", e);
            }

        }

    }
}