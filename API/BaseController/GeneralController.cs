﻿using API.Models;
using API.Repository;
using API.Repository.Data;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.BaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController<TRepository, TEntity, TKey> : ControllerBase
        where TRepository : IGeneralRepository<TEntity, TKey>
    {
        protected readonly TRepository _repository;

        public GeneralController(TRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var results = _repository.GetAll();
            //Handle ketika data tidak ada/ kosong
            if (results == null)
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "There are no data available"
                });

            return Ok(new ResponseDataVM<IEnumerable<TEntity>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = results
            });
        }

        [HttpGet("{key}")]
        public ActionResult GetByKey(TKey key)
        {
            var result = _repository.GetByKey(key);
            if (result == null)
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "Id Not Found"
                });

            return Ok(new ResponseDataVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = result
            });
        }

        [HttpPost]
        public ActionResult Insert(TEntity entity)
        {
            /*if (university.Name == "" || university.Name.ToLower() == "string")
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }*/

            var insert = _repository.Insert(entity);
            if (insert > 0)
                return Ok(new ResponseDataVM<TEntity>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Insert Success"
                });

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Insert Failed / Lost Connection"
            });
        }

        [HttpPut]
        public ActionResult Update(TEntity entity)
        {
            /*if (university.Name == "" || university.Name.ToLower() == "string")
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }*/

            var update = _repository.Update(entity);
            if (update > 0)
                return Ok(new ResponseDataVM<TEntity>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Update Success"
                });

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Update Failed / Lost Connection"
            });
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(TKey key)
        {
            var delete = _repository.Delete(key);
            if (delete > 0)
                return Ok(new ResponseDataVM<TEntity>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Delete Success"
                });

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Delete Failed / Lost Connection"
            });
        }
    }
}
