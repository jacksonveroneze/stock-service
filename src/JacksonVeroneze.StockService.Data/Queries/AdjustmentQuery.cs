using System;
using System.Linq.Expressions;
using JacksonVeroneze.StockService.Data.Util;
using JacksonVeroneze.StockService.Domain.Entities;
using JacksonVeroneze.StockService.Domain.Enums;
using JacksonVeroneze.StockService.Domain.Filters;

namespace JacksonVeroneze.StockService.Data.Queries
{
    public static class AdjustmentQuery
    {
        public static Expression<Func<Adjustment, bool>> GetQuery(AdjustmentFilter filter)
        {
            Expression<Func<Adjustment, bool>> expression = order => true;

            if (!string.IsNullOrEmpty(filter.Description))
                expression = expression.And(x => x.Description.Contains(filter.Description));

            if (filter.State.HasValue)
                expression = expression.And(x => x.State == (AdjustmentState)Enum.Parse(typeof(AdjustmentState),
                    filter.State.ToString()));

            if (filter.DateInitial.HasValue)
                expression = expression.And(x => x.Date >= filter.DateInitial);

            if (filter.DateEnd.HasValue)
                expression = expression.And(x => x.Date <= filter.DateEnd);

            return expression;
        }
    }
}
