using System;
using System.Linq.Expressions;
using JacksonVeroneze.StockService.Domain.Entities;
using JacksonVeroneze.StockService.Domain.Filters;
using JacksonVeroneze.StockService.Domain.Util;

namespace JacksonVeroneze.StockService.Domain.Queries
{
    public static class MovementQuery
    {
        public static Expression<Func<Movement, bool>> GetQuery(MovementFilter filter)
        {
            Expression<Func<Movement, bool>> expression = query => true;


            if (filter.productId.HasValue)
                expression = expression.And(x => x.Product.Id == filter.productId.Value);

            return expression;
        }
    }
}
