using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using ECommerce.Services.Discount.Dtos;
using ECommerce.Services.Discount.Settings;
using ECommerce.Shared.Dtos;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            _dbConnection = new MySqlConnection(databaseSettings.ToString());
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE Id = @Id", new { Id = id });
            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount Not Deleted", 500);
        }

        public async Task<Response<List<DiscountDto>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount");
            return Response<List<DiscountDto>>.Success(_mapper.Map<List<DiscountDto>>(discounts), 200);
        }

        public async Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE Code = @Code AND UserId = @UserId", new { Code = code, UserId = userId })).FirstOrDefault();
            return discount != null ? Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount), 200) : Response<DiscountDto>.Fail("Discount not found", 404);
        }

        public async Task<Response<DiscountDto>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE Id = @Id", new { Id = id })).SingleOrDefault();
            return discount != null ? Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount), 200) : Response<DiscountDto>.Fail("Discount not found", 404);
        }

        public async Task<Response<NoContent>> Save(CreateDiscountDto dto)
        {
            var discount = _mapper.Map<Models.Discount>(dto);
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount(UserId,Rate,Code) VALUES (@UserId,@Rate,@Code)", discount);
            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount Not Saved", 404);
        }

        public async Task<Response<NoContent>> Update(UpdateDiscountDto dto)
        {
            var discount = _mapper.Map<Models.Discount>(dto);
            var status = await _dbConnection.ExecuteAsync("UPDATE discount SET UserId=@UserId,Code=@Code,Rate=@Rate WHERE Id=@Id", discount);
            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount Not Updated", 404);
        }
    }
}
