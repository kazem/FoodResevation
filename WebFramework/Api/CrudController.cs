using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace WebFramework.Api
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiResultFilter]
    [ApiController]
    public class CrudController<TDto, TSelectDto, TEntity, TKey> : ControllerBase
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly IRepository<TEntity> repository;
        protected readonly IMapper mapper;

        public CrudController(IRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TSelectDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await repository.TableNoTracking.ProjectTo<TDto>(mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TSelectDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            var dto = await repository.TableNoTracking.ProjectTo<TSelectDto>(mapper.ConfigurationProvider)
                                                      .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return NotFound();

            return dto;
        }

        [HttpPost]
        public virtual async Task<ApiResult<TSelectDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);

            await repository.AddAsync(model, cancellationToken);

            var resultDto = await repository.TableNoTracking.ProjectTo<TSelectDto>(mapper.ConfigurationProvider)
                                                            .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }
    }
}
