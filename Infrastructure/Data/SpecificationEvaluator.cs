using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> Evalute(IQueryable<TEntity> inputquery
            ,ISpecification<TEntity> specification)
        {
            var query = inputquery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            query = specification.Includes.Aggregate(query, (currentquery, include) => currentquery.Include(include));
            return query;
        }
    }
}
